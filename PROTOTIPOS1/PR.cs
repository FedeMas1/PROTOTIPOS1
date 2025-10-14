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
        public PR()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void PR_Load(object sender, EventArgs e)
        {

            //cmb rubros
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
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

        private void CargarProductos(string rubroSeleccionado)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string query = "SELECT codigo_Producto, Descripcion, (cantidad_maxima - cantidad) AS cantidad_a_pedir, marca, id_Rubro FROM Stock WHERE id_Rubro = @Rubro AND cantidad < punto_pedido" +
                    "UNION ALL" +
                    "SELECT codigo_Producto, Descripcion, (cantidad_maxima - cantidad) AS cantidad_a_pedir, marca, id_Rubro FROM Materiales WHERE id_Rubro = @Rubro AND cantidad < punto_pedido";

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

        private void button5_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void bttnCargar_Click(object sender, EventArgs e)
        {
            if (cmbRubros.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un rubro");
                return;
            }

            string rubroSeleccionado = cmbRubros.SelectedItem.ToString();
            CargarProductos(rubroSeleccionado);
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    if (row.IsNewRow) continue;

                    string query = "INSERT INTO Sc (fecha, rubro, codigo_Producto, descripcion, cantidad_a_pedir, marca, solicitado, aprobado, denegado, cotizado) VALUES (@Fecha, @Rubro, @Codigo, @Descripcion, @Cantidad, @Marca, 1, 0, 0, 0)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue(@"Fecha", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue(@"Rubro", row.Cells["Rubro"].Value);
                        cmd.Parameters.AddWithValue(@"Codigo", row.Cells["Codigo"].Value);
                        cmd.Parameters.AddWithValue(@"Descripcion", row.Cells["Descripcion"].Value);
                        cmd.Parameters.AddWithValue(@"Cantidad", row.Cells["Cantidad"].Value);
                        cmd.Parameters.AddWithValue(@"Marca", row.Cells["Marca"].Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void bttnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {

        }
    }
}
