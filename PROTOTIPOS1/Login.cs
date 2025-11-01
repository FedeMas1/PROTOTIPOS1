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
        private static int intentosFallidos = 0;
        private static DateTime? bloqueoHasta = null;

        private string ver = "Oculto";
        public Login()
        {
            InitializeComponent();
            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            this.Icon = new Icon(@"C:\imagenes\Imagen_Icon.ico");

            ToolTip tooltip = new ToolTip();

            tooltip.SetToolTip(linkLabel1, "Haz clic aquí para restablecer tu contraseña.");
            tooltip.SetToolTip(Registrarse, "Crea una cuenta nueva.");
            tooltip.SetToolTip(button1, "Haz clic para ingresar a tu cuenta.");
            tooltip.SetToolTip(txtbCorreo, "Ej: emanuel@gmail.com");
            tooltip.SetToolTip(bttnVer, "Ver la contraseña oculta");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (bloqueoHasta != null && DateTime.Now < bloqueoHasta)
            {
                TimeSpan tiempoRestante = bloqueoHasta.Value - DateTime.Now;
                MessageBox.Show($"Demasiados intentos fallidos. Intente nuevamente en {tiempoRestante.Minutes} minutos y {tiempoRestante.Seconds} segundos.");
                return;
            }

            string correo = txtbCorreo.Text;
            string contraseña = txtbContraseña.Text;

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Faltan ingresar datos");
                return;
            }

            string contraseñaHasheada = Hasheo.generarHash(contraseña);

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
                        // Resetear intentos al ingresar correctamente
                        intentosFallidos = 0;
                        bloqueoHasta = null;

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
                        intentosFallidos++;

                        if (intentosFallidos >= 3)
                        {
                            bloqueoHasta = DateTime.Now.AddMinutes(3);
                            MessageBox.Show("Usuario o contraseña incorrectos. Ha alcanzado el máximo de intentos, intente nuevamente en 3 minutos.");
                        }
                        else
                        {
                            MessageBox.Show($"Usuario o contraseña incorrectos. Intento {intentosFallidos} de 3.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al conectar " + ex.Message);
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

        private void bttnVer_Click(object sender, EventArgs e)
        {
            if (ver == "Oculto")
            {
                txtbContraseña.UseSystemPasswordChar = false;
                ver = "Mostrar";
            }
            else
            {
                txtbContraseña.UseSystemPasswordChar = true;
                ver = "Oculto";
            }
        }

    
    }
}
