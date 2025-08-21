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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string correo = txtbCorreo.Text;
            string contraseña = txtbContraseña.Text;

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Faltan ingresar datos");
            }

            string cadena_Conexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection conexion = new SqlConnection(cadena_Conexion))
            {
                try
                {
                    conexion.Open();
                    string consulta = @"SELECT nivel FROM Usuarios WHERE email = @correo AND contraseña = @contraseña";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@correo", correo);
                    comando.Parameters.AddWithValue("@contraseña", contraseña);

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        int nivel = Convert.ToInt32(reader["nivel"]);
                        string nUsuario = " ";
                        if (nivel == 1) nUsuario = "empleado"; 
                        if (nivel == 2) nUsuario = "encargado";
                        if (nivel == 3) nUsuario = "administrador";

                        this.Hide();

                        PPrincipal pp = new PPrincipal(nUsuario);
                        pp.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error al conectar " + ex.Message);
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CambioContraseña ccontraseña = new CambioContraseña();
            ccontraseña.Show();
            Hide();
        }

        private void Registrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
            Hide();
        }
    }
}
