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
using Microsoft.VisualBasic;

namespace PROTOTIPOS1
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnAgregar, "Buscar un producto de rubros a traves de su descripcion en el textbox");
            toolTip.SetToolTip(bttnModificar, "Buscar un producto por la descripcion, permitira modificar y eliminar el producto");
            toolTip.SetToolTip(bttnEliminar, "Elimina un producto de la base de datos, siempre y cuando el producto ya este cargado en el formulario");
            toolTip.SetToolTip(bttnGuardar, "Guarda el producto en la base de datos, y guarda las modificaciones de ese producto");
            toolTip.SetToolTip(bttnCSesion, "Cierra la sesion del usuario logueado actualmente");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            
            
            //cmb descripcion
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT Descripcion FROM Stock";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    cmbDescripcion.Items.Clear();
                    foreach(DataRow fila in tabla.Rows)
                    {
                        cmbDescripcion.Items.Add(fila["Descripcion"].ToString());
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }

            //cmb rubros
            string consulta = "SELECT id_Rubro, Descripcion FROM Rubros";
            using(SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(consulta, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbRubros.DisplayMember = "Descripcion";
                cmbRubros.ValueMember = "id_Rubro";
                cmbRubros.DataSource = dt;
                cmbRubros.SelectedIndex = -1;
            }
        }

        public void limpiarValores()
        {
            cmbDescripcion.Text = "";
            cmbRubros.SelectedIndex = -1;
            lblCProducto.Text = "XXXX";
            txtbMarca.Clear();
            txtbUMVenta.Clear();
            txtBUMCompra.Clear();
            txtBCuit.Clear();
            txtbCantidad.Clear();
            txtBPVenta.Clear();
            txtbMarca.Clear();
            txtBPPedido.Clear();
            txtBCMaxima.Clear();
            txtBEProducto.Clear();
            dtpFIngreso.Value = DateTime.Now;
            dtpFVencimiento.Value = DateTime.Now;
            checkbActivo.Checked = false;
        }

        private bool esModificacion = false;
        private int idProductoActual = -1;

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string productoBuscado = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese la descripcion del producto", "Buscar producto", "");

            if (string.IsNullOrEmpty(productoBuscado)) return;

            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT * FROM Stock WHERE Descripcion = @Descripcion";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", productoBuscado);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        idProductoActual = Convert.ToInt32(reader["codigo_Producto"]);
                        esModificacion = true;

                        cmbDescripcion.Text = reader["Descripcion"].ToString();
                        cmbRubros.SelectedValue = reader["id_Rubro"];
                        lblCProducto.Text = reader["codigo_Producto"].ToString();
                        txtbUMVenta.Text = reader["UM_venta"].ToString();
                        txtBUMCompra.Text = reader["UM_compra"].ToString();
                        txtBCuit.Text = reader["cod_Proveedor"].ToString();
                        txtbCantidad.Text = reader["cantidad"].ToString();
                        txtBPVenta.Text = reader["precio_venta"].ToString();
                        txtBPVenta.Text = reader["precio_compra"].ToString();
                        txtbMarca.Text = reader["marca"].ToString();
                        txtBPPedido.Text = reader["punto_pedido"].ToString();
                        txtBCMaxima.Text = reader["cantidad_maxima"].ToString();
                        txtBEProducto.Text = reader["estado"].ToString();
                        checkbActivo.Checked = Convert.ToBoolean(reader["activo"]);

                        if (reader["fecha_ingreso"]!= DBNull.Value)
                        {
                            dtpFIngreso.Value = Convert.ToDateTime(reader["fecha_ingreso"]);
                        }

                        if (reader["fecha_vencimiento"] != DBNull.Value)
                        {
                            dtpFVencimiento.Value = Convert.ToDateTime(reader["fecha_vencimiento"]);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un producto con esa descripcion");
                    }
                }
            }
        }

        private void bttnAgregar_Click(object sender, EventArgs e)
        {
            string nuevoProducto = cmbDescripcion.Text.Trim();
            int rubro = Convert.ToInt32(cmbRubros.SelectedValue);
            string UMVenta = txtbUMVenta.Text;
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
                    return;
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
                string query = "INSERT INTO Stock (Descripcion, id_Rubro, UM_venta, marca, activo) VALUES (@Descripcion, @id_Rubro, @UM_Venta, @marca, @activo)";

                using(SqlConnection conexion = new SqlConnection(connectionString))
                {
                    try
                    {
                        conexion.Open();
                        SqlCommand comando = new SqlCommand(query, conexion);
                        comando.Parameters.AddWithValue("@Descripcion", nuevoProducto);        
                        comando.Parameters.AddWithValue("@id_Rubro", rubro);
                        comando.Parameters.AddWithValue("@UM_venta", UMVenta);
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

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";

            string descripcion = cmbDescripcion.Text.Trim();
            int? idRubro = cmbRubros.SelectedValue != null ? Convert.ToInt32(cmbRubros.SelectedValue) : (int?)null;
            string umCompra = txtBUMCompra.Text.Trim();
            string umVenta = txtbUMVenta.Text.Trim();
            string marca = txtbMarca.Text.Trim();
            string codProveedor = txtBCuit.Text.Trim();
            string estado = txtBEProducto.Text.Trim();
            decimal precioCompra = 0, precioVenta = 0;
            int cantidad = 0, puntoPedido = 0, cantidadMaxima = 0;
            DateTime fechaIngreso = dtpFIngreso.Value;
            DateTime fechaVencimiento = dtpFVencimiento.Value;
            bool activo = checkbActivo.Checked;

            // Intentar convertir los campos numéricos (si no se cargaron, usar 0)
            decimal.TryParse(lblPCompra.Text, out precioCompra);
            decimal.TryParse(txtBPVenta.Text, out precioVenta);
            int.TryParse(txtbCantidad.Text, out cantidad);
            int.TryParse(txtBPPedido.Text, out puntoPedido);
            int.TryParse(txtBCMaxima.Text, out cantidadMaxima);

            if (string.IsNullOrEmpty(descripcion) || idRubro == null || string.IsNullOrEmpty(umVenta) || string.IsNullOrEmpty(marca))
            {
                MessageBox.Show("Los campos Descripción, Rubro, Unidad de Medida de Venta y Marca son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                
                string queryExistencia = "SELECT COUNT(*) FROM Stock WHERE Descripcion = @Descripcion";
                SqlCommand cmdExistencia = new SqlCommand(queryExistencia, conexion);
                cmdExistencia.Parameters.AddWithValue("@Descripcion", descripcion);
                int existe = (int)cmdExistencia.ExecuteScalar();

                string query;

                if (existe > 0)
                {
                    // Modificar
                    query = @"UPDATE Stock SET 
                        id_Rubro = @idRubro,
                        UM_compra = @UMCompra,
                        UM_venta = @UMVenta,
                        cod_proveedor = @CodProveedor,
                        cantidad = @Cantidad,
                        precio_venta = @PrecioVenta,
                        precio_compra = @PrecioCompra,
                        marca = @Marca,
                        fecha_ingreso = @FechaIngreso,
                        fecha_vencimiento = @FechaVencimiento,
                        punto_pedido = @PuntoPedido,
                        cantidad_maxima = @CantidadMaxima,
                        estado = @Estado,
                        activo = @Activo
                      WHERE Descripcion = @Descripcion";

                    Bitacora bit = new Bitacora();
                    bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, "Modificó un producto del stock");
                    MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Guardar
                    query = @"INSERT INTO Stock 
                    (Descripcion, id_Rubro, UM_compra, UM_venta, cod_proveedor, cantidad, precio_venta, precio_compra, marca, fecha_ingreso, fecha_vencimiento, punto_pedido, cantidad_maxima, estado, activo)
                    VALUES
                    (@Descripcion, @idRubro, @UMCompra, @UMVenta, @CodProveedor, @Cantidad, @PrecioVenta, @PrecioCompra, @Marca, @FechaIngreso, @FechaVencimiento, @PuntoPedido, @CantidadMaxima, @Estado, @Activo)";

                    Bitacora bit = new Bitacora();
                    bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, $"Agregó un producto del stock {descripcion}");
                    MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@idRubro", idRubro);
                    cmd.Parameters.AddWithValue("@UMCompra", (object)umCompra ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UMVenta", umVenta);
                    cmd.Parameters.AddWithValue("@CodProveedor", (object)codProveedor ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                    cmd.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                    cmd.Parameters.AddWithValue("@Marca", marca);
                    cmd.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);
                    cmd.Parameters.AddWithValue("@FechaVencimiento", fechaVencimiento);
                    cmd.Parameters.AddWithValue("@PuntoPedido", puntoPedido);
                    cmd.Parameters.AddWithValue("@CantidadMaxima", cantidadMaxima);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@Activo", activo);

                    cmd.ExecuteNonQuery();
                }
            }

            limpiarValores();

        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            if(!esModificacion || idProductoActual <= 0)
            {
                MessageBox.Show("Primero cargue un producto");
                return;
            }
            DialogResult result = MessageBox.Show("¿Seguro que desea eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();
                    string delete = "DELETE FROM Stock WHERE codigo_Producto = @id";
                    using (SqlCommand command = new SqlCommand(delete, conexion))
                    {
                        command.Parameters.AddWithValue("@id", idProductoActual);
                        command.ExecuteNonQuery();
                    }
                    Bitacora bit = new Bitacora();
                    bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, "Eliminó un producto del stock");
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
