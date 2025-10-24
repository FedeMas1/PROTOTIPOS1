using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROTOTIPOS1
{
    public partial class Proveedores : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Proveedores()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            cbEstado.Checked = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBCUIT.Text) || string.IsNullOrEmpty(txtBRazonS.Text) || string.IsNullOrEmpty(txtBCalle.Text) || string.IsNullOrEmpty(txtBNCalle.Text) || string.IsNullOrEmpty(txtBProvincia.Text) || string.IsNullOrEmpty(txtBLocalidad.Text) || string.IsNullOrEmpty(txtBRubros.Text))
            {
                MessageBox.Show("Los datos requeridos no estan completos");
                return;
            }


            string duplicado = ProveedorExiste(txtBCUIT.Text, txtBRazonS.Text);

            if (duplicado == "CUIT")
            {
                DialogResult actualizar = MessageBox.Show("Ya existe un proveedor con este CUIT. ¿Desea actualizar sus datos?", "Proveedor existente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (actualizar == DialogResult.Yes)
                {
                    ActualizarProveedor();
                    return;
                }
                else return;
            }
            else
            {
                DialogResult confirmar = MessageBox.Show("¿Desea guardar el nuevo proveedor?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmar == DialogResult.Yes)
                {
                    GuardarProveedor();
                }
            }
        }


        private string ProveedorExiste(string cuit, string razonSocial)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT CUIT, Razon_Social FROM Proveedores WHERE CUIT = @CUIT OR Razon_Social = @Razon_Social";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@CUIT", cuit);
                    cmd.Parameters.AddWithValue("@Razon_Social", razonSocial);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string cuitExistente = dr["CUIT"].ToString();
                            string razonExistente = dr["Razon_Social"].ToString();

                            if (cuitExistente == cuit) return "CUIT";
                            else if (razonExistente.Equals(razonSocial, StringComparison.OrdinalIgnoreCase))
                            {
                                return "RazonSocial";
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void GuardarProveedor()
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "INSERT INTO Proveedores " +
                    "(CUIT, Razon_Social, Contacto, Nro_Telefono, Calle, Nro_Calle, Codigo_Postal, Provincia, Localidad, Piso, Departamento, Rubros, Marcas, Estado)" +
                    "VALUES (@CUIT, @RazonSocial, @Contacto, @NroTelefono, @Calle, @NroCalle, @CodigoPostal, @Provincia, @Localidad, @Piso, @Departamento, @Rubros, @Marcas, @Estado)";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    AsignarValores(cmd);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Proveedor guardado correctamente");
                LimpiarCampos();
            }
        }

        private void ActualizarProveedor()
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "UPDATE Proveedores SET" +
                    " Razon_Social = @RazonSocial, Contacto = @Contacto, Nro_Telefono = @NroTelefono, Calle = @Calle, Nro_Calle = @NroCalle, Codigo_Postal = @CodigoPostal, Provincia= @Provincia , Localidad = @Localidad, Piso = @Piso, Departamento = @Departamento , Rubros = @Rubros, Marcas = @Marcas, Estado = @Estado " +
                    "WHERE CUIT = @CUIT";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    AsignarValores(cmd);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Proveedor modificado correctamente");
                LimpiarCampos();
                cbEstado.Checked = false;
            }
        }

       

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBCUIT.Text))
            {
                MessageBox.Show("Se debe cargar un numero de CUIT");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Proveedores WHERE CUIT = @CUIT";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CUIT", txtBCUIT.Text.Trim());
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtBRazonS.Text = reader["Razon_Social"].ToString();
                        txtBContacto.Text = reader["Contacto"].ToString();
                        txtBNumeroT.Text = reader["Nro_Telefono"].ToString();
                        txtBCalle.Text = reader["Calle"].ToString();
                        txtBNCalle.Text = reader["Nro_Calle"].ToString();
                        txtBCPostal.Text = reader["Codigo_Postal"].ToString();
                        txtBProvincia.Text = reader["Provincia"].ToString();
                        txtBLocalidad.Text = reader["Localidad"].ToString();
                        txtBPiso.Text = reader["Piso"].ToString();
                        txtBDepartamento.Text = reader["Departamento"].ToString();
                        txtBRubros.Text = reader["Rubros"].ToString();
                        txtBMarcas.Text = reader["Marcas"].ToString();
                        cbEstado.Checked = Convert.ToBoolean(reader["Estado"]);
                    }
                    else
                    {
                        MessageBox.Show("No se encontro un proveedor con ese numero");
                        LimpiarCampos();
                    }
                }
            }
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el CUIT del proveedor que desea modificar", "Modificar proveedor", "");
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("No se ingresó ningun numero");
                return;
            }

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "SELECT * FROM Proveedores WHERE CUIT = @CUIT";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@CUIT", input);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtBCUIT.Text = reader["CUIT"].ToString();
                        txtBRazonS.Text = reader["Razon_Social"].ToString();
                        txtBContacto.Text = reader["Contacto"].ToString();
                        txtBNumeroT.Text = reader["Nro_Telefono"].ToString();
                        txtBCalle.Text = reader["Calle"].ToString();
                        txtBNCalle.Text = reader["Nro_Calle"].ToString();
                        txtBCPostal.Text = reader["Codigo_Postal"].ToString();
                        txtBProvincia.Text = reader["Provincia"].ToString();
                        txtBLocalidad.Text = reader["Localidad"].ToString();
                        txtBPiso.Text = reader["Piso"].ToString();
                        txtBDepartamento.Text = reader["Departamento"].ToString();
                        txtBRubros.Text = reader["Rubros"].ToString();
                        txtBMarcas.Text = reader["Marcas"].ToString();
                        cbEstado.Checked = Convert.ToBoolean(reader["Estado"]);
                    }
                    else
                    {
                        MessageBox.Show("No se encontro un proveedor con ese numero");
                        LimpiarCampos();
                    }
                }
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBCUIT.Text))
            {
                MessageBox.Show("Se debe cargar un proveedor antes de eliminarlo");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Estas seguro que deseas eliminar este proveedor?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                string query = "DELETE FROM Proveedores WHERE CUIT = @CUIT";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@CUIT", txtBCUIT.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Proveedor eliminado correctmente");
                LimpiarCampos();
            }
        }

        private void AsignarValores(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@CUIT", txtBCUIT.Text.Trim());
            cmd.Parameters.AddWithValue("@RazonSocial", txtBRazonS.Text.Trim());
            cmd.Parameters.AddWithValue("@Contacto", string.IsNullOrWhiteSpace(txtBContacto.Text) ? (object)DBNull.Value : txtBContacto.Text.Trim());
            cmd.Parameters.AddWithValue("@NroTelefono", string.IsNullOrWhiteSpace(txtBNumeroT.Text) ? (object)DBNull.Value : txtBNumeroT.Text.Trim());
            cmd.Parameters.AddWithValue("@Calle", txtBCalle.Text.Trim());
            cmd.Parameters.AddWithValue("@NroCalle", int.Parse(txtBNCalle.Text.Trim()));
            cmd.Parameters.AddWithValue("@CodigoPostal", string.IsNullOrWhiteSpace(txtBCPostal.Text) ? (object)DBNull.Value : txtBCPostal.Text.Trim());
            cmd.Parameters.AddWithValue("@Provincia", txtBProvincia.Text.Trim());
            cmd.Parameters.AddWithValue("@Localidad", txtBLocalidad.Text.Trim());
            cmd.Parameters.AddWithValue("@Piso", string.IsNullOrWhiteSpace(txtBPiso.Text) ? (object)DBNull.Value : txtBPiso.Text.Trim());
            cmd.Parameters.AddWithValue("@Departamento", string.IsNullOrWhiteSpace(txtBDepartamento.Text) ? (object)DBNull.Value : txtBDepartamento.Text.Trim());
            cmd.Parameters.AddWithValue("@Rubros", txtBRubros.Text.Trim());
            cmd.Parameters.AddWithValue("@Marcas", string.IsNullOrWhiteSpace(txtBMarcas.Text) ? (object)DBNull.Value : txtBMarcas.Text.Trim());
            cmd.Parameters.AddWithValue("@Estado", cbEstado.Checked ? 1 : 0);
        }

        private void LimpiarCampos()
        {
            txtBCUIT.Text = null;
            txtBRazonS.Text = null;
            txtBContacto.Text = null;
            txtBNumeroT.Text = null;
            txtBCalle.Text = null;
            txtBNCalle.Text = null;
            txtBProvincia.Text = null;
            txtBLocalidad.Text = null;
            txtBDepartamento.Text = null;
            txtBPiso.Text = null;
            txtBCPostal.Text = null;
            txtBMarcas.Text = null;
            txtBRubros.Text = null;
            cbEstado.Checked = false;
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
    

