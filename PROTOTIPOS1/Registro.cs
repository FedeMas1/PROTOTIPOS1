using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Web;

namespace PROTOTIPOS1
{
    public partial class Registro : Form
    {
        string idioma = "Español";
        private string ver = "Oculto";
        public Registro()
        {
            InitializeComponent();
            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();
            
            toolTip.SetToolTip(bttnTraducir, "Cmbiar el idioma.");
            toolTip.SetToolTip(button1, "Registrar nueva cuenta.");
            toolTip.SetToolTip(bttnBack, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(textBox4, "Ej: emanuel@gmail.com.");
            toolTip.SetToolTip(bttnVContraseña, "Ver la contraseña oculta");
            toolTip.SetToolTip(bttnVRContraseña, "Ver la contraseña oculta");
        }

        private void bttnBack_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            Hide();
        }

        private bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("El campo de correo no puede estar vacío.");
                return false;
            }

            if (!email.Contains("@"))
            {
                MessageBox.Show("El correo debe contener el símbolo '@'.");
                return false;
            }

            string[] partes = email.Split('@');
            if (partes.Length != 2)
            {
                MessageBox.Show("El formato del correo no es válido.");
                return false;
            }

            string antes = partes[0];
            string despues = partes[1];

            if (string.IsNullOrWhiteSpace(antes))
            {
                MessageBox.Show("Debe ingresar un nombre antes del '@'.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(despues))
            {
                MessageBox.Show("Debe ingresar un dominio después del '@'.");
                return false;
            }

            if (despues == "gmail.com" || despues == "hotmail.com" || despues == "outlook.com")
            {
                
                return true;
            }
            else
            {
                MessageBox.Show("Solo se permiten correos de Gmail, Hotmail o Outlook.");
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string apellido = textBox5.Text;
            string nombre_usuario = textBox2.Text;
            string mail = textBox4.Text;
            string contraseña = textBox3.Text;
            string r_contraseña = textBox6.Text;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(nombre_usuario) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(r_contraseña))
            {
                MessageBox.Show("Los datos no estan completos");
            }

            if (contraseña.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener por lo menos 8 caracteres");
                return;
            }

            if (!contraseña.Any(char.IsUpper))
            {
                MessageBox.Show("La contraseña no contiene mayusculas");
                return;
            }

            if (!contraseña.Any(char.IsDigit))
            {
                MessageBox.Show("La contraseña no contiene ningun numero");
                return;
            }

            if (contraseña != r_contraseña)
            {
                MessageBox.Show("La contraseña ingresada no coincide");
                return;
            }




            string contraseñaHasheada = Hasheo.generarHash(contraseña);

            bool mailVerificado = EsEmailValido(mail);

            if (mailVerificado == true)
            {
                string cadena_Conexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
                var gestor = new GestorDV(cadena_Conexion);

            using (SqlConnection conexion = new SqlConnection(cadena_Conexion))
            {
                try
                {
                    conexion.Open();
                    SqlCommand verificarUsuario = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE nombre_Usuario = @nombre_usuario OR email = @mail", conexion);
                    verificarUsuario.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
                    verificarUsuario.Parameters.AddWithValue("@mail", mail);

                    int existe = (int)verificarUsuario.ExecuteScalar();
                    if (existe > 0)
                    {
                        MessageBox.Show("Ya existe un usuario con ese nombre o correo");
                        return;
                    }

                        string cadenaDVH = nombre_usuario + contraseñaHasheada + mail + nombre + apellido + "1";
                        string dvh = Hasheo.generarHash(cadenaDVH);

                        SqlCommand insertar = new SqlCommand(@"
                        INSERT INTO Usuarios (nombre_Usuario, contraseña, email, nombre, apellido, nivel, dvh) 
                        VALUES (@nombre_Usuario, @contraseña, @mail, @nombre, @apellido, @nivel, @dvh);
                        SELECT SCOPE_IDENTITY();", conexion);

                        

                        insertar.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
                        insertar.Parameters.AddWithValue("@contraseña", contraseñaHasheada);
                        insertar.Parameters.AddWithValue("@mail", mail);
                        insertar.Parameters.AddWithValue("@nombre", nombre);
                        insertar.Parameters.AddWithValue("@apellido", apellido);
                        insertar.Parameters.AddWithValue("@nivel", 1);
                        insertar.Parameters.AddWithValue("@dvh", dvh);



                        int idGenerado = Convert.ToInt32(insertar.ExecuteScalar());

                        gestor.ActualizarDVV();

                        if (idGenerado > 0)
                    {
                        MessageBox.Show("Usuario registrado correctamente");
                        Bitacora bit = new Bitacora();
                        bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, $"Registró un nuevo usuario: {nombre_usuario}");
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error al registrarse");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexion " + ex.Message);
                }
            }
            
           }
            }
       
    private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void bttnTraducir_Click(object sender, EventArgs e)
        {
            

            if (idioma == "Español")
            {

                bttnBack.Text = "Back";
                lblNombre.Text = "Name";
                lblApellido.Text = "Surname";
                lblNombreUsuario.Text = "User name";
                lblMail.Text = "Email";
                lblContraseña.Text = "Password";
                lblRepetirContraseña.Text = "Repeat password";
                button1.Text = "Sign up";
                lblTitulo.Text = "User Registration";
                lblArroba.Text = "Must contain a @gmail.com, @hotmail.com or @outlook.com";
                lblParametros.Text = "Must contain at least 8 characters, a capital letter and a number";
                bttnTraducir.Text = "Translate";
                bttnVContraseña.Text = "See";
                bttnVRContraseña.Text = "See";

                idioma = "Ingles";
            }
            else
            {
                bttnBack.Text = "Volver";
                lblNombre.Text = "Nombre";
                lblApellido.Text = "Apellido";
                lblNombreUsuario.Text = "Nombre de usuario";
                lblMail.Text = "Mail";
                lblContraseña.Text = "Contraseña";
                lblRepetirContraseña.Text = "Repetir contraseña";
                button1.Text = "Registrarse";
                lblTitulo.Text = "Registro de Usuario";
                lblArroba.Text = "Debe contener un @gmail.com, @hotmail.com o @outlook.com";
                lblParametros.Text = "Debe contener al menos 8 caracteres, una mayuscula y un numero";
                bttnTraducir.Text = "Traducir";
                bttnVContraseña.Text = "Ver";
                bttnVRContraseña.Text = "Ver";

                idioma = "Español";
            }

          
        }


        private void bttnVContraseña_Click(object sender, EventArgs e)
        {
            if (ver == "Oculto")
            {
                textBox3.UseSystemPasswordChar = false;
                ver = "Mostrar";
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
                ver = "Oculto";
            }
        }

        private void bttnVRContraseña_Click(object sender, EventArgs e)
        {
            if (ver == "Oculto")
            {
                textBox6.UseSystemPasswordChar = false;
                ver = "Mostrar";
            }
            else
            {
                textBox6.UseSystemPasswordChar = true;
                ver = "Oculto";
            }
        }
    }
}
