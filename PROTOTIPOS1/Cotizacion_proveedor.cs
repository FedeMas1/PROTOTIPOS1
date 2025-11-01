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
    public partial class Cotizacion_proveedor : Form
    {
        private string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        private bool editando = false;
        private int idCotActual = 0;
        public Cotizacion_proveedor()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnModificar, "Modificar cotizacion.");
            toolTip.SetToolTip(bttnBPcot, "Buscar prdido de cotizacion.");
            toolTip.SetToolTip(bttnGuardar, "Guardar un proveedor.");
            toolTip.SetToolTip(bttnSiguiente, "Comparar proveedores.");
            toolTip.SetToolTip(button1, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Orden_de_compra oc = new Orden_de_compra();
            oc.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void Cotizacion_proveedor_Load(object sender, EventArgs e)
        {
            AgregarBotonEliminar();
        }

        private void bttnBCUIT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbCUIT.Text))
            {
                MessageBox.Show("Ingrese un CUIT para buscar el proveedor.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Razon_Social FROM Proveedores WHERE CUIT = @cuit";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@cuit", txtbCUIT.Text.Trim());

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        lblRSocial.Text = result.ToString();
                    }
                    else
                    {
                        lblRSocial.Text = "—";
                        MessageBox.Show("No se encontró ningún proveedor con ese CUIT.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar proveedor: " + ex.Message);
                }
            }
        }

        private void bttnBPcot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbNPcot.Text))
            {
                MessageBox.Show("Ingrese el número de pedido de cotización.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 1. Verificar que el pedido exista y esté aprobado en la tabla master
                    string queryMaster = "SELECT estado FROM Pcot_Master WHERE id_Pcot = @idPcot";
                    SqlCommand cmdMaster = new SqlCommand(queryMaster, connection);
                    cmdMaster.Parameters.AddWithValue("@idPcot", txtbNPcot.Text.Trim());
                    object estadoObj = cmdMaster.ExecuteScalar();

                    if (estadoObj == null)
                    {
                        MessageBox.Show("No se encontró ningún pedido con ese número.");
                        dataGridView1.DataSource = null;
                        return;
                    }

                    string estado = estadoObj.ToString();
                    if (estado != "Aprobado")
                    {
                        MessageBox.Show("El pedido aún no está aprobado.");
                        dataGridView1.DataSource = null;
                        return;
                    }

                    // 2. Traer los detalles del pedido aprobado
                    string queryDetalle = @"SELECT codigo_producto, descripcion, marca, cantidad_a_cotizar 
                                    FROM Pcot_Detalle
                                    WHERE id_Pcot = @idPcot";
                    SqlCommand cmdDetalle = new SqlCommand(queryDetalle, connection);
                    cmdDetalle.Parameters.AddWithValue("@idPcot", txtbNPcot.Text.Trim());
                    SqlDataAdapter adapter = new SqlDataAdapter(cmdDetalle);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);

                    if (tabla.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = tabla;

                        // Agregar columnas de precios si no existen
                        if (!dataGridView1.Columns.Contains("precio_unitario"))
                        {
                            dataGridView1.Columns.Add("precio_unitario", "Precio Unitario");
                            dataGridView1.Columns.Add("precio_total", "Precio Total");
                        }

                        // Agregar botón eliminar si no está
                        if (!dataGridView1.Columns.Contains("bttnEliminar"))
                            AgregarBotonEliminar();

                        // Inicializar precios totales en 0
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                                row.Cells["precio_total"].Value = 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron productos para ese pedido de cotización.");
                        dataGridView1.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar pedido de cotización: " + ex.Message);
                }
            }
        }

        

        private void AgregarBotonEliminar()
        {
            DataGridViewButtonColumn bttnEliminar = new DataGridViewButtonColumn();
            bttnEliminar.HeaderText = "Eliminar";
            bttnEliminar.Name = "bttnEliminar";
            bttnEliminar.Text = "X";
            bttnEliminar.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(bttnEliminar);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "bttnEliminar")
            {
                DialogResult resultado = MessageBox.Show(
                    "¿Desea eliminar este producto del pedido?",
                    "Confirmación",
                    MessageBoxButtons.YesNo
                );

                if (resultado == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row.Cells["cantidad_a_cotizar"].Value != null && row.Cells["precio_unitario"].Value != null)
            {
                decimal.TryParse(row.Cells["cantidad_a_cotizar"].Value.ToString(), out decimal cantidad);
                decimal.TryParse(row.Cells["precio_unitario"].Value.ToString(), out decimal precioUnitario);

                row.Cells["precio_total"].Value = cantidad * precioUnitario;
                ActualizarImporteTotal();
            }
        }

        private void ActualizarImporteTotal()
        {
            decimal suma = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                decimal pt = 0;
                decimal.TryParse(row.Cells["precio_total"].Value?.ToString() ?? "0", out pt);
                suma += pt;
            }
            lblTotal.Text = suma.ToString("C2");
        }

        private void bttnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            editando = false;
            idCotActual = 0;
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de cotización a modificar:", "Modificar cotización");
            if (int.TryParse(input, out int idCot))
            {
                if (CargarCotizacion(idCot))
                {
                    editando = true;
                    idCotActual = idCot;
                }
                else
                {
                    MessageBox.Show("Cotización no encontrada.");
                }
            }
        }

        private bool CargarCotizacion(int idCot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 1. Cargar datos generales de Cot_Master
                    string queryMaster = "SELECT * FROM Cot_Master WHERE id_Cot = @id";
                    SqlCommand cmdMaster = new SqlCommand(queryMaster, connection);
                    cmdMaster.Parameters.AddWithValue("@id", idCot);
                    SqlDataReader reader = cmdMaster.ExecuteReader();

                    if (!reader.Read())
                        return false; // Cotización no encontrada

                    txtbCUIT.Text = reader["id_Proveedor"].ToString();
                    lblTotal.Text = Convert.ToDecimal(reader["importe_total"]).ToString("C2");
                    dtpEmision.Value = Convert.ToDateTime(reader["fecha"]);

                    string metodo = reader["metodo_de_pago"].ToString();
                    cbContado.Checked = metodo == "Contado";
                    cbCheque.Checked = metodo == "Cheque";
                    cbCCorriente.Checked = metodo == "Cuenta Corriente";

                    reader.Close();

                    // 2. Cargar detalles de Cot_Detalle
                    string queryDetalle = @"SELECT codigo_producto, descripcion, marca, cantidad AS cantidad_a_cotizar, 
                                           precio_unitario, precio_total 
                                    FROM Cot_Detalle 
                                    WHERE id_Cot = @idCot";
                    SqlCommand cmdDetalle = new SqlCommand(queryDetalle, connection);
                    cmdDetalle.Parameters.AddWithValue("@idCot", idCot);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmdDetalle);
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);

                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = tabla;

                    // 3. Agregar botón eliminar si no existe
                    if (!dataGridView1.Columns.Contains("bttnEliminar"))
                        AgregarBotonEliminar();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar cotización: " + ex.Message);
                    return false;
                }
            }
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            if (editando)
                ActualizarCotizacion();
            else
                GuardarNuevaCotizacion();
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtbCUIT.Text))
            {
                MessageBox.Show("Debe ingresar un proveedor.");
                return false;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar productos a la cotización.");
                return false;
            }
            return true;
        }

        private void GuardarNuevaCotizacion()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryMaster = "INSERT INTO Cot_Master (fecha, metodo_de_pago, importe_total, id_Proveedor, id_Pcot) " +
                                     "VALUES (@fecha, @metodo, @importe, @proveedor, @idPcot); SELECT SCOPE_IDENTITY();";

                SqlCommand cmdMaster = new SqlCommand(queryMaster, connection);
                cmdMaster.Parameters.AddWithValue("@fecha", dtpEmision.Value);
                cmdMaster.Parameters.AddWithValue("@metodo", ObtenerMetodoPago());
                cmdMaster.Parameters.AddWithValue("@importe", CalcularImporteTotal());
                cmdMaster.Parameters.AddWithValue("@proveedor", txtbCUIT.Text);
                cmdMaster.Parameters.AddWithValue("@idPcot", txtbNPcot.Text);

                try
                {
                    connection.Open();
                    int idCot = Convert.ToInt32(cmdMaster.ExecuteScalar());
                    GuardarDetalles(idCot);
                    MessageBox.Show("Cotización guardada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
            }
        }

        private void ActualizarCotizacion()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryMaster = "UPDATE Cot_Master SET fecha=@fecha, metodo_de_pago=@metodo, importe_total=@importe WHERE id_Cot=@id";
                SqlCommand cmdMaster = new SqlCommand(queryMaster, connection);
                cmdMaster.Parameters.AddWithValue("@fecha", dtpEmision.Value);
                cmdMaster.Parameters.AddWithValue("@metodo", ObtenerMetodoPago());
                cmdMaster.Parameters.AddWithValue("@importe", CalcularImporteTotal());
                cmdMaster.Parameters.AddWithValue("@id", idCotActual);

                try
                {
                    connection.Open();
                    cmdMaster.ExecuteNonQuery();
                    EliminarDetalles(idCotActual);
                    GuardarDetalles(idCotActual);
                    MessageBox.Show("Cotización actualizada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar: " + ex.Message);
                }
            }
        }

        private string ObtenerMetodoPago()
        {
            if (cbContado.Checked) return "Contado";
            if (cbCheque.Checked) return "Cheque";
            if (cbCCorriente.Checked) return "Cuenta Corriente";
            return "";
        }

        private decimal CalcularImporteTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                decimal.TryParse(row.Cells["precio_total"].Value?.ToString() ?? "0", out decimal pt);
                total += pt;
            }
            return total;
        }

        private void GuardarDetalles(int idCot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string query = @"INSERT INTO Cot_Detalle
                    (id_Cot, codigo_producto, descripcion, marca, cantidad, precio_unitario, precio_total)
                    VALUES (@idCot, @codigo, @descripcion, @marca, @cantidad, @precioU, @precioT)";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@idCot", idCot);
                    cmd.Parameters.AddWithValue("@codigo", row.Cells["codigo_producto"].Value ?? "");
                    cmd.Parameters.AddWithValue("@descripcion", row.Cells["descripcion"].Value ?? "");
                    cmd.Parameters.AddWithValue("@marca", row.Cells["marca"].Value ?? "");
                    cmd.Parameters.AddWithValue("@cantidad", row.Cells["cantidad_a_cotizar"].Value ?? 0);
                    cmd.Parameters.AddWithValue("@precioU", row.Cells["precio_unitario"].Value ?? 0);
                    cmd.Parameters.AddWithValue("@precioT", row.Cells["precio_total"].Value ?? 0);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EliminarDetalles(int idCot)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Cot_Detalle WHERE id_Cot = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idCot);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar detalles: " + ex.Message);
                }
            }
        }

        private void LimpiarFormulario()
        {
            txtbCUIT.Clear();
            txtbNPcot.Clear();
            lblTotal.Text = "$0,00";
            dtpEmision.Value = DateTime.Now;

            cbContado.Checked = false;
            cbCheque.Checked = false;
            cbCCorriente.Checked = false;

            // Limpiar el DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
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
