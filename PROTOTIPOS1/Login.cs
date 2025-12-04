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
            var gestor = new GestorDV(cadena_Conexion);

            // ✅ SOLO VERIFICA DVV — NO GENERA NADA
            if (!gestor.ExisteDVV())
            {
                MessageBox.Show("No existe DVV inicial. El sistema no fue inicializado.");
                return;
            }

            if (!gestor.VerificarDVV(out string dvvActual, out string dvvGuardado))
            {
                MessageBox.Show("ERROR DE INTEGRIDAD DEL SISTEMA (DVV). Contacte al administrador.");
                return;
            }

            using (SqlConnection conexion = new SqlConnection(cadena_Conexion))
            {
                try
                {
                    conexion.Open();

                    // ✅ PRIMERO BUSCAMOS SOLO POR EMAIL
                    string consulta = @"SELECT id_Usuario, contraseña, nivel, nombre_Usuario, email
                                FROM Usuarios WHERE email = @correo";

                    SqlCommand comando = new SqlCommand(consulta, conexion);
                    comando.Parameters.AddWithValue("@correo", correo);

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        int id_Usuario = Convert.ToInt32(reader["id_Usuario"]);
                        string contraseñaBD = reader["contraseña"].ToString();

                        reader.Close();

                        // ✅ PRIMERO DVH
                        if (!gestor.VerificarDVH(id_Usuario, out _, out _))
                        {
                            MessageBox.Show("ERROR DE INTEGRIDAD EN EL USUARIO (DVH). Acceso bloqueado.");
                            return;
                        }

                        // ✅ DESPUÉS CONTRASEÑA
                        if (contraseñaBD != contraseñaHasheada)
                        {
                            intentosFallidos++;
                            MessageBox.Show("Usuario o contraseña incorrectos.");
                            return;
                        }

                        // ✅ LOGIN CORRECTO
                        intentosFallidos = 0;
                        bloqueoHasta = null;

                        using (SqlCommand cmdDatos = new SqlCommand(
                            @"SELECT id_Usuario, email, nivel, nombre_Usuario 
                      FROM Usuarios WHERE id_Usuario = @id", conexion))
                        {
                            cmdDatos.Parameters.AddWithValue("@id", id_Usuario);

                            using (SqlDataReader r2 = cmdDatos.ExecuteReader())
                            {
                                r2.Read();

                                string emailBD = r2["email"].ToString();
                                int nivel = Convert.ToInt32(r2["nivel"]);
                                string nombreUsuario = r2["nombre_Usuario"].ToString();

                                Sesion.CargarSesion(id_Usuario, emailBD, nombreUsuario, nivel);
                            }
                        }

                        Bitacora bi = new Bitacora();
                        bi.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, "Inicio Sesion");

                        Modulos mod = new Modulos();
                        mod.Show();
                        Hide();
                    }
                    else
                    {
                        intentosFallidos++;
                        MessageBox.Show("Usuario o contraseña incorrectos.");
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
