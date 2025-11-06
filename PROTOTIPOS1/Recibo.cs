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
    public partial class Recibo : Form
    {
        string cadenaConexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Recibo()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            MostrarProximoNumeroRecibo();

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnModificar, "Modificar un Recibo.");
            toolTip.SetToolTip(bttnGuardar, "Guardar un Recibo.");
            toolTip.SetToolTip(button3, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
            toolTip.SetToolTip(txtnpago, "Ingrese el NUmero de Pago");
        }








        private void button3_Click(object sender, EventArgs e)
        {
            PPrincipal_Pagos ppp = new PPrincipal_Pagos();
            ppp.Show();
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtnpago.Text))
            {
                MessageBox.Show("Ingrese un número de pago.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtnpago.Text, out int nroPago))
            {
                MessageBox.Show("El número de pago debe ser un número entero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string query = @"
            SELECT 
            p.importe_total,
            op.importe_nt,
            op.nro_factura,
            f.orden_compra,
            oc.id_Proveedor
            FROM Pagos p
            INNER JOIN Orden_De_Pago op ON p.orden_de_pago = op.nro_op
            INNER JOIN Factura_proveedor f ON op.nro_factura = f.nro_factura
            INNER JOIN OC_Master oc ON f.orden_compra = oc.id_OC
            INNER JOIN Proveedores prov ON oc.id_Proveedor = prov.CUIT
            WHERE p.nro_pago = @nro_pago;
            ";

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nro_pago", nroPago);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblfact.Text = reader["nro_factura"].ToString();
                        lbloc.Text = reader["orden_compra"].ToString();
                        lblprov.Text = reader["id_Proveedor"].ToString();
                        lblnt.Text = Convert.ToDecimal(reader["importe_nt"]).ToString("N2");
                        lbltotal.Text = Convert.ToDecimal(reader["importe_total"]).ToString("N2");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un pago con ese número.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        lblfact.Text = "";
                        lbloc.Text = "";
                        lblprov.Text = "";
                        lblnt.Text = "";
                        lbltotal.Text = "";
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MostrarProximoNumeroRecibo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();


                    string query = "SELECT ISNULL(MAX(nro_recibo), 0) + 1 FROM Recibooo";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int proximoNumero = Convert.ToInt32(cmd.ExecuteScalar());
                        lblnrecibo.Text = proximoNumero.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el número de orden de pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtnpago.Text))
            {
                MessageBox.Show("Ingrese un número de pago.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtnpago.Text, out int nroPago))
            {
                MessageBox.Show("El número de pago debe ser un número entero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(lblfact.Text) ||
                string.IsNullOrWhiteSpace(lbloc.Text) ||
                string.IsNullOrWhiteSpace(lblprov.Text))
            {
                MessageBox.Show("Primero busque un pago válido antes de guardar el recibo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Desea guardar el recibo?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            int factura = Convert.ToInt32(lblfact.Text);
            int ordenCompra = Convert.ToInt32(lbloc.Text);
            int proveedor = Convert.ToInt32(lblprov.Text);
            decimal notaCredito = Convert.ToDecimal(lblnt.Text);
            decimal importeTotal = Convert.ToDecimal(lbltotal.Text);
            DateTime fecha = dtpfecha.Value;

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                conn.Open();

                // 🔹 Primero verificamos si ya existe un recibo con ese nro_pago
                string checkQuery = "SELECT COUNT(*) FROM Recibooo WHERE nro_pago = @nro_pago";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@nro_pago", nroPago);
                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("Ya existe un recibo con ese número de pago.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                string insertQuery = @"
                INSERT INTO Recibooo (factura, orden_de_compra, proveedor, nota_credito, importe_total, fecha, nro_pago)
                VALUES (@factura, @orden_de_compra, @proveedor, @nota_credito, @importe_total, @fecha, @nro_pago);
                ";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@factura", factura);
                    cmd.Parameters.AddWithValue("@orden_de_compra", ordenCompra);
                    cmd.Parameters.AddWithValue("@proveedor", proveedor);
                    cmd.Parameters.AddWithValue("@nota_credito", notaCredito);
                    cmd.Parameters.AddWithValue("@importe_total", importeTotal);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@nro_pago", nroPago);

                    try
                    {
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Recibo guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            txtnpago.Clear();
                            lblfact.Text = "";
                            lbloc.Text = "";
                            lblprov.Text = "";
                            lblnt.Text = "";
                            lbltotal.Text = "";
                            dtpfecha.Value = DateTime.Now;


                            txtnpago.Focus();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo guardar el recibo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar el recibo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
