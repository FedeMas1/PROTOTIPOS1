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
    public partial class Orden_de_Pago : Form
    {
        private decimal importeFacturaValor = 0;
        private decimal importeNotaValor = 0;
        string cadenaConexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Orden_de_Pago()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnEliminar, "Eliminar una Orden de Pago.");
            toolTip.SetToolTip(bttnModificar, "Modificar una Orden de Pago.");
            toolTip.SetToolTip(bttnGuardar, "Guardar una Orden de Pago.");
            toolTip.SetToolTip(button4, "Buscar numero de Factura.");
            toolTip.SetToolTip(button5, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
            toolTip.SetToolTip(bttnCSesion, "Vuelve al formulario anterior.");
        }

        private decimal ParseLabelToDecimal(Label lbl)
        {
            if (lbl == null || string.IsNullOrWhiteSpace(lbl.Text))
                return 0;


            string texto = lbl.Text.Replace("$", "").Trim();


            if (decimal.TryParse(texto, out decimal valor))
                return valor;


            texto = texto.Replace(",", ".");
            decimal.TryParse(texto, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valor);

            return valor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nroFactura = txtfact.Text.Trim();

            if (string.IsNullOrEmpty(nroFactura))
            {
                MessageBox.Show("Por favor, ingrese el número de factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    f.total_pagar,
                    p.CUIT AS CUIT,
                    p.Razon_Social
                FROM Factura_proveedor f
                INNER JOIN OC_Master o ON f.orden_compra = o.id_OC
                INNER JOIN Proveedores p ON o.id_Proveedor = p.CUIT
                WHERE f.nro_factura = @nro_factura;
            ";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nro_factura", nroFactura);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblcuit.Text = reader["CUIT"].ToString();
                                lblrazon.Text = reader["Razon_Social"].ToString();

                                importeFacturaValor = Convert.ToDecimal(reader["total_pagar"]);


                                lblimportefact.Text = importeFacturaValor.ToString("0.00");


                                CalcularTotal();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró una factura con ese número.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                lblcuit.Text = "";
                                lblrazon.Text = "";
                                lblimportefact.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chknt_CheckedChanged(object sender, EventArgs e)
        {
            bool mostrar = chknt.Checked;


            lblnnt.Visible = mostrar;
            lblnt.Visible = mostrar;
            txtnnt.Visible = mostrar;
            txtnt.Visible = mostrar;
        }

        private void MostrarProximoNumeroOrdenPago()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();


                    string query = "SELECT ISNULL(MAX(nro_op), 0) + 1 FROM Orden_De_Pago";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int proximoNumero = Convert.ToInt32(cmd.ExecuteScalar());
                        lblop.Text = proximoNumero.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el número de orden de pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Orden_de_Pago_Load(object sender, EventArgs e)
        {
            MostrarProximoNumeroOrdenPago();
            chknt.Checked = false;


            lblnnt.Visible = false;
            lblnt.Visible = false;
            txtnnt.Visible = false;
            txtnt.Visible = false;
        }


        private void CalcularTotal()
        {
            decimal total = importeFacturaValor - (chknt.Checked ? importeNotaValor : 0);


            lbltotal.Text = total.ToString("0.00");
        }

        private void txtnt_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtnt.Text, out decimal valorNT))
            {
                importeNotaValor = valorNT;
            }
            else
            {
                importeNotaValor = 0;
            }
            CalcularTotal();
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            string nroFactura = txtfact.Text.Trim();
            string nroNT = txtnnt.Text.Trim();


            if (ExisteFactura(nroFactura))
            {
                MessageBox.Show("Ya existe una orden de pago asociada a esta factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(nroNT) && ExisteNT(nroNT))
            {
                MessageBox.Show("Ya existe una orden de pago con ese número de nota de crédito.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            decimal importeNT = 0;
            decimal.TryParse(
                txtnt.Text.Trim().Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out importeNT
            );


            decimal importeFactura = ParseLabelToDecimal(lblimportefact);
            decimal total = ParseLabelToDecimal(lbltotal);


            MessageBox.Show($"ImporteFactura: {importeFactura} | Total: {total}");


            string estado = Procesando.Checked ? "Procesando" :
                            Aceptado.Checked ? "Aceptado" :
                            Denegado.Checked ? "Denegado" :
                            Realizado.Checked ? "Realizado" : "";

            string cuitProveedor = lblcuit.Text.Trim();
            string razonSocial = lblrazon.Text.Trim();
            DateTime fecha = dtpfecha.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string insertQuery = @"
                INSERT INTO Orden_De_Pago
                (nro_factura, nro_nt, importe_nt, total, importe_factura, estado, cuit_proveedor, razon_social_prov, fecha)
                VALUES
                (@nroFactura, @nroNT, @importeNT, @total, @importeFactura, @estado, @cuitProveedor, @razonSocial, @fecha);
            ";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                        cmd.Parameters.AddWithValue("@nroNT", string.IsNullOrEmpty(nroNT) ? (object)DBNull.Value : nroNT);
                        cmd.Parameters.Add("@importeNT", SqlDbType.Decimal).Value = importeNT;
                        cmd.Parameters.Add("@total", SqlDbType.Decimal).Value = total;
                        cmd.Parameters.Add("@importeFactura", SqlDbType.Decimal).Value = importeFactura;
                        cmd.Parameters.AddWithValue("@estado", estado);
                        cmd.Parameters.AddWithValue("@cuitProveedor", cuitProveedor);
                        cmd.Parameters.AddWithValue("@razonSocial", razonSocial);
                        cmd.Parameters.AddWithValue("@fecha", fecha);

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                            MessageBox.Show("Orden de pago guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("No se pudo guardar la orden de pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la orden de pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ExisteFactura(string nroFactura)
        {
            bool existe = false;
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Orden_De_Pago WHERE nro_factura = @nroFactura";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nroFactura", nroFactura);
                    int count = (int)cmd.ExecuteScalar();
                    existe = count > 0;
                }
            }
            return existe;
        }

        private bool ExisteNT(string nroNT)
        {
            bool existe = false;
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Orden_De_Pago WHERE nro_nt = @nroNT";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nroNT", nroNT);
                    int count = (int)cmd.ExecuteScalar();
                    existe = count > 0;
                }
            }
            return existe;
        }

        private decimal ObtenerImporteFactura()
        {
            decimal valor = 0;

            if (!string.IsNullOrEmpty(lblimportefact.Text))
            {

                string txt = lblimportefact.Text.Replace("$", "").Replace(" ", "").Trim();


                txt = txt.Replace(".", "").Replace(",", ".");

                decimal.TryParse(txt, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valor);
            }

            return valor;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void bttnCSesion_Click(object sender, EventArgs e)
        {
            PPrincipal_Pagos ppp = new PPrincipal_Pagos();
            ppp.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            Modificar_OP modificar_OP = new Modificar_OP();
            modificar_OP.Show();
            Hide();
        }
    }
}
