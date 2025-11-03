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
    public partial class CambioContraseña : Form
    {
        private string ver = "Oculto";
        public CambioContraseña()
        {
            InitializeComponent();
            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip tooltip = new ToolTip();

            
            tooltip.SetToolTip(button1, "Guardar el cambio de la contraseña");
            tooltip.SetToolTip(bttnBack, "Ir al formulario anterior");
            tooltip.SetToolTip(bttnVNContraseña, "Ver la contraseña oculta");
            tooltip.SetToolTip(bttnVRContraseña, "Ver la contraseña oculta");
        }

        private void bttnBack_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtBUsuario.Text;
            string mail = txtBMail.Text;
            string nContraseña = txtBNContraseña.Text;
            string rContraseña = txtBRContraseña.Text;

            if(string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(nContraseña) || string.IsNullOrEmpty(rContraseña))
            {
                MessageBox.Show("algun dato esta incompleto");
            }

            if (!mail.Contains("@"))
            {
                MessageBox.Show("El mail no tiene @");
                return;
            }
            ;

            if (nContraseña.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener por lo menos 8 caracteres");
                return;
            }
            ;

            if (!nContraseña.Any(char.IsUpper))
            {
                MessageBox.Show("La contraseña no contiene mayusculas");
                return;
            }

            if (!nContraseña.Any(char.IsDigit))
            {
                MessageBox.Show("La contraseña no contiene ningun numero");
                return;
            }

            if (nContraseña != rContraseña)
            {
                MessageBox.Show("La contraseña ingresada no coincide");
                return;
            }

            if (nContraseña.Length >= 8 && nContraseña.Any(char.IsUpper) && nContraseña.Any(char.IsDigit) && nContraseña == rContraseña)
            {
                MessageBox.Show("Las contraseñas coinciden");
            }

            string contraseñaHasheada = Hasheo.generarHash(nContraseña);
            //conexion a la bdd

            string cadena_Conexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            using (SqlConnection conexion = new SqlConnection(cadena_Conexion))
            {
                try
                {
                    conexion.Open();
                    string consulta = @"SELECT COUNT(*) FROM Usuarios WHERE nombre_Usuario = @usuario AND email = @mail";
                    SqlCommand verificar = new SqlCommand(consulta, conexion);
                    verificar.Parameters.AddWithValue("@usuario", usuario);
                    verificar.Parameters.AddWithValue("@mail", mail);

                    int existe = (int)verificar.ExecuteScalar();
                    if (existe == 0) MessageBox.Show("Usuario o mail incorrectos");

                    string actualizar = @"UPDATE Usuarios SET contraseña = @nContraseña WHERE nombre_Usuario = @usuario AND email = @mail";
                    SqlCommand comando = new SqlCommand(actualizar, conexion);
                    comando.Parameters.AddWithValue("@nContraseña", contraseñaHasheada);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@mail", mail);

                    int filas = comando.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        MessageBox.Show("Contraseña modificada correctamente");

                        Bitacora bit = new Bitacora();
                        bit.RegistrarEvento(0, usuario, "Cambió su contraseña sin iniciar sesión");

                        txtBUsuario.Clear();
                        txtBMail.Clear();
                        txtBNContraseña.Clear();
                        txtBRContraseña.Clear();

                    }
                    else MessageBox.Show("Error al modificar la contraseña");
                } catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }

        }

        private void bttnVNContraseña_Click(object sender, EventArgs e)
        {
            if (ver == "Oculto")
            {
                txtBNContraseña.UseSystemPasswordChar = false;
                ver = "Mostrar";
            }
            else
            {
                txtBNContraseña.UseSystemPasswordChar = true;
                ver = "Oculto";
            }
        }

        private void bttnVRContraseña_Click(object sender, EventArgs e)
        {
            if (ver == "Oculto")
            {
                txtBRContraseña.UseSystemPasswordChar = false;
                ver = "Mostrar";
            }
            else
            {
                txtBRContraseña.UseSystemPasswordChar = true;
                ver = "Oculto";
            }
        }
    }
}
