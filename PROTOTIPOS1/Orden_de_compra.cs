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
    public partial class Orden_de_compra : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        private bool modoModificacion = false;
        private int idOCSeleccionado = 0;
        public Orden_de_compra()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnEliminar, "Eliminar una orden de compra.");
            toolTip.SetToolTip(button3, "Modificar una orden de compra.");
            toolTip.SetToolTip(bttnGuardar, "Guardar una orden de compra.");
            toolTip.SetToolTip(button1, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
        }

        private void InicializarDGV()
        {
            dgvOC.Columns.Clear();

            dgvOC.Columns.Add("Descripcion", "Descripción");
            dgvOC.Columns.Add("PrecioUnitario", "Precio Unitario");
            dgvOC.Columns.Add("Cantidad", "Cantidad");
            dgvOC.Columns.Add("Subtotal", "Subtotal");

            // Columna botón Eliminar
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.Name = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            dgvOC.Columns.Add(btnEliminar);

            // Ajustes opcionales
            dgvOC.AllowUserToAddRows = false;
        }

        private void RecalcularSubtotal(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvOC.Rows.Count) return;

            DataGridViewRow row = dgvOC.Rows[rowIndex];

            if (!decimal.TryParse(row.Cells["PrecioUnitario"].Value?.ToString(), out decimal precio)) return;
            if (!int.TryParse(row.Cells["Cantidad"].Value?.ToString(), out int cantidad)) cantidad = 0;

            decimal subtotal = precio * cantidad;
            row.Cells["Subtotal"].Value = subtotal.ToString("N2");

            RecalcularTotalFinal();
        }

        private void RecalcularTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvOC.Rows)
            {
                if (decimal.TryParse(row.Cells["Subtotal"].Value?.ToString(), out decimal subtotal))
                {
                    total += subtotal;
                }
            }
            lblTotal.Text = total.ToString("0.00");
        }


        private void RecalcularTotalFinal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvOC.Rows)
            {
                if (row.Cells["Subtotal"].Value != null &&
                    decimal.TryParse(row.Cells["Subtotal"].Value.ToString(), out decimal subtotal))
                {
                    total += subtotal;

                    // Mostrar Subtotal con 2 decimales
                    row.Cells["Subtotal"].Value = subtotal.ToString("N2");
                }

                if (row.Cells["PrecioUnitario"].Value != null &&
                    decimal.TryParse(row.Cells["PrecioUnitario"].Value.ToString(), out decimal precio))
                {
                    // Mostrar Precio Unitario con 2 decimales
                    row.Cells["PrecioUnitario"].Value = precio.ToString("N2");
                }
            }

            lblTotal.Text = total.ToString("N2");
        }

        private void dgvOC_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvOC.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                DialogResult r = MessageBox.Show("¿Eliminar este producto?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    dgvOC.Rows.RemoveAt(e.RowIndex);
                    RecalcularTotalFinal();
                }
            }
        }

        private void dgvOC_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (dgvOC.Columns[e.ColumnIndex].Name == "Cantidad" ||
                                    dgvOC.Columns[e.ColumnIndex].Name == "PrecioUnitario"))
            {
                DataGridViewRow row = dgvOC.Rows[e.RowIndex];

                decimal precio = 0;
                decimal cantidad = 0;

                decimal.TryParse(row.Cells["PrecioUnitario"].Value?.ToString(), out precio);
                decimal.TryParse(row.Cells["Cantidad"].Value?.ToString(), out cantidad);

                decimal subtotal = precio * cantidad;
                row.Cells["Subtotal"].Value = subtotal.ToString("N2");

                RecalcularTotalFinal();
            }
        }

        private void ConfigurarDGV_OC()
        {
            dgvOC.Columns.Clear();

            dgvOC.AutoGenerateColumns = false;
            dgvOC.AllowUserToAddRows = false;

            // DESCRIPCION
            dgvOC.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Descripcion",
                HeaderText = "Descripción",
                ReadOnly = true
            });

            // PRECIO UNITARIO
            dgvOC.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio Unitario",
                ReadOnly = true
            });

            // CANTIDAD (editable)
            dgvOC.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                ReadOnly = false
            });

            // SUBTOTAL
            dgvOC.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                ReadOnly = true
            });

            // BOTÓN ELIMINAR
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "Eliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "X";
            btnEliminar.UseColumnTextForButtonValue = true;
            dgvOC.Columns.Add(btnEliminar);
        }

        

        private void dgvOC_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvOC.Columns["Cantidad"].Index)
            {
                RecalcularSubtotal(e.RowIndex);
            }
        }

        private void ModoNuevaOC()
        {
            bttnGuardar.Enabled = true;      
            button3.Enabled = true;
            bttnEliminar.Enabled = false;     // Solo debería estar para eliminar OC completa
            bttnBuscar.Enabled = true;        // Buscar OC
            bttnBCotizacion.Enabled = true;   // Botón para buscar Cotización

            // Habilitar edición de campos
            dtpFecha.Enabled = true;
            dtpFEntrega.Enabled = true;
            txtbLEntrega.Enabled = true;

            cbEfectivo.Enabled = true;
            cbCheque.Enabled = true;
            cbCCorriente.Enabled = true;
        }

        private void LimpiarCampos()
        {
            lblNOC.Text = ""; // O "Nuevo" si preferís
            dtpFecha.Value = DateTime.Today;
            lblProveedor.Text = "";
            lblCUIT.Text = "";
            lblDomicilio.Text = "";
            lblNumero.Text = "";
            lblPiso.Text = "";
            lblDepto.Text = "";
            dtpFEntrega.Value = DateTime.Today;
            txtbLEntrega.Clear();

            cbEfectivo.Checked = false;
            cbCheque.Checked = false;
            cbCCorriente.Checked = false;

            dgvOC.Rows.Clear();
            lblTotal.Text = "0.00";
        }

        private void Orden_de_compra_Load(object sender, EventArgs e)
        {
            InicializarDGV();
            ConfigurarDGV_OC();
            LimpiarCampos();
            ModoNuevaOC();
            dgvOC.CellValueChanged += dgvOC_CellValueChanged;
        }



        // guardar
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbNCotizacion.Text) || dgvOC.Rows.Count == 0)
            {
                MessageBox.Show("Debe cargar una cotización y productos para guardar la OC.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int idOC = idOCSeleccionado; // Para usar en Update
                    int idCot = int.Parse(txtbNCotizacion.Text);
                    int idProveedor = 0; // Lo buscamos según lblProveedor
                    DateTime fechaEmision = dtpFecha.Value;
                    DateTime fechaEntrega = dtpFEntrega.Value;
                    string lugarEntrega = txtbLEntrega.Text;

                    // Construir modo de pago
                    string modoPago = "";
                    if (cbEfectivo.Checked) modoPago += "Efectivo;";
                    if (cbCheque.Checked) modoPago += "Cheque;";
                    if (cbCCorriente.Checked) modoPago += "Cuenta Corriente;";

                    decimal totalFinal = 0;
                    foreach (DataGridViewRow row in dgvOC.Rows)
                        totalFinal += Convert.ToDecimal(row.Cells["Subtotal"].Value);

                    // Obtener idProveedor desde la DB según lblProveedor
                    string queryProveedor = "SELECT CUIT FROM Proveedores WHERE Razon_Social = @razon";
                    SqlCommand cmdProv = new SqlCommand(queryProveedor, connection, transaction);
                    cmdProv.Parameters.AddWithValue("@razon", lblProveedor.Text);
                    idProveedor = Convert.ToInt32(cmdProv.ExecuteScalar());

                    if (!modoModificacion)
                    {
                        // INSERT OC_Master
                        string queryInsertMaster = @"
                    INSERT INTO OC_Master (id_Cot, fecha_Emision, fecha_Entrega, lugar_Entrega, modo_Pago, id_Proveedor, total_Final)
                    VALUES (@idCot, @fechaEm, @fechaEnt, @lugar, @modo, @idProv, @total);
                    SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdMaster = new SqlCommand(queryInsertMaster, connection, transaction);
                        cmdMaster.Parameters.AddWithValue("@idCot", idCot);
                        cmdMaster.Parameters.AddWithValue("@fechaEm", fechaEmision);
                        cmdMaster.Parameters.AddWithValue("@fechaEnt", fechaEntrega);
                        cmdMaster.Parameters.AddWithValue("@lugar", lugarEntrega);
                        cmdMaster.Parameters.AddWithValue("@modo", modoPago);
                        cmdMaster.Parameters.AddWithValue("@idProv", idProveedor);
                        cmdMaster.Parameters.AddWithValue("@total", totalFinal);

                        idOC = Convert.ToInt32(cmdMaster.ExecuteScalar());
                    }
                    else
                    {
                        // UPDATE OC_Master
                        string queryUpdateMaster = @"
                    UPDATE OC_Master
                    SET fecha_Entrega=@fechaEnt, lugar_Entrega=@lugar, modo_Pago=@modo, total_Final=@total
                    WHERE id_OC=@idOC";
                        SqlCommand cmdMaster = new SqlCommand(queryUpdateMaster, connection, transaction);
                        cmdMaster.Parameters.AddWithValue("@fechaEnt", fechaEntrega);
                        cmdMaster.Parameters.AddWithValue("@lugar", lugarEntrega);
                        cmdMaster.Parameters.AddWithValue("@modo", modoPago);
                        cmdMaster.Parameters.AddWithValue("@total", totalFinal);
                        cmdMaster.Parameters.AddWithValue("@idOC", idOC);
                        cmdMaster.ExecuteNonQuery();

                        // DELETE detalles antiguos
                        string queryDeleteDetalles = "DELETE FROM OC_Detalle WHERE id_OC=@idOC";
                        SqlCommand cmdDel = new SqlCommand(queryDeleteDetalles, connection, transaction);
                        cmdDel.Parameters.AddWithValue("@idOC", idOC);
                        cmdDel.ExecuteNonQuery();
                    }

                    // INSERT detalles
                    foreach (DataGridViewRow row in dgvOC.Rows)
                    {
                        string queryInsertDet = @"
                    INSERT INTO OC_Detalle (id_OC, descripcion, precio_unitario, cantidad, subtotal)
                    VALUES (@idOC, @desc, @precio, @cant, @subt)";

                        SqlCommand cmdDet = new SqlCommand(queryInsertDet, connection, transaction);
                        cmdDet.Parameters.AddWithValue("@idOC", idOC);
                        cmdDet.Parameters.AddWithValue("@desc", row.Cells["Descripcion"].Value.ToString());
                        cmdDet.Parameters.AddWithValue("@precio", Convert.ToDecimal(row.Cells["PrecioUnitario"].Value));
                        cmdDet.Parameters.AddWithValue("@cant", Convert.ToInt32(row.Cells["Cantidad"].Value));
                        cmdDet.Parameters.AddWithValue("@subt", Convert.ToDecimal(row.Cells["Subtotal"].Value));
                        cmdDet.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Orden de Compra guardada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar formulario
                    LimpiarFormulario();
                    ModoNuevaOC();
                    modoModificacion = false;
                    idOCSeleccionado = 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al guardar la OC: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
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

        //agregar
        private void button5_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarEdicion(true);

            modoModificacion = false;
            idOCSeleccionado = 0;

            dtpFecha.Value = DateTime.Now;
            dtpFEntrega.Value = DateTime.Now;

            MessageBox.Show("Complete los datos para generar una nueva Orden de Compra.",
                "Nueva OC", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LimpiarFormulario()
        {
            txtbNCotizacion.Clear();
            lblNOC.Text = "";
            lblProveedor.Text = "";
            lblCUIT.Text = "";
            lblDomicilio.Text = "";
            lblNumero.Text = "";
            lblPiso.Text = "";
            lblDepto.Text = "";
            txtbLEntrega.Clear();

            cbEfectivo.Checked = false;
            cbCheque.Checked = false;
            cbCCorriente.Checked = false;

            dtpFEntrega.Value = DateTime.Now;
            dtpFecha.Value = DateTime.Now;

            dgvOC.Rows.Clear();
        }

        private void HabilitarEdicion(bool habilitar)
        {
            txtbNCotizacion.Enabled = habilitar;
            dtpFecha.Enabled = habilitar;
            dtpFEntrega.Enabled = habilitar;
            txtbLEntrega.Enabled = habilitar;

            cbEfectivo.Enabled = habilitar;
            cbCheque.Enabled = habilitar;
            cbCCorriente.Enabled = habilitar;

            dgvOC.Enabled = habilitar;

            bttnGuardar.Enabled = habilitar; 
            bttnEliminar.Enabled = habilitar; 
        }
      

        //modificar
        private void button3_Click(object sender, EventArgs e)
        {
            // Pedir número de OC a modificar
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el número de Orden de Compra a modificar:",
                "Modificar OC");

            if (!int.TryParse(input, out int nroOC))
            {
                MessageBox.Show("Número de OC inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string queryMaster = @"
                                        SELECT OC.id_OC, OC.id_Cot, OC.fecha_Emision, OC.fecha_Entrega,
                                               OC.lugar_Entrega, OC.modo_Pago, OC.total_Final,
                                               P.Razon_Social, P.CUIT, P.Calle, P.Nro_Calle, P.Piso, P.Departamento
                                        FROM OC_Master OC
                                        INNER JOIN Proveedores P ON OC.id_Proveedor = P.CUIT
                                        WHERE OC.id_OC = @idOC";

                    SqlCommand cmdMaster = new SqlCommand(queryMaster, connection);
                    cmdMaster.Parameters.AddWithValue("@idOC", nroOC);

                    SqlDataReader reader = cmdMaster.ExecuteReader();

                    if (!reader.Read())
                    {
                        MessageBox.Show("La Orden de Compra no existe.", "Sin resultados",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader.Close();
                        return;
                    }

                    lblNOC.Text = reader["id_OC"].ToString();
                    txtbNCotizacion.Text = reader["id_Cot"].ToString();
                    dtpFecha.Value = Convert.ToDateTime(reader["fecha_Emision"]);
                    dtpFEntrega.Value = Convert.ToDateTime(reader["fecha_Entrega"]);
                    txtbLEntrega.Text = reader["lugar_Entrega"].ToString();
                    lblProveedor.Text = reader["Razon_Social"].ToString();
                    lblCUIT.Text = reader["CUIT"].ToString();
                    lblDomicilio.Text = reader["Calle"].ToString();
                    lblNumero.Text = reader["Nro_Calle"].ToString();
                    lblPiso.Text = reader["Piso"].ToString();
                    lblDepto.Text = reader["Departamento"].ToString();
                    lblTotal.Text = Convert.ToDecimal(reader["total_Final"]).ToString("N2");

                    string modoPago = reader["modo_Pago"].ToString();
                    cbEfectivo.Checked = modoPago.Contains("Efectivo");
                    cbCheque.Checked = modoPago.Contains("Cheque");
                    cbCCorriente.Checked = modoPago.Contains("Cuenta Corriente");

                    reader.Close();

                    dgvOC.Rows.Clear();
                    string queryDetalles = @"
                SELECT descripcion, precio_unitario, cantidad, subtotal
                FROM OC_Detalle
                WHERE id_OC = @idOC";

                    SqlCommand cmdDetalles = new SqlCommand(queryDetalles, connection);
                    cmdDetalles.Parameters.AddWithValue("@idOC", nroOC);

                    SqlDataReader readerDet = cmdDetalles.ExecuteReader();

                    while (readerDet.Read())
                    {
                        dgvOC.Rows.Add(
                            readerDet["descripcion"].ToString(),
                            readerDet["precio_unitario"].ToString(),
                            readerDet["cantidad"].ToString(),
                            readerDet["subtotal"].ToString()
                        );
                    }

                    readerDet.Close();

                    dtpFEntrega.Enabled = true;
                    txtbLEntrega.Enabled = true;

                    cbEfectivo.Enabled = true;
                    cbCheque.Enabled = true;
                    cbCCorriente.Enabled = true;

                    dgvOC.Enabled = true; 

                    bttnGuardar.Enabled = true;  // Guardar
                    bttnEliminar.Enabled = true;  // Eliminar
                    button3.Enabled = false; // Modificar
                    button5.Enabled = true;  // Agregar
                    bttnBuscar.Enabled = true; 

                    modoModificacion = true;
                    idOCSeleccionado = nroOC;

                    MessageBox.Show("Orden de Compra cargada en modo edición.", "Modificar OC",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar OC: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //eliminar
        private void button4_Click(object sender, EventArgs e)
        {
            if (idOCSeleccionado == 0)
            {
                MessageBox.Show("No hay OC seleccionada para eliminar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Desea eliminar esta Orden de Compra completa?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string queryDelDet = "DELETE FROM OC_Detalle WHERE id_OC=@idOC";
                    SqlCommand cmdDet = new SqlCommand(queryDelDet, connection, transaction);
                    cmdDet.Parameters.AddWithValue("@idOC", idOCSeleccionado);
                    cmdDet.ExecuteNonQuery();
                
                    string queryDelMaster = "DELETE FROM OC_Master WHERE id_OC=@idOC";
                    SqlCommand cmdMaster = new SqlCommand(queryDelMaster, connection, transaction);
                    cmdMaster.Parameters.AddWithValue("@idOC", idOCSeleccionado);
                    cmdMaster.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Orden de Compra eliminada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LimpiarFormulario();
                    ModoNuevaOC();
                    modoModificacion = false;
                    idOCSeleccionado = 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al eliminar la OC: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bttnBCotizacion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbNCotizacion.Text))
            {
                MessageBox.Show("Ingrese un número de cotización.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtbNCotizacion.Text, out int nroCot))
            {
                MessageBox.Show("Número de cotización inválido.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }          

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    string queryMaster = @"
                SELECT CM.id_Cot, CM.id_Proveedor, P.Razon_Social, P.CUIT, P.Calle, 
                       P.Nro_Calle, P.Piso, P.Departamento, CM.importe_total
                FROM Cot_Master CM
                INNER JOIN Proveedores P ON CM.id_Proveedor = P.CUIT
                WHERE CM.id_Cot = @idCot";

                    SqlCommand cmdMaster = new SqlCommand(queryMaster, connection);
                    cmdMaster.Parameters.AddWithValue("@idCot", nroCot);

                    SqlDataReader reader = cmdMaster.ExecuteReader();

                    if (!reader.Read())
                    {
                        MessageBox.Show("La cotización no existe.", "Sin resultados",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader.Close();
                        return;
                    }

                    
                    lblNOC.Text = reader["id_Cot"].ToString();
                    lblProveedor.Text = reader["Razon_Social"].ToString();
                    lblCUIT.Text = reader["CUIT"].ToString();
                    lblDomicilio.Text = reader["Calle"].ToString();
                    lblNumero.Text = reader["Nro_Calle"].ToString();
                    lblPiso.Text = reader["Piso"].ToString();
                    lblDepto.Text = reader["Departamento"].ToString();
                    lblTotal.Text = reader["importe_total"].ToString();

                    int idProveedor = Convert.ToInt32(reader["id_Proveedor"]);
                    reader.Close();

                   
                    dgvOC.Rows.Clear();

                    string queryDetalles = @"SELECT descripcion, precio_unitario, cantidad, precio_total
                                     FROM Cot_Detalle
                                     WHERE id_Cot = @idCot";

                    SqlCommand cmdDetalles = new SqlCommand(queryDetalles, connection);
                    cmdDetalles.Parameters.AddWithValue("@idCot", nroCot);

                    SqlDataReader readerDet = cmdDetalles.ExecuteReader();

                    while (readerDet.Read())
                    {
                        dgvOC.Rows.Add(
                            readerDet["descripcion"].ToString(),
                            readerDet["precio_unitario"].ToString(),
                            readerDet["cantidad"].ToString(),
                            readerDet["precio_total"].ToString()
                        );
                    }

                    readerDet.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar cotización: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
        "Ingrese el número de Orden de Compra a buscar:",
        "Buscar OC");

            if (!int.TryParse(input, out int nroOC))
            {
                MessageBox.Show("Número de OC inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string queryMaster = @"
                                            SELECT OC.id_OC, OC.id_Cot, OC.fecha_Emision, OC.fecha_Entrega,
                                                   OC.lugar_Entrega, OC.modo_Pago, OC.total_Final,
                                                   P.Razon_Social, P.CUIT, P.Calle, P.Nro_Calle, P.Piso, P.Departamento
                                            FROM OC_Master OC
                                            INNER JOIN Proveedores P ON OC.id_Proveedor = P.CUIT
                                            WHERE OC.id_OC = @idOC";

                    SqlCommand cmdMaster = new SqlCommand(queryMaster, connection);
                    cmdMaster.Parameters.AddWithValue("@idOC", nroOC);

                    SqlDataReader reader = cmdMaster.ExecuteReader();

                    if (!reader.Read())
                    {
                        MessageBox.Show("La Orden de Compra no existe.", "Sin resultados",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader.Close();
                        return;
                    }

                   
                    lblNOC.Text = reader["id_OC"].ToString();
                    txtbNCotizacion.Text = reader["id_Cot"].ToString();
                    dtpFecha.Value = Convert.ToDateTime(reader["fecha_Emision"]);
                    dtpFEntrega.Value = Convert.ToDateTime(reader["fecha_Entrega"]);
                    txtbLEntrega.Text = reader["lugar_Entrega"].ToString();
                    lblProveedor.Text = reader["Razon_Social"].ToString();
                    lblCUIT.Text = reader["CUIT"].ToString();
                    lblDomicilio.Text = reader["Calle"].ToString();
                    lblNumero.Text = reader["Nro_Calle"].ToString();
                    lblPiso.Text = reader["Piso"].ToString();
                    lblDepto.Text = reader["Departamento"].ToString();
                    lblTotal.Text = Convert.ToDecimal(reader["total_Final"]).ToString("N2");

                    string modoPago = reader["modo_Pago"].ToString();

                    cbEfectivo.Checked = modoPago.Contains("Efectivo");
                    cbCheque.Checked = modoPago.Contains("Cheque");
                    cbCCorriente.Checked = modoPago.Contains("Cuenta Corriente");

                    reader.Close();
                    dgvOC.Rows.Clear();

                    string queryDetalles = @"
                SELECT descripcion, precio_unitario, cantidad, subtotal
                FROM OC_Detalle
                WHERE id_OC = @idOC";

                    SqlCommand cmdDetalles = new SqlCommand(queryDetalles, connection);
                    cmdDetalles.Parameters.AddWithValue("@idOC", nroOC);

                    SqlDataReader readerDet = cmdDetalles.ExecuteReader();

                    while (readerDet.Read())
                    {
                        dgvOC.Rows.Add(
                            readerDet["descripcion"].ToString(),
                            readerDet["precio_unitario"].ToString(),
                            readerDet["cantidad"].ToString(),
                            readerDet["subtotal"].ToString()
                        );
                    }

                    readerDet.Close();
                    dtpFecha.Enabled = false;
                    dtpFEntrega.Enabled = false;
                    txtbLEntrega.Enabled = false;

                    cbEfectivo.Enabled = false;
                    cbCheque.Enabled = false;
                    cbCCorriente.Enabled = false;

                    dgvOC.Enabled = false;        
                    bttnGuardar.Enabled = false;      // Guardar
                    bttnEliminar.Enabled = false;      // Eliminar
                    button3.Enabled = true;       // Modificar
                    button5.Enabled = true;       // Agregar
                    bttnBuscar.Enabled = true;       

                    MessageBox.Show("Orden de Compra cargada en modo lectura.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar OC: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
