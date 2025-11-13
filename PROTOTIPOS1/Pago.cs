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
    public partial class Pago : Form
    {
        private int selectedNroOp = 0;
        string cadenaConexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Pago()
        {
            InitializeComponent();
            CargarOrdenesAprobadas();
            CargarBancosComboBox();
            MostrarProximoNumeroPago();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnEliminar, "Eliminar un Pago.");
            toolTip.SetToolTip(bttnGuardar, "Realizar un pago.");
            toolTip.SetToolTip(button4, "Seleccionar una Orden de Pago.");
            toolTip.SetToolTip(button5, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
            toolTip.SetToolTip(bttnCSesion, "Vuelve al formulario anterior.");
        }

        private void Pago_Load(object sender, EventArgs e)
        {
            CargarOrdenesAceptadas();
            CargarOrdenesAprobadas();
            CargarBancos();

            if (Sesion.nivel == 1)
            {
                bttnEliminar.Enabled = false;
                bttnGuardar.Enabled = false;
            }
        }

        private void CargarOrdenesAceptadas()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string query = @"
                SELECT nro_op, nro_factura, total, estado
                FROM Orden_De_Pago
                WHERE estado = 'Aceptado'";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvOrdenes.DataSource = dt;
                    }
                }


                dgvOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvOrdenes.MultiSelect = false;
                dgvOrdenes.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar órdenes: " + ex.Message);
            }
        }

        private void CargarBancos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string query = "SELECT cod_banco, nombre FROM Bancos";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        comboBoxBancos.DataSource = dt;
                        comboBoxBancos.DisplayMember = "nombre";
                        comboBoxBancos.ValueMember = "cod_banco";
                        comboBoxBancos.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar bancos: " + ex.Message);
            }
        }

        private void CargarOrdenesAprobadas()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();

                    string query = @"
                SELECT nro_op, nro_factura, nro_nt, importe_nt, total, estado, cuit_proveedor, razon_social_prov, fecha
                FROM Orden_De_Pago
                WHERE estado = 'Aceptado'"; // Cambia 'Aprobado' si tu base usa otro valor exacto

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvOrdenes.DataSource = dt;

                        //ajustar columnas
                        dgvOrdenes.Columns["nro_op"].HeaderText = "N° OP";
                        dgvOrdenes.Columns["nro_factura"].HeaderText = "N° Factura";
                        dgvOrdenes.Columns["nro_nt"].HeaderText = "N° NT";
                        dgvOrdenes.Columns["importe_nt"].HeaderText = "Importe NT";
                        dgvOrdenes.Columns["total"].HeaderText = "Total";
                        dgvOrdenes.Columns["estado"].HeaderText = "Estado";
                        dgvOrdenes.Columns["cuit_proveedor"].HeaderText = "CUIT";
                        dgvOrdenes.Columns["razon_social_prov"].HeaderText = "Razon Social";
                        dgvOrdenes.Columns["fecha"].HeaderText = "Fecha";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar órdenes de pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarBancosComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string query = "SELECT nombre FROM Bancos";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBoxBancos.Items.Clear();
                        while (reader.Read())
                        {
                            comboBoxBancos.Items.Add(reader["nombre"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los bancos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow == null)
            {
                MessageBox.Show("No seleccionó ninguna orden de pago.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            selectedNroOp = Convert.ToInt32(dgvOrdenes.CurrentRow.Cells["nro_op"].Value);


            lbltotal.Text = dgvOrdenes.CurrentRow.Cells["total"].Value.ToString();
        }


        private void MostrarProximoNumeroPago()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();


                    string query = "SELECT ISNULL(MAX(nro_pago), 0) + 1 FROM Pagos";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int proximoNumero = Convert.ToInt32(cmd.ExecuteScalar());
                        lblnpago.Text = proximoNumero.ToString();
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
            if (selectedNroOp == 0)
            {
                MessageBox.Show("Seleccione primero una orden de pago.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxBancos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un banco.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(lbltotal.Text, out decimal importeTotal))
            {
                MessageBox.Show("El importe total no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult resultado = MessageBox.Show(
            $"¿Está seguro que desea realizar el pago por ${importeTotal:0.00}?",
            "Confirmar Pago",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
);

            if (resultado != DialogResult.Yes)
                return;

            int codBanco = Convert.ToInt32(((DataRowView)comboBoxBancos.SelectedItem)["cod_banco"]);
            DateTime fechaPago = dtpfecha.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();

                    // 2️⃣ Verificar saldo del banco
                    string saldoQuery = "SELECT saldo FROM Bancos WHERE cod_banco = @cod_banco";
                    decimal saldoBanco = 0;

                    using (SqlCommand cmdSaldo = new SqlCommand(saldoQuery, conn))
                    {
                        cmdSaldo.Parameters.AddWithValue("@cod_banco", codBanco);
                        saldoBanco = Convert.ToDecimal(cmdSaldo.ExecuteScalar());
                    }

                    if (saldoBanco < importeTotal)
                    {
                        MessageBox.Show("Saldo insuficiente en el banco seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 3️⃣ Insertar el pago
                    string insertQuery = @"
                INSERT INTO Pagos (fecha, orden_de_pago, cod_banco, importe_total)
                VALUES (@fecha, @nro_op, @cod_banco, @importe_total)";

                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@fecha", fechaPago);
                        cmdInsert.Parameters.AddWithValue("@nro_op", selectedNroOp);
                        cmdInsert.Parameters.AddWithValue("@cod_banco", codBanco);
                        cmdInsert.Parameters.AddWithValue("@importe_total", importeTotal);

                        cmdInsert.ExecuteNonQuery();
                    }

                    // 4️⃣ Actualizar saldo del banco
                    string updateSaldo = "UPDATE Bancos SET saldo = saldo - @importe WHERE cod_banco = @cod_banco";
                    using (SqlCommand cmdUpdate = new SqlCommand(updateSaldo, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@importe", importeTotal);
                        cmdUpdate.Parameters.AddWithValue("@cod_banco", codBanco);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    MessageBox.Show("Pago registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            string nroPago = Microsoft.VisualBasic.Interaction.InputBox(
           "Ingrese el número de pago que desea eliminar:",
           "Eliminar Pago",
                           ""
            ).Trim();

            if (string.IsNullOrEmpty(nroPago))
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();


                    string checkQuery = "SELECT COUNT(*) FROM Pagos WHERE nro_pago = @nro_pago";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@nro_pago", nroPago);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("No se encontró ningún pago con ese número.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }


                    string deleteQuery = "DELETE FROM Pagos WHERE nro_pago = @nro_pago";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@nro_pago", nroPago);
                        deleteCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Pago eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

       
    }
}
