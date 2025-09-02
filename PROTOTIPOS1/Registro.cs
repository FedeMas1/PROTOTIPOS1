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

namespace PROTOTIPOS1
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void bttnBack_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string apellido = textBox5.Text;
            string nombre_usuario = textBox2.Text;
            string mail = textBox4.Text;
            string contraseña = textBox3.Text;
            string r_contraseña = textBox6.Text;
            
            if(string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(nombre_usuario) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(r_contraseña))
            {
                MessageBox.Show("Los datos no estan completos");
            }

            if (!mail.Contains("@"))
            {
                MessageBox.Show("El mail no tiene @");
                return;
            }
            ;

            if (contraseña.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener por lo menos 8 caracteres");
                return;
            }
            ;

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

            // conexion a sql

            string cadena_Conexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";

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
                    SqlCommand insertar = new SqlCommand(@"INSERT INTO Usuarios (nombre_Usuario, contraseña, email, nombre, apellido, nivel) 
                    VALUES (@nombre_Usuario, @contraseña, @mail, @nombre, @apellido, @nivel)", conexion);

                    insertar.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
                    insertar.Parameters.AddWithValue("@contraseña", contraseñaHasheada);
                    insertar.Parameters.AddWithValue("@mail", mail);
                    insertar.Parameters.AddWithValue("@nombre", nombre);
                    insertar.Parameters.AddWithValue("@apellido", apellido);
                    insertar.Parameters.AddWithValue("@nivel", 1);


                    int filas = insertar.ExecuteNonQuery();

                    if (filas > 0)
                    {
                        MessageBox.Show("Usuario registrado correctamente");
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


    private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
    }
}
