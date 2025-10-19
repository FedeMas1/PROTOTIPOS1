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
    public partial class PR : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        private int idPrActual = -1;    
        public PR()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void PR_Load(object sender, EventArgs e)
        { 
            CargarRubros();
            lblNPedido.Text = "XXXX";
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

                string query = "SELECT codigo_Producto, Descripcion, (cantidad_maxima - cantidad) AS cantidad_a_pedir, marca, id_Rubro FROM Stock WHERE id_Rubro = @Rubro AND cantidad < punto_pedido" +
                    " UNION ALL " +
                    "SELECT codigo_Producto, Descripcion, (cantidad_maxima - cantidad_compra) AS cantidad_a_pedir, marca, id_Rubro FROM Materiales WHERE id_Rubro = @Rubro AND cantidad_compra < punto_pedido";

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
            if (cmbRubros.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un rubro");
                return;
            }

            int rubroSeleccionado = Convert.ToInt32(cmbRubros.SelectedIndex);
            CargarProductos(rubroSeleccionado);
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if(dgvProductos.Rows.Count > 0)
            {
                MessageBox.Show("No hay productos para guardar.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (idPrActual == 0)
                {
                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string query = "INSERT INTO Pr (fecha, rubro, codigo_Producto, descripcion, cantidad_a_pedir, marca, estado) VALUES (@Fecha, @Rubro, @Codigo, @Descripcion, @Cantidad, @Marca, @Estado)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue(@"Fecha", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue(@"Rubro", row.Cells["id_Rubro"].Value);
                            cmd.Parameters.AddWithValue(@"Codigo", row.Cells["codigo_producto"].Value);
                            cmd.Parameters.AddWithValue(@"Descripcion", row.Cells["Descripcion"].Value);
                            cmd.Parameters.AddWithValue(@"Cantidad", row.Cells["cantidad_a_pedir"].Value);
                            cmd.Parameters.AddWithValue(@"Marca", row.Cells["marca"].Value);
                            cmd.Parameters.AddWithValue("@Estado", "@Solicitado");

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Pedido de reaprovisionamiento dado de alta correctamente");
                }
                else
                {
                    foreach(DataGridViewRow row in dgvProductos.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string query = "UPDATE Pr SET fecha=@Fecha, rubro=@Rubro, codigo_producto=@Codigo, descripcion=@Descripcion, cantidad_a_pedir=@Cantidad, marca=@Marca, estado=@Estado WHERE id_Pr=@Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue(@"Fecha", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue(@"Rubro", row.Cells["id_Rubro"].Value);
                            cmd.Parameters.AddWithValue(@"Codigo", row.Cells["codigo_producto"].Value);
                            cmd.Parameters.AddWithValue(@"Descripcion", row.Cells["Descripcion"].Value);
                            cmd.Parameters.AddWithValue(@"Cantidad", row.Cells["cantidad_a_pedir"].Value);
                            cmd.Parameters.AddWithValue(@"Marca", row.Cells["marca"].Value);
                            cmd.Parameters.AddWithValue("@Estado", "@Solicitado");
                            cmd.Parameters.AddWithValue("@Id", idPrActual);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Pedido de reaprovisionamineto modificado correctamente.");
                }
            }
            lblNPedido.Text = "XXXX";
            idPrActual = 0;
            dgvProductos.DataSource = null;
        }

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de PR a cargar:", "Buscar PR", "");
            if (string.IsNullOrEmpty(input)) return;

            int idBuscado;
            if(!int.TryParse(input, out idBuscado))
            {
                MessageBox.Show("Numero de Pr inavilido");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Pr WHERE id_Pr = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    MessageBox.Show("No se encontró el Pr deseado");
                    return;
                }

                idPrActual = idBuscado;
                lblNPedido.Text = idBuscado.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["fecha"]);
                cmbRubros.SelectedValue = Convert.ToInt32(dt.Rows[0]["rubro"]);

                string queryProductos = "SELECT codigo_producto, descripcion, cantidad_a_pedir, marca, rubro AS id_Rubro FROM Pr WHERE id_Pr = @id";
                SqlCommand cmdProductos = new SqlCommand(queryProductos, conn);
                cmdProductos.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataAdapter daProd = new SqlDataAdapter(cmdProductos);
                DataTable dtProducto = new DataTable();
                daProd.Fill(dtProducto);
                dgvProductos.DataSource = dtProducto;

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
            if (idPrActual == 0)
            {
                MessageBox.Show("Primero debe cargar un PR antes de eliminarlo");
                return;
            }

            DialogResult dr = MessageBox.Show("¿Desea eliminar este pedido de reaprovisionamiento?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Pr WHERE id_Pr = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", idPrActual);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Pedido de reaprovisionamiento eliminado correctamente");
            idPrActual = 0;
            lblNPedido.Text = "XXXX";
            dgvProductos.DataSource = null;
            cmbRubros.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }
    }
}
