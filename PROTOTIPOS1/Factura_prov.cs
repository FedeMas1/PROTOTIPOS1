using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROTOTIPOS1
{
    public partial class Factura_prov : Form
    {
        string cadena_Conexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Factura_prov()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();


            toolTip.SetToolTip(bttnBuscar, "Buscar orden de compra.");
            toolTip.SetToolTip(bttnModificar, "Modificar una factura.");
            toolTip.SetToolTip(bttnGuardar, "Guardar una factura.");
            toolTip.SetToolTip(button1, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
        }

        private void Factura_prov_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOC.Text))
            {
                MessageBox.Show("Por favor, ingrese un Id de Orden de Compra.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(cadena_Conexion))
                {
                    conn.Open();


                    string queryOC = @" SELECT p.Razon_Social, oc.total_final FROM OC_Master oc INNER JOIN Proveedores p ON oc.id_Proveedor = p.CUIT 
                    WHERE oc.id_OC = @id_OC;";

                    SqlCommand cmdOC = new SqlCommand(queryOC, conn);

                    if (!int.TryParse(txtOC.Text, out int id_OC))
                    {
                        MessageBox.Show("El ID de Orden de Compra debe ser un número.");
                        return;
                    }

                    cmdOC.Parameters.AddWithValue("@id_OC", id_OC);

                    SqlDataReader reader = cmdOC.ExecuteReader();

                    if (reader.Read())
                    {
                        lblprov.Text = reader["Razon_Social"].ToString();

                        txtimporte.Text = reader["total_final"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ninguna Orden de Compra con ese ID.");
                        lblprov.Text = string.Empty;

                        txtimporte.Clear();
                        reader.Close();
                        return;
                    }

                    reader.Close();

                    //  Obtener próximo número de factura
                    string queryFactura = "SELECT ISNULL(MAX(nro_factura), 0) + 1 AS ProximoNumeroFactura FROM Factura_proveedor;";
                    SqlCommand cmdFactura = new SqlCommand(queryFactura, conn);
                    int proximoNumero = (int)cmdFactura.ExecuteScalar();

                    lblnfact.Text = proximoNumero.ToString();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error al acceder a la base de datos:\n{ex.Message}", "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttnCalcular_Click(object sender, EventArgs e)
        {

            const decimal IVA_PORC = 0.21m;


            txtiva.Clear();
            txttotal.Clear();

            string texto = txtimporte.Text?.Trim();
            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("Ingrese el importe neto.");
                return;
            }


            if (!decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal importeNeto))
            {

                string alt = texto.Replace(',', '.');
                if (!decimal.TryParse(alt, NumberStyles.Number, CultureInfo.InvariantCulture, out importeNeto))
                {
                    MessageBox.Show("Valor de Importe Neto inválido. Use números, ej: 1234,56");
                    return;
                }
            }


            decimal iva = Math.Round(importeNeto * IVA_PORC, 2, MidpointRounding.AwayFromZero);
            decimal total = importeNeto + iva;


            txtiva.Text = iva.ToString("N2", CultureInfo.CurrentCulture);
            txttotal.Text = total.ToString("N2", CultureInfo.CurrentCulture);
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el numero de factura que desea modificar", "Modificar proveedor", "");
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("No se ingresó ningun numero");
                return;
            }

            using (SqlConnection conexion = new SqlConnection(cadena_Conexion))
            {
                conexion.Open();
                string query = "SELECT * FROM Factura_proveedor WHERE nro_factura = @nro_factura";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nro_factura", input);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtOC.Text = reader["orden_compra"].ToString();
                        lblprov.Text = reader["nombre_proveedor"].ToString();
                        lblnfact.Text = reader["nro_factura"].ToString();
                        dtpingreso.Text = reader["fecha_ingreso"].ToString();
                        dtpvencimiento.Text = reader["fecha_vencimiento"].ToString();
                        txtiva.Text = reader["IVA"].ToString();
                        txtimporte.Text = reader["importe_neto"].ToString();
                        txttotal.Text = reader["total_pagar"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("No se encontro un proveedor con ese numero");

                    }
                }
            }
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
           "¿Desea guardar la factura?",
           "Confirmar",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question
           );

            if (result == DialogResult.No)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(cadena_Conexion))
                {
                    conn.Open();


                    string checkQuery = "SELECT COUNT(*) FROM Factura_proveedor WHERE orden_compra = @orden";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@orden", txtOC.Text);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {

                        string updateQuery = @"
                    UPDATE Factura_proveedor
                    SET nombre_proveedor = @nombre_proveedor,
                        importe_neto = @importe_neto,
                        IVA = @IVA,
                        total_pagar = @total_pagar,
                        fecha_ingreso = @fecha_ingreso,
                        fecha_vencimiento = @fecha_vencimiento
                    WHERE orden_compra = @orden;";

                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@orden", txtOC.Text);
                        updateCmd.Parameters.AddWithValue("@nombre_proveedor", lblprov.Text);
                        updateCmd.Parameters.AddWithValue("@importe_neto", Convert.ToDecimal(txtimporte.Text));
                        updateCmd.Parameters.AddWithValue("@IVA", Convert.ToDecimal(txtiva.Text));
                        updateCmd.Parameters.AddWithValue("@total_pagar", Convert.ToDecimal(txttotal.Text));
                        updateCmd.Parameters.AddWithValue("@fecha_ingreso", dtpingreso.Value.Date);
                        updateCmd.Parameters.AddWithValue("@fecha_vencimiento", dtpvencimiento.Value.Date);

                        updateCmd.ExecuteNonQuery();
                        MessageBox.Show("Factura actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        string insertQuery = @"
                    INSERT INTO Factura_proveedor 
                    (orden_compra, nombre_proveedor, importe_neto, IVA, total_pagar, fecha_ingreso, fecha_vencimiento)
                    VALUES
                    (@orden_compra, @nombre_proveedor, @importe_neto, @IVA, @total_pagar, @fecha_ingreso, @fecha_vencimiento);
                    SELECT SCOPE_IDENTITY();";

                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@orden_compra", txtOC.Text);
                        insertCmd.Parameters.AddWithValue("@nombre_proveedor", lblprov.Text);
                        insertCmd.Parameters.AddWithValue("@importe_neto", Convert.ToDecimal(txtimporte.Text));
                        insertCmd.Parameters.AddWithValue("@IVA", Convert.ToDecimal(txtiva.Text));
                        insertCmd.Parameters.AddWithValue("@total_pagar", Convert.ToDecimal(txttotal.Text));
                        insertCmd.Parameters.AddWithValue("@fecha_ingreso", dtpingreso.Value.Date);
                        insertCmd.Parameters.AddWithValue("@fecha_vencimiento", dtpvencimiento.Value.Date);

                        int nuevoNroFactura = Convert.ToInt32(insertCmd.ExecuteScalar());
                        lblnfact.Text = nuevoNroFactura.ToString();

                        MessageBox.Show("Factura guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    txtOC.Clear();
                    txtimporte.Clear();
                    txtiva.Clear();
                    txttotal.Clear();
                    lblprov.Text = string.Empty;
                    lblnfact.Text = string.Empty;
                    dtpingreso.Value = DateTime.Today;
                    dtpvencimiento.Value = DateTime.Today;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Revisar que los campos numéricos estén completos y correctos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al acceder a la base de datos:\n" + ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
