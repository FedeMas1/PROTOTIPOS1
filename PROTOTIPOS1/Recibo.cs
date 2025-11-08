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
        private int? idReciboEnEdicion = null;
        string cadenaConexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Recibo()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);


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

            int factura = int.Parse(lblfact.Text);
            int orden = int.Parse(lbloc.Text);
            int proveedor = int.Parse(lblprov.Text);
            decimal notaCredito = decimal.Parse(lblnt.Text);
            decimal importeTotal = decimal.Parse(lbltotal.Text);
            DateTime fecha = dtpfecha.Value;

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;

                // Si idReciboEnEdicion es null → INSERT, si tiene valor → UPDATE
                if (idReciboEnEdicion == null)
                {
                    cmd.CommandText = @"INSERT INTO Recibo 
                                (factura, orden_de_compra, proveedor, nota_credito, importe_total, fecha, nro_pago)
                                VALUES (@factura, @orden, @proveedor, @notaCredito, @importeTotal, @fecha, @nroPago)";
                }
                else
                {
                    cmd.CommandText = @"UPDATE Recibo SET
                                factura = @factura,
                                orden_de_compra = @orden,
                                proveedor = @proveedor,
                                nota_credito = @notaCredito,
                                importe_total = @importeTotal,
                                fecha = @fecha,
                                nro_pago = @nroPago
                                WHERE nro_recibo = @id";

                    cmd.Parameters.AddWithValue("@id", idReciboEnEdicion.Value);
                }

                cmd.Parameters.AddWithValue("@factura", factura);
                cmd.Parameters.AddWithValue("@orden", orden);
                cmd.Parameters.AddWithValue("@proveedor", proveedor);
                cmd.Parameters.AddWithValue("@notaCredito", notaCredito);
                cmd.Parameters.AddWithValue("@importeTotal", importeTotal);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@nroPago", nroPago);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            if (idReciboEnEdicion == null)
                MessageBox.Show("Recibo guardado correctamente.");
            else
                MessageBox.Show("Recibo modificado correctamente.");

            // Resetear para que el próximo GUARDAR vuelva a insertar
            idReciboEnEdicion = null;
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de recibo a modificar:", "Modificar Recibo");

            if (!int.TryParse(input, out int nroRecibo))
            {
                MessageBox.Show("Número inválido.");
                return;
            }

            string query = "SELECT * FROM Recibo WHERE nro_recibo = @nro";

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@nro", nroRecibo);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    // Guarda el ID para usarlo en el UPDATE
                    idReciboEnEdicion = nroRecibo;

                    // Cargar datos en los controles
                    lblfact.Text = dr["factura"].ToString();
                    lbloc.Text = dr["orden_de_compra"].ToString();
                    lblprov.Text = dr["proveedor"].ToString();
                    lblnt.Text = dr["nota_credito"].ToString();
                    lbltotal.Text = dr["importe_total"].ToString();
                    dtpfecha.Value = Convert.ToDateTime(dr["fecha"]);
                    txtnpago.Text = dr["nro_pago"].ToString();

                    MessageBox.Show("Recibo cargado. Modifique los valores y luego presione GUARDAR.");
                }
                else
                {
                    MessageBox.Show("No existe un recibo con ese número.");
                }
            }
        }
    }
}
