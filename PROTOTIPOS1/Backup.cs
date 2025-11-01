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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROTOTIPOS1
{
    public partial class Backup : Form
    {
        string cadena_Conexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Backup()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnrestore, "Restaurar la Base de Datos.");
            toolTip.SetToolTip(btnbackup, "Respaldar la Base de Datos.");
        }

        private void btnbackup_Click(object sender, EventArgs e)
        {
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

        private void btnrestore_Click(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            PaginaAdministrador pAdmin = new PaginaAdministrador();
            pAdmin.Show();
            this.Hide();
        }
    }
    }

