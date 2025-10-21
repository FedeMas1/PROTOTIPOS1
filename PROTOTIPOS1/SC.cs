using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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

            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
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

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvProductos.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                if(e.RowIndex >= 0 && e.RowIndex < dgvProductos.Rows.Count && !dgvProductos.Rows[e.RowIndex].IsNewRow)
                {
                    DialogResult confirm = MessageBox.Show("¿Desea eliminar este producto?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(confirm == DialogResult.Yes)
                    {
                        dgvProductos.Rows.RemoveAt(e.RowIndex);
                    }
                }
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

            DialogResult confirm = MessageBox.Show("¿Desea guardar la solicitud de compra?", "Confirmar guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int nuevoIdSc = 0;
                    if (idScActual == 0)
                    {                        // Agregar en Sc_Master
                        string insertMaster = "INSERT INTO Sc_Master (fecha, rubro, estado) " +
                            "VALUES (@Fecha, @Rubro, @Estado); " +
                            "SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(insertMaster, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@Rubro", cmbRubros.SelectedValue);
                            cmd.Parameters.AddWithValue("@Estado", "Solicitado");
                            nuevoIdSc = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }


                    if (idScActual == 0)
                    {
                        // GUARDAR en Sc_Detalle
                        foreach (DataGridViewRow row in dgvProductos.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string queryDetalle = "INSERT INTO Sc_Detalle (id_Sc, codigo_bien_de_uso, descripcion, cantidad_a_pedir, marca) " +
                                           "VALUES (@IdSc, @Fecha, @Codigo, @Descripcion, @Cantidad, @Marca)";

                            using (SqlCommand cmd = new SqlCommand(queryDetalle, conn))
                            {
                                cmd.Parameters.AddWithValue("@IdSc", nuevoIdSc);
                                cmd.Parameters.AddWithValue("@Codigo", row.Cells["codigo_bien_de_uso"].Value);
                                cmd.Parameters.AddWithValue("@Descripcion", row.Cells["Descripcion"].Value);
                                cmd.Parameters.AddWithValue("@Cantidad", row.Cells["cantidad_a_pedir"].Value);
                                cmd.Parameters.AddWithValue("@Marca", row.Cells["marca"].Value);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Solicitud de compra guardada correctamente.");
                    }
                    else
                    {
                        // MODIFICAR 


                        string query = "UPDATE Sc_Master SET fecha=@Fecha, rubro=@Rubro, estado=@Estado WHERE id_Sc=@Id";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@Rubro", cmbRubros.SelectedValue);
                            cmd.Parameters.AddWithValue("@Estado", "Solicitado");
                            cmd.Parameters.AddWithValue("@Id", idScActual);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmdDetalle = new SqlCommand("DELETE FROM SC_Detalle WHERE id_Sc=@Id", conn, transaction))
                        {
                            cmdDetalle.Parameters.AddWithValue("@Id", idScActual);
                            cmdDetalle.ExecuteNonQuery();
                        }

                        foreach (DataGridViewRow row in dgvProductos.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string insertDetalle = "INSERT INTO Sc_Detalle (id_Sc, codigo_bien_de_uso, descripcion, cantidad_a_pedir, marca) " +
                                "VALUES (@Id, @Codigo, @Descripcion, @Cantidad, @Marca)";
                            using (SqlCommand cmdDet = new SqlCommand(insertDetalle, conn, transaction))
                            {
                                cmdDet.Parameters.AddWithValue("@IdSc", idScActual);
                                cmdDet.Parameters.AddWithValue("@Codigo", row.Cells["codigo_bien_de_uso"].Value);
                                cmdDet.Parameters.AddWithValue("@Descripcion", row.Cells["Descripcion"].Value);
                                cmdDet.Parameters.AddWithValue("@Cantidad", row.Cells["cantidad_a_pedir"].Value);
                                cmdDet.Parameters.AddWithValue("@Marca", row.Cells["marca"].Value);
                                cmdDet.ExecuteNonQuery();
                            }

                        }
                        transaction.Commit();
                        MessageBox.Show("Solicitud de compra modificada correctamente.");
                    }

                    LimpiarFormulario();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
            }
        }



        private void LimpiarFormulario()
        {
            lblNSolicitud.Text = "XXXX";
            idScActual = 0;
            dgvProductos.DataSource = null;
            cmbRubros.SelectedIndex = -1;
        }

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de Solicitud de Compra a cargar:", "Buscar SC", "");
            if (string.IsNullOrWhiteSpace(input)) return;

           
            if (!int.TryParse(input, out int idBuscado))
            {
                MessageBox.Show("Número de SC inválido.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string queryMaster = "SELECT * FROM SC WHERE id_Sc = @Id";
                SqlCommand cmd = new SqlCommand(queryMaster, conn);
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

                // cargar detalle del SC
                string queryProductos = "SELECT codigo_bien_de_uso, descripcion, cantidad_a_pedir, marca FROM SC WHERE id_Sc = @Id";
                SqlDataAdapter daDet = new SqlDataAdapter(queryProductos, conn);
                daDet.SelectCommand.Parameters.AddWithValue("@Id", idBuscado);
                DataTable dtDet = new DataTable();
                daDet.Fill(dtDet);
                dgvProductos.DataSource = dtDet;

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
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using(SqlCommand cmd1 = new SqlCommand("DELETE FROM SC_Detalle WHERE id_Sc=@Id", conn, transaction))
                    {
                        cmd1.Parameters.AddWithValue("@Id", idScActual);
                        cmd1.ExecuteNonQuery();
                    }

                    using(SqlCommand cmd2 = new SqlCommand("DELETE FROM SC_Master WHERE id_Sc = @Id", conn, transaction))
                    {
                        cmd2.Parameters.AddWithValue("@Id", idScActual);
                        cmd2.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Solicitud eliminada correctamente");
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al eliminar solicitud: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void bttnCSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro que deseas cerrar sesión?", "Cerrar sesión",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {

                Sesion.CerrarSesion();

                Login login = new Login();
                login.Show();
                this.Hide();

            }
        }
    }
}
    