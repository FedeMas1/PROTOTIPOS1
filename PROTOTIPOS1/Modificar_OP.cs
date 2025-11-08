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
    public partial class Modificar_OP : Form
    {
        string cadenaConexion = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public Modificar_OP()
        {
            InitializeComponent();
            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Orden_de_Pago orden_De_Pago = new Orden_de_Pago();
            orden_De_Pago.Show();
            Hide();
        }

        private void btnsesion_Click(object sender, EventArgs e)
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

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string nroOp = txtop.Text.Trim();
            if (string.IsNullOrEmpty(nroOp))
            {
                MessageBox.Show("Ingrese un número de orden.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string query = @"
                SELECT nro_op, nro_factura, nro_nt, importe_nt, importe_factura, total, estado, cuit_proveedor, razon_social_prov, fecha
                FROM Orden_De_Pago
                WHERE nro_op = @nro_op";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@nro_op", nroOp);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvop.DataSource = dt;
                    }
                }

                // Permitir solo edición de ciertas columnas
                foreach (DataGridViewColumn col in dgvop.Columns)
                {
                    col.ReadOnly = true; // primero todo readonly
                }
                dgvop.Columns["nro_nt"].ReadOnly = false;
                dgvop.Columns["importe_nt"].ReadOnly = false;
                dgvop.Columns["estado"].ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar la orden: " + ex.Message);
            }
        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            if (dgvop.CurrentRow == null) return;

            DataGridViewRow row = dgvop.CurrentRow;

            int nroOp = Convert.ToInt32(row.Cells["nro_op"].Value);
            string nroNT = row.Cells["nro_nt"].Value?.ToString();
            string estado = row.Cells["estado"].Value?.ToString();
            decimal importeNT = row.Cells["importe_nt"].Value == null || row.Cells["importe_nt"].Value == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(row.Cells["importe_nt"].Value);

            decimal importeFactura = row.Cells["importe_factura"].Value == null || row.Cells["importe_factura"].Value == DBNull.Value
                                     ? 0
                                     : Convert.ToDecimal(row.Cells["importe_factura"].Value);

            // Recalculamos el total
            decimal total = importeFactura - importeNT;
            row.Cells["total"].Value = total;

            try
            {
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string query = @"
                UPDATE Orden_De_Pago
                SET nro_nt = @nroNT,
                    importe_nt = @importeNT,
                    total = @total,
                    estado = @estado
                WHERE nro_op = @nro_op";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nroNT", string.IsNullOrEmpty(nroNT) ? (object)DBNull.Value : nroNT);
                        cmd.Parameters.Add("@importeNT", SqlDbType.Decimal).Value = importeNT == 0 ? (object)DBNull.Value : importeNT;
                        cmd.Parameters.Add("@total", SqlDbType.Decimal).Value = total;
                        cmd.Parameters.AddWithValue("@estado", estado);
                        cmd.Parameters.AddWithValue("@nro_op", nroOp);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Orden actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (dgvop.CurrentRow == null)
            {
                MessageBox.Show("No se seleccionó ninguna orden de pago para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pedimos confirmación
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea eliminar la orden de pago seleccionada?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                // Obtenemos el nro_op de la fila seleccionada
                int nroOp = Convert.ToInt32(dgvop.CurrentRow.Cells["nro_op"].Value);

                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string query = "DELETE FROM Orden_De_Pago WHERE nro_op = @nro_op";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nro_op", nroOp);
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Orden de pago eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // También removemos la fila del DataGridView
                            dgvop.Rows.Remove(dgvop.CurrentRow);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar la orden de pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la orden de pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
