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
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnEliminar, "Eliminar un bien de uso.");
            toolTip.SetToolTip(bttnModificar, "Modificar un bien de uso.");
            toolTip.SetToolTip(bttnGuardar, "Guardar un bien de uso.");
            toolTip.SetToolTip(button5, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            //cmb descripcion
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT Descripcion FROM Inventario";
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    cmbDescripcion.Items.Clear();
                    foreach (DataRow fila in tabla.Rows)
                    {
                        cmbDescripcion.Items.Add(fila["Descripcion"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }

            //cmb rubros   
            string consulta = "SELECT id_Rubro, Descripcion FROM Rubros";
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(consulta, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbRubros.DisplayMember = "Descripcion";
                cmbRubros.ValueMember = "id_Rubro";
                cmbRubros.DataSource = dt;
                cmbRubros.SelectedIndex = -1;
            }

            if(Sesion.nivel == 1)
            {
                bttnModificar.Enabled = false;
                bttnEliminar.Enabled = false;
                bttnGuardar.Enabled = false;
            }

        }

        public void limpiarValores()
        {
            cmbDescripcion.Text = "";
            cmbRubros.SelectedIndex = -1;
            txtbMarca.Clear();
            txtbCProveedor.Clear();
            txtBCantidadA.Clear();
            txtBPPedido.Clear();
            txtbCantidadM.Clear();
            dtpFechaIngreso.Value = DateTime.Now;
            checkbActivo.Checked = false;
            lblCProducto.Text = "XXXX";
        }

        private void bttnAgregar_Click(object sender, EventArgs e)
        {
            string nuevoProducto = cmbDescripcion.Text.Trim();
            int rubro = Convert.ToInt32(cmbRubros.SelectedValue);
            string marca = txtbMarca.Text;
            bool activo = checkbActivo.Checked;

            if (string.IsNullOrEmpty(nuevoProducto))
            {
                MessageBox.Show("La descripcion del producto esta vacía");
                return;
            }

            bool existe = false;

            foreach (var item in cmbDescripcion.Items)
            {
                if (item.ToString().Trim().ToLower() == nuevoProducto.ToLower())
                {
                    existe = true;
                    break;
                }
            }

            if (existe)
            {
                MessageBox.Show("El producto ya existe");
                return;
            }

            else
            {

                string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
                string query = "INSERT INTO Inventario (Descripcion, id_Rubro, marca, activo) VALUES (@Descripcion, @id_Rubro, @marca, @activo)";

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    try
                    {
                        conexion.Open();
                        SqlCommand comando = new SqlCommand(query, conexion);
                        comando.Parameters.AddWithValue("@Descripcion", nuevoProducto);
                        comando.Parameters.AddWithValue("@id_Rubro", rubro);
                        comando.Parameters.AddWithValue("@marca", marca);
                        comando.Parameters.AddWithValue("@activo", activo);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Producto agregado correctamente");
                        cmbDescripcion.Items.Add(nuevoProducto);


                        limpiarValores();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) MessageBox.Show("Ya existe un producto con ese nombre");
                        else MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";

            string descripcion = cmbDescripcion.Text.Trim();
            int? idRubro = cmbRubros.SelectedValue != null ? Convert.ToInt32(cmbRubros.SelectedValue) : (int?)null;
            string marca = txtbMarca.Text.Trim();
            string codProveedor = txtbCProveedor.Text.Trim();
            decimal precioCompra = 0;
            int cantidad = 0, puntoPedido = 0, cantidadMaxima = 0;
            DateTime fechaIngreso = dtpFechaIngreso.Value;
            bool activo = checkbActivo.Checked;

            // Intentar convertir los campos numéricos (si no se cargaron, usar 0)
            decimal.TryParse(lblPCompra.Text, out precioCompra);
            int.TryParse(txtBCantidadA.Text, out cantidad);
            int.TryParse(txtBPPedido.Text, out puntoPedido);
            int.TryParse(txtbCantidadM.Text, out cantidadMaxima);

            if (string.IsNullOrEmpty(descripcion) || idRubro == null || string.IsNullOrEmpty(marca))
            {
                MessageBox.Show("Los campos Descripción, Rubro, y Marca son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();


                string queryExistencia = "SELECT COUNT(*) FROM Inventario WHERE Descripcion = @Descripcion";
                SqlCommand cmdExistencia = new SqlCommand(queryExistencia, conexion);
                cmdExistencia.Parameters.AddWithValue("@Descripcion", descripcion);
                int existe = (int)cmdExistencia.ExecuteScalar();

                string query;

                if (existe > 0)
                {
                    // Modificar
                    query = @"UPDATE Inventario SET 
                        id_Rubro = @idRubro,
                        cod_proveedor = @CodProveedor,
                        cantidad = @Cantidad,
                        precio_compra = @PrecioCompra,
                        marca = @Marca,
                        fecha_ingreso = @FechaIngreso,
                        punto_pedido = @PuntoPedido,
                        cantidad_maxima = @CantidadMaxima,
                        activo = @Activo
                      WHERE Descripcion = @Descripcion";

                    Bitacora bit = new Bitacora();
                    bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, "Modificó un producto de inventario");
                    MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Guardar
                    query = @"INSERT INTO Inventario 
                    (Descripcion, id_Rubro, cod_proveedor, cantidad, precio_compra, marca, fecha_ingreso, punto_pedido, cantidad_maxima, activo)
                    VALUES
                    (@Descripcion, @idRubro, @CodProveedor, @Cantidad, @PrecioCompra, @Marca, @FechaIngreso, @PuntoPedido, @CantidadMaxima, @Activo)";

                    Bitacora bit = new Bitacora();
                    bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, $"Agregó un producto al inventario: {descripcion}");
                    MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@idRubro", idRubro);
                    cmd.Parameters.AddWithValue("@CodProveedor", (object)codProveedor ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);  
                    cmd.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                    cmd.Parameters.AddWithValue("@Marca", marca);
                    cmd.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);
                    cmd.Parameters.AddWithValue("@PuntoPedido", puntoPedido);
                    cmd.Parameters.AddWithValue("@CantidadMaxima", cantidadMaxima);
                    cmd.Parameters.AddWithValue("@Activo", activo);

                    cmd.ExecuteNonQuery();
                }
            }

            limpiarValores();

        }

        private bool esModificacion = false;
        private int idProductoActual = -1;
        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string productoBuscado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese la descripcion del producto", "Buscar producto", "");

            if (string.IsNullOrEmpty(productoBuscado)) return;

            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT * FROM Inventario WHERE Descripcion = @Descripcion";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", productoBuscado);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        idProductoActual = Convert.ToInt32(reader["cod_Producto"]);
                        esModificacion = true;

                        cmbDescripcion.Text = reader["Descripcion"].ToString();
                        cmbRubros.SelectedValue = reader["id_Rubro"];
                        txtbMarca.Text = reader["marca"].ToString();
                        txtBCantidadA.Text = reader["cantidad"].ToString();
                        lblCProducto.Text = reader["cod_Producto"].ToString();
                        txtbCProveedor.Text = reader["cod_proveedor"].ToString();
                        txtBCantidadA.Text = reader["cantidad"].ToString();
                        txtBPPedido.Text = reader["punto_pedido"].ToString();
                        txtbCantidadM.Text = reader["cantidad_maxima"].ToString();
                        checkbActivo.Checked = Convert.ToBoolean(reader["activo"]);

                        if (reader["fecha_ingreso"] != DBNull.Value)
                        {
                            dtpFechaIngreso.Value = Convert.ToDateTime(reader["fecha_ingreso"]);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un producto con esa descripcion");
                    }
                }
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            if (!esModificacion || idProductoActual <= 0)
            {
                MessageBox.Show("Primero cargue un producto");
                return;
            }
            DialogResult result = MessageBox.Show("¿Seguro que desea eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    string delete = "DELETE FROM Inventario WHERE cod_Producto = @id";
                    using (SqlCommand command = new SqlCommand(delete, conexion))
                    {
                        command.Parameters.AddWithValue("@id", idProductoActual);
                        command.ExecuteNonQuery();
                    }
                    Bitacora bit = new Bitacora();
                    bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, "Eliminó un producto del inventario");
                }
                MessageBox.Show("Producto eliminado correctamente");
                limpiarValores();
                esModificacion = false;
                idProductoActual = -1;
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
    }
}
