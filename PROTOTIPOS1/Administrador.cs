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
    public partial class Administrador : Form
    {
        string cadena_Conexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Administrador()
        {
            InitializeComponent();
            dgvUsuarios.CellEndEdit += dgvUsuarios_CellEndEdit;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Modulos mod = new Modulos();
            mod.Show();
            Hide();
        }

        private void Administrador_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena_Conexion))
                {
                    conn.Open();
                    string consulta = "SELECT id_Usuario, nombre_Usuario, email, contraseña, nombre, apellido, nivel FROM Usuarios ";
                    SqlDataAdapter adapter = new SqlDataAdapter(consulta, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvUsuarios.DataSource = dt;

                    // nivel
                    foreach(DataGridViewColumn columna in dgvUsuarios.Columns)
                    {
                        if(columna.Name == "nivel")
                        {
                            columna.ReadOnly = false;
                        }
                        else
                        {
                            columna.ReadOnly = true;
                        }
                    }

                    dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }


            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar " + ex.Message);
            }
    }

        private void dgvUsuarios_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.Columns[e.ColumnIndex].Name != "nivel")
                return;

            try
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["id_Usuario"].Value);
                int nuevoNivel = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["nivel"].Value);

                if (nuevoNivel < 1 || nuevoNivel > 3)
                {
                    MessageBox.Show("Nivel inválido. Solo se permiten valores 1, 2 o 3.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarUsuarios(); 
                    return;
                }

                using (SqlConnection conn = new SqlConnection(cadena_Conexion))
                {
                    conn.Open();
                    string consulta = "UPDATE Usuarios SET nivel = @nivel WHERE id_Usuario = @id";

                    SqlCommand comando = new SqlCommand(consulta, conn);
                    comando.Parameters.AddWithValue("@nivel", nuevoNivel);
                    comando.Parameters.AddWithValue("@id", idUsuario);

                    comando.ExecuteNonQuery();
                }

                MessageBox.Show("Nivel actualizado correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al actualizar el nivel" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
