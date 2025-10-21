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
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
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

                    if (!dgvUsuarios.Columns.Contains("Eliminar"))
                    {
                        DataGridViewButtonColumn bttnEliminar = new DataGridViewButtonColumn();
                        bttnEliminar.Name = "Eliminar";
                        bttnEliminar.HeaderText = "Eliminar";
                        bttnEliminar.Text = "Eliminar";
                        bttnEliminar.UseColumnTextForButtonValue = true;
                        dgvUsuarios.Columns.Add(bttnEliminar);
                    }

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
       
        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && dgvUsuarios.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["id_Usuario"].Value);
                string nombre = dgvUsuarios.Rows[e.RowIndex].Cells["nombre_Usuario"].Value.ToString();
                
                DialogResult confirmar = MessageBox.Show(
           $"¿Seguro que desea eliminar el usuario '{nombre}'?",
           "Confirmar eliminación",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question
       );
                if(confirmar == DialogResult.Yes)
                {
                    try
                    {
                        using(SqlConnection conn = new SqlConnection(cadena_Conexion))
                        {
                            conn.Open();
                            string consulta = "DELETE FROM Usuarios WHERE id_Usuario = @id";
                            SqlCommand comando = new SqlCommand(consulta, conn);
                            comando.Parameters.AddWithValue("@id", idUsuario);
                            comando.ExecuteNonQuery();
                        }
                        MessageBox.Show("Usuario eliminado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuarios();
                    }catch(Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        

        private void btnbackup_Click_1(object sender, EventArgs e)
        {
            /*string connectionString = @"Data Source=DESKTOP-1N5CLIH\MSQLSERVER2022;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";*/
            string nombre_BD = "Panaderia";
            string backupPath = @"C:\Backups\panaderia.bak";

            string query = $@"
            BACKUP DATABASE [{nombre_BD}]
            TO DISK = N'{backupPath}'
            WITH INIT, NAME = N'Full Backup of {nombre_BD}';
            ";

            try
            {
                using (SqlConnection connection = new SqlConnection(cadena_Conexion))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Backup completado correctamente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al hacer el backup" + ex.Message);

            }
        }

        private void btnrestore_Click_1(object sender, EventArgs e)
        {
            /*string connectionString = $@"Data Source=DESKTOP-1N5CLIH\MSQLSERVER2022;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;";*/

            string nombre_BD = "Panaderia";
            string backup = @"C:\Backups\panaderia.bak";


            string query = $@"
            USE master;
            ALTER DATABASE [{nombre_BD}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            RESTORE DATABASE [{nombre_BD}]
            FROM DISK = N'{backup}'
            WITH REPLACE;
            ALTER DATABASE [{nombre_BD}] SET MULTI_USER;
            ";

            try
            {


                using (SqlConnection connection = new SqlConnection(cadena_Conexion))

                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }


                MessageBox.Show(" Restauración completada correctamente.", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error al restaurar la base de datos:" + ex.Message);
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

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

