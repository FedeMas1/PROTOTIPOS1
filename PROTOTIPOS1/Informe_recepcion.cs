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
    public partial class Informe_recepcion : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        private bool modoModificar = false; 
        private int idIRMActual = 0;        
        public Informe_recepcion()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnEliminar, "Eliminar un informe de recepcion.");
            toolTip.SetToolTip(bttnModificar, "Modificar un informe de recepcion.");
            toolTip.SetToolTip(bttnBuscar, "Buscar una orden de compra.");
            toolTip.SetToolTip(bttnGuardar, "Guardar un informe de recepcion.");
            toolTip.SetToolTip(button1, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
        }

        private void Informe_recepcion_Load(object sender, EventArgs e)
        {
            dgvIRM.Columns.Clear();

            dgvIRM.Columns.Add("CUIT", "CUIT Proveedor");
            dgvIRM.Columns.Add("Proveedor", "Razón Social");
            dgvIRM.Columns.Add("Descripción", "Descripción");
            dgvIRM.Columns.Add("Cantidad_Pedida", "Cant. Solicitada");
            dgvIRM.Columns.Add("Cantidad_Real", "Cant. Recibida");
            dgvIRM.Columns.Add("Fecha_Vencimiento", "Fecha Vencimiento");
            dgvIRM.Columns.Add("U_Buen_Estado", "U. Buen Estado");
            dgvIRM.Columns.Add("U_Mal_Estado", "U. Mal Estado");
        }


        private void LimpiarCampos()
        {
            lblCInforme.Text = ""; 
            dtp1.Value = DateTime.Now;
            txtBReceptor.Clear();
            txtbNRemito.Clear();
            txtBOC.Clear();
            rtxtbObservaciones.Clear();
            dgvIRM.Rows.Clear();
        }


        private bool ValidarIRM()
        {
            if (string.IsNullOrWhiteSpace(txtBReceptor.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del receptor.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtbNRemito.Text))
            {
                MessageBox.Show("Debe ingresar el número de remito.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBOC.Text))
            {
                MessageBox.Show("Debe ingresar el número de Orden de Compra.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dgvIRM.Rows.Count == 0)
            {
                MessageBox.Show("Debe cargar al menos un producto en la IRM.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
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
            if (string.IsNullOrWhiteSpace(txtBOC.Text))
            {
                MessageBox.Show("Debe ingresar un número de Orden de Compra.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dgvIRM.Rows.Clear(); // Limpiar el DGV antes de cargar

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Validar que la OC existe
                    string queryOC = @"
                                        SELECT OC.id_Proveedor AS IdProv, P.Razon_Social, D.descripcion, D.cantidad
                                        FROM OC_Master AS OC
                                        INNER JOIN Proveedores AS P ON OC.id_Proveedor = P.CUIT
                                        INNER JOIN OC_Detalle AS D ON OC.id_OC = D.id_OC
                                        WHERE OC.id_OC = @nroOC";
                    SqlCommand cmd = new SqlCommand(queryOC, con);
                    cmd.Parameters.AddWithValue("@nroOC", txtBOC.Text);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        MessageBox.Show("No se encontró la Orden de Compra ingresada.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    while (reader.Read())
                    {
                        dgvIRM.Rows.Add(
                            reader["IdProv"].ToString(),    
                            reader["Razon_Social"].ToString(),    
                            reader["descripcion"].ToString(),   
                            reader["cantidad"].ToString(),        
                            "",                                   // Cantidad_Real (manual)
                            "",                                   // Fecha_Vencimiento (manual)
                            "",                                   // U_buen_estado (manual)
                            ""                                    // U_mal_estado (manual)
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar la OC: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de IRM a modificar:", "Modificar IRM");

            if (!int.TryParse(input, out int nroIRM))
            {
                MessageBox.Show("Número de IRM inválido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // 1) Traer IRM_Master
                    string queryMaster = "SELECT * FROM IRM_Master WHERE id_IRM = @idIRM";
                    SqlCommand cmdMaster = new SqlCommand(queryMaster, con);
                    cmdMaster.Parameters.AddWithValue("@idIRM", nroIRM);

                    SqlDataReader readerMaster = cmdMaster.ExecuteReader();

                    if (!readerMaster.HasRows)
                    {
                        readerMaster.Close();
                        MessageBox.Show("No se encontró el IRM ingresado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    readerMaster.Read();
                    idIRMActual = nroIRM;
                    lblCInforme.Text = readerMaster["id_IRM"].ToString();
                    dtp1.Value = Convert.ToDateTime(readerMaster["fecha"]);
                    txtBReceptor.Text = readerMaster["receptor"].ToString();
                    txtBOC.Text = readerMaster["Nro_OC"].ToString();
                    rtxtbObservaciones.Text = readerMaster["observaciones"].ToString();
                    txtbNRemito.Text = readerMaster["Nro_Remito"].ToString();

                    // ❗ CERRAR READER ANTES DEL SIGUIENTE SELECT
                    readerMaster.Close();

                    // 2) Traer IRM_Detalle
                    dgvIRM.Rows.Clear();

                    string queryDetalle = "SELECT * FROM IRM_Detalle WHERE id_IRM = @idIRM";
                    SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con);
                    cmdDetalle.Parameters.AddWithValue("@idIRM", nroIRM);

                    SqlDataReader readerDet = cmdDetalle.ExecuteReader();

                    while (readerDet.Read())
                    {
                        dgvIRM.Rows.Add(
                            readerDet["CUIT"].ToString(),
                            readerDet["proveedor"].ToString(),
                            readerDet["descripcion"].ToString(),
                            readerDet["cantidad_Pedida"].ToString(),
                            readerDet["cantidad_Real"].ToString(),
                            readerDet["fecha_Vencimiento"].ToString(),
                            readerDet["U_buen_estado"].ToString(),
                            readerDet["U_mal_estado"].ToString()
                        );
                    }

                    readerDet.Close();

                    modoModificar = true;
                    MessageBox.Show("IRM cargada. Puede realizar cambios y presionar Guardar.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el IRM: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            if (!modoModificar || idIRMActual == 0)
            {
                MessageBox.Show("Primero debe cargar un IRM usando Modificar para poder eliminarlo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Está seguro que desea eliminar este IRM?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                  
                    string queryEliminarDetalle = "DELETE FROM IRM_Detalle WHERE id_IRM = @idIRM";
                    SqlCommand cmdDetalle = new SqlCommand(queryEliminarDetalle, con);
                    cmdDetalle.Parameters.AddWithValue("@idIRM", idIRMActual);
                    cmdDetalle.ExecuteNonQuery();
                  
                    string queryEliminarMaster = "DELETE FROM IRM_Master WHERE id_IRM = @idIRM";
                    SqlCommand cmdMaster = new SqlCommand(queryEliminarMaster, con);
                    cmdMaster.Parameters.AddWithValue("@idIRM", idIRMActual);
                    cmdMaster.ExecuteNonQuery();

                    MessageBox.Show("IRM eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                    modoModificar = false;
                    idIRMActual = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el IRM: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarIRM()) return;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    using (SqlTransaction tx = cn.BeginTransaction())
                    {
                        try
                        {
                            if (!modoModificar)
                            {
                                // INSERT MASTER
                                string sqlMaster = @"INSERT INTO IRM_Master (fecha, observaciones, receptor, Nro_OC, Nro_Remito)
                                             VALUES (@fecha, @obs, @receptor, @oc, @remito);
                                             SELECT CAST(SCOPE_IDENTITY() AS int);";
                                int idIRM;
                                using (SqlCommand cmdM = new SqlCommand(sqlMaster, cn, tx))
                                {
                                    cmdM.Parameters.AddWithValue("@fecha", dtp1.Value);
                                    cmdM.Parameters.AddWithValue("@obs", string.IsNullOrWhiteSpace(rtxtbObservaciones.Text) ? (object)DBNull.Value : rtxtbObservaciones.Text);
                                    cmdM.Parameters.AddWithValue("@receptor", txtBReceptor.Text);
                                    cmdM.Parameters.AddWithValue("@oc", txtBOC.Text);
                                    cmdM.Parameters.AddWithValue("@remito", txtbNRemito.Text);
                                    idIRM = Convert.ToInt32(cmdM.ExecuteScalar());
                                }

                                // INSERT DETALLES
                                foreach (DataGridViewRow row in dgvIRM.Rows)
                                {
                                    if (row.IsNewRow) continue;

                                    string sqlDet = @"INSERT INTO IRM_Detalle
                                             (id_IRM, CUIT, proveedor, descripcion, fecha_Vencimiento, cantidad_Pedida, cantidad_Real, U_buen_estado, U_mal_estado)
                                             VALUES (@idIRM, @cuit, @prov, @desc, @fv, @cp, @cr, @ube, @ume)";
                                    using (SqlCommand cmdD = new SqlCommand(sqlDet, cn, tx))
                                    {
                                        cmdD.Parameters.AddWithValue("@idIRM", idIRM);
                                        cmdD.Parameters.AddWithValue("@cuit", row.Cells["CUIT"].Value?.ToString() ?? "");
                                        cmdD.Parameters.AddWithValue("@prov", row.Cells["Proveedor"].Value?.ToString() ?? "");
                                        cmdD.Parameters.AddWithValue("@desc", row.Cells["Descripción"].Value?.ToString() ?? "");
                                        // fecha
                                        if (DateTime.TryParse(row.Cells["Fecha_Vencimiento"].Value?.ToString(), out DateTime fv))
                                            cmdD.Parameters.AddWithValue("@fv", fv);
                                        else
                                            cmdD.Parameters.AddWithValue("@fv", DBNull.Value);
                                        // enteros
                                        cmdD.Parameters.AddWithValue("@cp", int.TryParse(row.Cells["Cantidad_Pedida"].Value?.ToString(), out int tmpCp) ? tmpCp : 0);
                                        cmdD.Parameters.AddWithValue("@cr", int.TryParse(row.Cells["Cantidad_Real"].Value?.ToString(), out int tmpCr) ? tmpCr : 0);
                                        cmdD.Parameters.AddWithValue("@ube", int.TryParse(row.Cells["U_Buen_Estado"].Value?.ToString(), out int tmpUbe) ? tmpUbe : 0);
                                        cmdD.Parameters.AddWithValue("@ume", int.TryParse(row.Cells["U_Mal_Estado"].Value?.ToString(), out int tmpUme) ? tmpUme : 0);

                                        cmdD.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                // UPDATE MASTER
                                string sqlUpdateMaster = @"UPDATE IRM_Master
                                                   SET fecha = @fecha,
                                                       observaciones = @obs,
                                                       receptor = @receptor,
                                                       Nro_OC = @oc,
                                                       Nro_Remito = @remito
                                                   WHERE id_IRM = @idIRM";
                                using (SqlCommand cmdUM = new SqlCommand(sqlUpdateMaster, cn, tx))
                                {
                                    cmdUM.Parameters.AddWithValue("@fecha", dtp1.Value);
                                    cmdUM.Parameters.AddWithValue("@obs", string.IsNullOrWhiteSpace(rtxtbObservaciones.Text) ? (object)DBNull.Value : rtxtbObservaciones.Text);
                                    cmdUM.Parameters.AddWithValue("@receptor", txtBReceptor.Text);
                                    cmdUM.Parameters.AddWithValue("@oc", txtBOC.Text);
                                    cmdUM.Parameters.AddWithValue("@remito", txtbNRemito.Text);
                                    cmdUM.Parameters.AddWithValue("@idIRM", idIRMActual);
                                    cmdUM.ExecuteNonQuery();
                                }

                                // DELETE OLD DETAILS
                                using (SqlCommand cmdDel = new SqlCommand("DELETE FROM IRM_Detalle WHERE id_IRM = @idIRM", cn, tx))
                                {
                                    cmdDel.Parameters.AddWithValue("@idIRM", idIRMActual);
                                    cmdDel.ExecuteNonQuery();
                                }

                                // INSERT NEW DETAILS
                                foreach (DataGridViewRow row in dgvIRM.Rows)
                                {
                                    if (row.IsNewRow) continue;

                                    string sqlDet = @"INSERT INTO IRM_Detalle
                                             (id_IRM, CUIT, proveedor, descripcion, fecha_Vencimiento, cantidad_Pedida, cantidad_Real, U_buen_estado, U_mal_estado)
                                             VALUES (@idIRM, @cuit, @prov, @desc, @fv, @cp, @cr, @ube, @ume)";
                                    using (SqlCommand cmdD = new SqlCommand(sqlDet, cn, tx))
                                    {
                                        cmdD.Parameters.AddWithValue("@idIRM", idIRMActual);
                                        cmdD.Parameters.AddWithValue("@cuit", row.Cells["CUIT"].Value?.ToString() ?? "");
                                        cmdD.Parameters.AddWithValue("@prov", row.Cells["Proveedor"].Value?.ToString() ?? "");
                                        cmdD.Parameters.AddWithValue("@desc", row.Cells["Descripción"].Value?.ToString() ?? "");
                                        if (DateTime.TryParse(row.Cells["Fecha_Vencimiento"].Value?.ToString(), out DateTime fv))
                                            cmdD.Parameters.AddWithValue("@fv", fv);
                                        else
                                            cmdD.Parameters.AddWithValue("@fv", DBNull.Value);
                                        cmdD.Parameters.AddWithValue("@cp", int.TryParse(row.Cells["Cantidad_Pedida"].Value?.ToString(), out int tmpCp) ? tmpCp : 0);
                                        cmdD.Parameters.AddWithValue("@cr", int.TryParse(row.Cells["Cantidad_Real"].Value?.ToString(), out int tmpCr) ? tmpCr : 0);
                                        cmdD.Parameters.AddWithValue("@ube", int.TryParse(row.Cells["U_Buen_Estado"].Value?.ToString(), out int tmpUbe) ? tmpUbe : 0);
                                        cmdD.Parameters.AddWithValue("@ume", int.TryParse(row.Cells["U_Mal_Estado"].Value?.ToString(), out int tmpUme) ? tmpUme : 0);

                                        cmdD.ExecuteNonQuery();
                                    }
                                }
                            }

                            tx.Commit();
                            MessageBox.Show(modoModificar ? "IRM modificada correctamente." : "IRM guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // reset
                            LimpiarCampos();
                            modoModificar = false;
                            idIRMActual = 0;
                        }
                        catch 
                        {
                            tx.Rollback();
                            throw; // sube al catch exterior
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la IRM: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
