using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROTOTIPOS1
{
    public partial class SC : Form
    {
        private string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        private int idScActual = 0;
        public SC()
        {
            InitializeComponent();
        }

        private void SC_Load(object sender, EventArgs e)
        {
            CargarRubros();
            lblNSolicitud.Text = "XXXX";
        }

        private void CargarRubros()
        {
            string consulta = "SELECT id_Rubro, Descripcion FROM Rubros";
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(consulta, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbRubros.DisplayMember = "Descripcion";
                cmbRubros.ValueMember = "id_Rubro";
                cmbRubros.DataSource = dt;
                cmbRubros.SelectedIndex = -1;
            }
        }

        private void CargarProductos(int rubroSeleccionado)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string query = "SELECT cod_Producto AS codigo_bien_de_uso, Descripcion, (cantidad_maxima - cantidad) AS cantidad_a_pedir, marca, id_Rubro " +
                               "FROM Inventario WHERE id_Rubro = @Rubro AND cantidad < punto_pedido AND activo = 1";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Rubro", rubroSeleccionado);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvProductos.DataSource = dt;
                }
            }

            if (!dgvProductos.Columns.Contains("Eliminar"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.Name = "Eliminar";
                btnEliminar.Text = "Eliminar";
                btnEliminar.UseColumnTextForButtonValue = true;
                dgvProductos.Columns.Add(btnEliminar);
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProductos.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                dgvProductos.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void bttnCargar_Click(object sender, EventArgs e)
        {
            if (cmbRubros.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un rubro.");
                return;
            }

            int rubroSeleccionado = Convert.ToInt32(cmbRubros.SelectedValue);
            CargarProductos(rubroSeleccionado);
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos para guardar.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (idScActual == 0)
                {
                    // GUARDAR 
                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string query = "INSERT INTO SC (fecha, codigo_bien_de_uso, descripcion, cantidad_a_pedir, marca, rubro, estado) " +
                                       "VALUES (@Fecha, @Codigo, @Descripcion, @Cantidad, @Marca, @Rubro, @Estado)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@Codigo", row.Cells["codigo_bien_de_uso"].Value);
                            cmd.Parameters.AddWithValue("@Descripcion", row.Cells["Descripcion"].Value);
                            cmd.Parameters.AddWithValue("@Cantidad", row.Cells["cantidad_a_pedir"].Value);
                            cmd.Parameters.AddWithValue("@Marca", row.Cells["marca"].Value);
                            cmd.Parameters.AddWithValue("@Rubro", row.Cells["id_Rubro"].Value);
                            cmd.Parameters.AddWithValue("@Estado", "Solicitado");

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Solicitud de compra guardada correctamente.");
                }
                else
                {
                    // MODIFICAR 
                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string query = "UPDATE SC SET fecha=@Fecha, codigo_bien_de_uso=@Codigo, descripcion=@Descripcion, cantidad_a_pedir=@Cantidad, marca=@Marca, rubro=@Rubro, estado=@Estado WHERE id_Sc=@Id";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@Codigo", row.Cells["codigo_bien_de_uso"].Value);
                            cmd.Parameters.AddWithValue("@Descripcion", row.Cells["Descripcion"].Value);
                            cmd.Parameters.AddWithValue("@Cantidad", row.Cells["cantidad_a_pedir"].Value);
                            cmd.Parameters.AddWithValue("@Marca", row.Cells["marca"].Value);
                            cmd.Parameters.AddWithValue("@Rubro", row.Cells["id_Rubro"].Value);
                            cmd.Parameters.AddWithValue("@Estado", "Solicitado");
                            cmd.Parameters.AddWithValue("@Id", idScActual);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Solicitud de compra modificada correctamente.");
                }
            }

            lblNSolicitud.Text = "XXXX";
            idScActual = 0;
            dgvProductos.DataSource = null;
      }

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de Solicitud de Compra a cargar:", "Buscar SC", "");
            if (string.IsNullOrWhiteSpace(input)) return;

            int idBuscado;
            if (!int.TryParse(input, out idBuscado))
            {
                MessageBox.Show("Número de SC inválido.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM SC WHERE id_Sc = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró la solicitud especificada.");
                    return;
                }

                idScActual = idBuscado;
                lblNSolicitud.Text = idBuscado.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["fecha"]);
                cmbRubros.SelectedValue = Convert.ToInt32(dt.Rows[0]["rubro"]);

                // cargar productos del SC
                string queryProductos = "SELECT codigo_bien_de_uso, descripcion, cantidad_a_pedir, marca, rubro AS id_Rubro FROM SC WHERE id_Sc = @Id";
                SqlCommand cmdProductos = new SqlCommand(queryProductos, conn);
                cmdProductos.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataAdapter daProd = new SqlDataAdapter(cmdProductos);
                DataTable dtProd = new DataTable();
                daProd.Fill(dtProd);
                dgvProductos.DataSource = dtProd;

                if (!dgvProductos.Columns.Contains("Eliminar"))
                {
                    DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                    btnEliminar.Name = "Eliminar";
                    btnEliminar.Text = "Eliminar";
                    btnEliminar.UseColumnTextForButtonValue = true;
                    dgvProductos.Columns.Add(btnEliminar);
                }
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            if (idScActual == 0)
            {
                MessageBox.Show("Primero debe cargar una solicitud antes de eliminarla.");
                return;
            }

            DialogResult dr = MessageBox.Show("¿Desea eliminar esta solicitud de compra?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM SC WHERE id_Sc = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", idScActual);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Solicitud de compra eliminada correctamente.");

            idScActual = 0;
            lblNSolicitud.Text = "XXXX";
            dgvProductos.DataSource = null;
            cmbRubros.SelectedIndex = -1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }
    }
}
    