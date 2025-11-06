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
    public partial class Devolucion : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        private int currentDevolucionId = -1;
        public Devolucion()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();


            toolTip.SetToolTip(bttnEliminar, "Buscar orden de compra.");
            toolTip.SetToolTip(bttnModificar, "Modificar una factura.");
            toolTip.SetToolTip(bttnBOC, "Guardar una factura.");
            toolTip.SetToolTip(button1, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
        }

        private void Devolucion_Load(object sender, EventArgs e)
        {
            SetupDgv();
        }

        private void SetupDgv()
        {
            // Asegurarse de que el DGV tenga las columnas esperadas
            dgvDevolucion.Columns.Clear();
            dgvDevolucion.Columns.Add("Producto", "Producto");
            dgvDevolucion.Columns.Add("Cantidad", "Cantidad");
            dgvDevolucion.Columns.Add("OC", "Orden de Compra");
            dgvDevolucion.Columns.Add("IRM", "Informe de Recepción");
            dgvDevolucion.Columns.Add("NroRemito", "Nro de Remito");


            // La columna Cantidad debe ser editable
            dgvDevolucion.Columns[1].ReadOnly = false;
            // Columnas informativas de origen
            dgvDevolucion.Columns[2].ReadOnly = true;
            dgvDevolucion.Columns[3].ReadOnly = true;
            dgvDevolucion.Columns[4].ReadOnly = true;


            dgvDevolucion.AllowUserToAddRows = false;
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

        private void CargarOCEnGrilla(string idOC)
        {
            // Trae productos y cantidades desde OC_Detalle y llena el DGV.
            // No modifica cantidades; el campo Cantidad queda editable para que el usuario elija.
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT descripcion AS Producto, cantidad FROM OC_Detalle WHERE id_OC = @idOC", cn))
            {
                cmd.Parameters.AddWithValue("@idOC", idOC);
                var dt = new DataTable();
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dt);


                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("La Orden de Compra no contiene productos o no existe.");
                    return;
                }


                dgvDevolucion.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    int rowIndex = dgvDevolucion.Rows.Add();
                    dgvDevolucion.Rows[rowIndex].Cells[0].Value = r["Producto"].ToString();
                    // Por defecto cantidad la dejamos vacía (según tu preferencia)
                    dgvDevolucion.Rows[rowIndex].Cells[1].Value = "";
                    dgvDevolucion.Rows[rowIndex].Cells[2].Value = idOC; // OC informativa
                    dgvDevolucion.Rows[rowIndex].Cells[3].Value = ""; // IRM vacía hasta cargar IRM
                    dgvDevolucion.Rows[rowIndex].Cells[4].Value = ""; // Remito vacía
                }


                // Completar CUIT del proveedor si está en OC_Master

                int idProveedor = 0;
                using (SqlCommand cmd2 = new SqlCommand("SELECT id_Proveedor FROM OC_Master WHERE id_OC = @idOC", cn))
                {
                    cmd2.Parameters.AddWithValue("@idOC", idOC);
                    cn.Open();
                    var result = cmd2.ExecuteScalar();
                    cn.Close();

                    if (result == null)
                    {
                        lblCUIT.Text = "(no encontrado)";
                        return;
                    }

                    idProveedor = Convert.ToInt32(result);
                }

                // 2) Buscar la Razón Social del proveedor en la tabla Proveedores
                using (SqlCommand cmd3 = new SqlCommand("SELECT Razon_Social FROM Proveedores WHERE CUIT = @cuit", cn))
                {
                    cmd3.Parameters.AddWithValue("@cuit", idProveedor);
                    cn.Open();
                    var razon = cmd3.ExecuteScalar();
                    cn.Close();

                    lblCUIT.Text = razon != null ? razon.ToString() : "(sin razón social)";
                }
            }
        }

        private void CargarIRM(string idIRM)
        {
            // Valida que el IRM exista y que corresponda a la OC cargada en el DGV (si aplica)
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT Nro_Remito, Nro_OC FROM IRM_Master WHERE id_IRM = @idIRM", cn))
            {
                cmd.Parameters.AddWithValue("@idIRM", idIRM);
                cn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                    {
                        MessageBox.Show("El IRM no existe.");
                        cn.Close();
                        return;
                    }


                    string nroRem = rdr["Nro_Remito"].ToString();
                    string idOCFromIRM = rdr["Nro_OC"].ToString();
                    cn.Close();


                    // Si ya hay OC cargada en DGV, validar coincidencia
                    string loadedOC = (dgvDevolucion.Rows.Count > 0) ? (dgvDevolucion.Rows[0].Cells[2].Value ?? "").ToString() : "";
                    if (!string.IsNullOrEmpty(loadedOC) && loadedOC != idOCFromIRM)
                    {
                        MessageBox.Show("El IRM no corresponde a la Orden de Compra cargada. No se cargó la información del IRM.");
                        return;
                    }


                    // Completar columnas IRM y NroRemito en el DGV
                    foreach (DataGridViewRow row in dgvDevolucion.Rows)
                    {
                        row.Cells[3].Value = idIRM;
                        row.Cells[4].Value = nroRem;
                    }


                    // Guardar Nro Remito y id_IRM en controles/master al guardar
                    MessageBox.Show("IRM válido. Se completaron IRM y Nro de Remito en el listado.");
                }
            }
        }

        private void bttnBOC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbOC.Text.Trim()))
            {
                MessageBox.Show("Ingrese un número de Orden de Compra.");
                return;
            }


            CargarOCEnGrilla(txtbOC.Text.Trim());
        }

        private void bttnBIRM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbIRM.Text.Trim()))
            {
                MessageBox.Show("Ingrese un número de IRM.");
                return;
            }


            CargarIRM(txtbIRM.Text.Trim());
        }

        private bool ValidarAntesDeGuardar()
        {
            if (dgvDevolucion.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos cargados en el detalle.");
                return false;
            }


            if (string.IsNullOrWhiteSpace(lblCUIT.Text) || lblCUIT.Text == "(no encontrado)")
            {
                MessageBox.Show("No se encontró el CUIT del proveedor. Cargue una OC válida.");
                return false;
            }


            // Verificar que al menos una fila tenga cantidad
            bool anyQty = false;
            foreach (DataGridViewRow row in dgvDevolucion.Rows)
            {
                var val = row.Cells[1].Value;
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    anyQty = true;
                    break;
                }
            }


            if (!anyQty)
            {
                MessageBox.Show("Ingrese la cantidad a devolver en al menos un producto.");
                return false;
            }


            return true;
        }

        private int GuardarMaestroYDetalle(int idDevolucion)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction trx = cn.BeginTransaction();

                try
                {
                    string idOC = dgvDevolucion.Rows.Count > 0 ? dgvDevolucion.Rows[0].Cells[2].Value?.ToString() : "";
                    string idIRM = dgvDevolucion.Rows.Count > 0 ? dgvDevolucion.Rows[0].Cells[3].Value?.ToString() : "";
                    string nroRem = dgvDevolucion.Rows.Count > 0 ? dgvDevolucion.Rows[0].Cells[4].Value?.ToString() : "";
                    string descripcion = rtbDescripcion.Text.Trim();
                    DateTime fecha = DateTime.Now;
                    string proveedor = lblCUIT.Text;

                    // INSERT or UPDATE en Master
                    using (SqlCommand cmdMaster = new SqlCommand(
                        "IF EXISTS(SELECT 1 FROM Devolucion_Master WHERE id_Devolucion = @id) " +
                        "BEGIN " +
                            "UPDATE Devolucion_Master SET id_OC=@idoc, id_IRM=@idirm, descripcion=@desc, fecha=@fecha, proveedor=@prov, Nro_Remito=@nro WHERE id_Devolucion=@id " +
                            "SELECT @id " +
                        "END " +
                        "ELSE BEGIN " +
                            "INSERT INTO Devolucion_Master(id_OC, id_IRM, descripcion, fecha, proveedor, Nro_Remito) " +
                            "VALUES(@idoc,@idirm,@desc,@fecha,@prov,@nro); " +
                            "SELECT SCOPE_IDENTITY(); " +
                        "END", cn, trx))
                    {
                        cmdMaster.Parameters.AddWithValue("@id", idDevolucion);
                        cmdMaster.Parameters.AddWithValue("@idoc", (object)idOC ?? DBNull.Value);
                        cmdMaster.Parameters.AddWithValue("@idirm", string.IsNullOrWhiteSpace(idIRM) ? (object)DBNull.Value : idIRM);
                        cmdMaster.Parameters.AddWithValue("@desc", descripcion);
                        cmdMaster.Parameters.AddWithValue("@fecha", fecha);
                        cmdMaster.Parameters.AddWithValue("@prov", proveedor);
                        cmdMaster.Parameters.AddWithValue("@nro", string.IsNullOrWhiteSpace(nroRem) ? (object)DBNull.Value : nroRem);

                        object result = cmdMaster.ExecuteScalar();
                        idDevolucion = Convert.ToInt32(result);
                    }

                    // Borrar detalle existente
                    using (SqlCommand cmdDel = new SqlCommand("DELETE FROM Devolucion_Detalle WHERE id_Devolucion = @id", cn, trx))
                    {
                        cmdDel.Parameters.AddWithValue("@id", idDevolucion);
                        cmdDel.ExecuteNonQuery();
                    }

                    // Insertar detalle
                    using (SqlCommand cmdIns = new SqlCommand(
                        "INSERT INTO Devolucion_Detalle(id_Devolucion, producto, cantidad) VALUES(@id,@prod,@cant)", cn, trx))
                    {
                        cmdIns.Parameters.Add("@id", SqlDbType.Int);
                        cmdIns.Parameters.Add("@prod", SqlDbType.NVarChar, 500);
                        cmdIns.Parameters.Add("@cant", SqlDbType.Decimal);

                        foreach (DataGridViewRow row in dgvDevolucion.Rows)
                        {
                            cmdIns.Parameters["@id"].Value = idDevolucion;
                            cmdIns.Parameters["@prod"].Value = row.Cells[0].Value?.ToString() ?? "";
                            cmdIns.Parameters["@cant"].Value = decimal.TryParse(row.Cells[1].Value?.ToString(), out decimal c) ? c : 0;
                            cmdIns.ExecuteNonQuery();
                        }
                    }

                    trx.Commit();
                    return idDevolucion;
                }
                catch (Exception ex)
                {
                    try { trx.Rollback(); } catch { }
                    MessageBox.Show("Error al guardar: " + ex.Message);
                    return -1;
                }
            }
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarAntesDeGuardar())
                return;

            int nuevoId = GuardarMaestroYDetalle(currentDevolucionId);

            if (nuevoId > 0)
            {
                currentDevolucionId = nuevoId;
                MessageBox.Show("✅ Devolución guardada correctamente.");
            }
            LimpiarFormulario();
        }


        private bool CargarDevolucionPorId(int id)
        {
            // Carga master y detalle al formulario
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT id_Devolucion, id_OC, id_IRM, descripcion, fecha, proveedor, Nro_Remito FROM Devolucion_Master WHERE id_Devolucion = @id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read()) return false;


                        lblNDev.Text = rdr["id_Devolucion"].ToString();
                        txtbOC.Text = rdr["id_OC"].ToString();
                        txtbIRM.Text = rdr["id_IRM"].ToString();
                        rtbDescripcion.Text = rdr["descripcion"].ToString();
                        lblCUIT.Text = rdr["proveedor"].ToString();
                        // Nro remito se mostrará en el DGV
                    }
                }


                // Cargar detalle
                using (SqlCommand cmdDet = new SqlCommand("SELECT producto, cantidad FROM Devolucion_Detalle WHERE id_Devolucion = @id", cn))
                {
                    cmdDet.Parameters.AddWithValue("@id", id);
                    using (var da = new SqlDataAdapter(cmdDet))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvDevolucion.Rows.Clear();
                        foreach (DataRow r in dt.Rows)
                        {
                            int idx = dgvDevolucion.Rows.Add();
                            dgvDevolucion.Rows[idx].Cells[0].Value = r["producto"].ToString();
                            dgvDevolucion.Rows[idx].Cells[1].Value = r["cantidad"].ToString();
                            dgvDevolucion.Rows[idx].Cells[2].Value = txtbOC.Text; // OC asociada
                            dgvDevolucion.Rows[idx].Cells[3].Value = txtbIRM.Text; // IRM asociada
                                                                                   // NroRemito: se puede cargar consultando master
                        }
                    }
                }
                // Cargar Nro Remito al DGV desde master
                using (SqlCommand cmdRem = new SqlCommand("SELECT Nro_Remito FROM Devolucion_Master WHERE id_Devolucion = @id", cn))
                {
                    cmdRem.Parameters.AddWithValue("@id", id);
                    var nro = cmdRem.ExecuteScalar();
                    if (nro != null)
                    {
                        foreach (DataGridViewRow row in dgvDevolucion.Rows)
                            row.Cells[4].Value = nro.ToString();
                    }
                }


                cn.Close();
                return true;
            }
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de devolución a modificar:", "Modificar devolución");
            if (!int.TryParse(input, out int id))
            {
                MessageBox.Show("Número inválido.");
                return;
            }


            if (CargarDevolucionPorId(id))
            {
                currentDevolucionId = id;
                lblNDev.Text = id.ToString();
                MessageBox.Show("Devolución cargada para modificar.");
            }
            else
            {
                MessageBox.Show("No se encontró la devolución especificada.");
            }
        }

        private bool EliminarDevolucion(int id)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                SqlTransaction trx = cn.BeginTransaction();
                try
                {
                    using (SqlCommand cmdDelDet = new SqlCommand("DELETE FROM Devolucion_Detalle WHERE id_Devolucion = @id", cn, trx))
                    {
                        cmdDelDet.Parameters.AddWithValue("@id", id);
                        cmdDelDet.ExecuteNonQuery();
                    }


                    using (SqlCommand cmdDelMaster = new SqlCommand("DELETE FROM Devolucion_Master WHERE id_Devolucion = @id", cn, trx))
                    {
                        cmdDelMaster.Parameters.AddWithValue("@id", id);
                        cmdDelMaster.ExecuteNonQuery();
                    }


                    trx.Commit();
                    return true;
                }
                catch
                {
                    try { trx.Rollback(); } catch { }
                    return false;
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            // Según tu elección, el botón está siempre habilitado pero valida antes
            if (currentDevolucionId == -1)
            {
                MessageBox.Show("No hay ninguna devolución cargada para eliminar. Use Modificar para cargar una devolución existente.");
                return;
            }


            var resp = MessageBox.Show("¿Confirma que desea eliminar esta devolución?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (resp != DialogResult.Yes) return;


            if (EliminarDevolucion(currentDevolucionId))
            {
                MessageBox.Show("Devolución eliminada.");
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al eliminar la devolución.");
            }
        }

        private void LimpiarFormulario()
        {
            currentDevolucionId = -1;
            lblNDev.Text = "(nuevo)";
            lblCUIT.Text = "";
            txtbOC.Text = "";
            txtbIRM.Text = "";
            rtbDescripcion.Text = "";
            dgvDevolucion.Rows.Clear();
        }

    }
}
