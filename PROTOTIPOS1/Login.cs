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
            ToolTip tooltip = new ToolTip();

            tooltip.SetToolTip(linkLabel1, "Haz clic aquí para restablecer tu contraseña.");
            tooltip.SetToolTip(Registrarse, "Crea una cuenta nueva.");
            tooltip.SetToolTip(button1, "Haz clic para ingresar a tu cuenta.");
            tooltip.SetToolTip(txtbCorreo, "Ej: emanuel@gmail.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string correo = txtbCorreo.Text;
            string contraseña = txtbContraseña.Text;

            string contraseñaHasheada = Hasheo.generarHash(contraseña);

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
                    string consulta = @"SELECT id_Usuario, email, nivel FROM Usuarios WHERE email = @correo AND contraseña = @contraseña";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@correo", correo);
                    comando.Parameters.AddWithValue("@contraseña", contraseñaHasheada);

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        int id_Usuario = Convert.ToInt32(reader["id_Usuario"]);
                        string email = reader["email"].ToString();
                        int nivel = Convert.ToInt32(reader["nivel"]);

                        Sesion.CargarSesion(id_Usuario, email, nivel);

                        Modulos mod = new Modulos();
                        mod.Show();
                        Hide();
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
