using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROTOTIPOS1
{
    public partial class Ped_cotizacion : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        int idCotizacionActual = 0;
        public Ped_cotizacion()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnEliminar, "Eliminar una solicitud de compra.");
            toolTip.SetToolTip(bttnBuscar, "Buscar una solicitud de compra.");
            toolTip.SetToolTip(bttnGuardar, "Guardar una solicitud de compra.");
            toolTip.SetToolTip(button1, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
            toolTip.SetToolTip(bttnCargar, "Carga en el dgv solicitudes de compra y pedidos de reaprovisionamiento existemtes y de estado aprobado");
        }

        private void Ped_cotizacion_Load(object sender, EventArgs e)
        {
            CargarRubros();

            dgvTemporal.Rows.Clear();
            dgvTemporal.Columns.Clear();
            dgvPCot.Rows.Clear();
            dgvPCot.Columns.Clear();

            dateTimePicker1.Value = DateTime.Now;

            // ----- Crear columnas REALES del dgvPCot -----
            dgvPCot.Columns.Add("Origen", "Origen");
            dgvPCot.Columns.Add("IdOrigen", "ID");
            dgvPCot.Columns.Add("codigo_producto", "Código");
            dgvPCot.Columns.Add("descripcion", "Descripción");
            dgvPCot.Columns.Add("cantidad_a_cotizar", "Cantidad");
            dgvPCot.Columns.Add("marca", "Marca");

            DataGridViewButtonColumn colEliminar = new DataGridViewButtonColumn();
            colEliminar.Name = "Eliminar";
            colEliminar.HeaderText = "Eliminar";
            colEliminar.Text = "X";
            colEliminar.UseColumnTextForButtonValue = true;
            dgvPCot.Columns.Add(colEliminar);




            cbSolicitado.Checked = true;

            if (Sesion.nivel == 1)
            {
                bttnModificar.Enabled = false;
                bttnEliminar.Enabled = false;
                bttnGuardar.Enabled = false;
            }

        }


        private void CargarRubros()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id_Rubro, Descripcion FROM Rubros";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbRubros.DataSource = dt;
                cmbRubros.DisplayMember = "Descripcion";
                cmbRubros.ValueMember = "id_Rubro";
            }
        }


        private void bttnCargar_Click(object sender, EventArgs e)
        {
            if (cmbRubros.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un rubro.");
                return;
            }

            int idRubro = Convert.ToInt32(cmbRubros.SelectedValue);

            dgvTemporal.Columns.Clear();
            dgvTemporal.Rows.Clear();

            dgvTemporal.Columns.Add("Origen", "Origen");
            dgvTemporal.Columns.Add("IdOrigen", "ID");

            DataGridViewCheckBoxColumn colSel = new DataGridViewCheckBoxColumn();
            colSel.Name = "Seleccionar";
            colSel.HeaderText = "Seleccionar";
            dgvTemporal.Columns.Add(colSel);

            string sql =
                "SELECT 'SC' AS Origen, id_Sc AS ID " +
                "FROM Sc_Master WHERE estado = 'Aprobado' AND rubro = @rubro " +
                "UNION ALL " +
                "SELECT 'PR' AS Origen, id_PR AS ID " +
                "FROM PR_Master WHERE estado = 'Aprobado' AND rubro = @rubro;";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@rubro", idRubro);

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dgvTemporal.Rows.Add(
                            reader["Origen"].ToString(),
                            reader["ID"].ToString()
                        );
                    }
                }
            }
        }

        private void CargarDetalleSC(int idSC)
        {
            string sql = "SELECT codigo_bien_de_uso, descripcion, cantidad_a_pedir, marca " +
                 "FROM SC_Detalle WHERE id_sc = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", idSC);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string codigo = dr["codigo_bien_de_uso"]?.ToString() ?? "";
                        string descripcion = dr["descripcion"]?.ToString() ?? "";
                        string cantidad = dr["cantidad_a_pedir"]?.ToString() ?? "";
                        string marca = dr["marca"]?.ToString() ?? "";

                        // Evitar duplicados: mismo Origen + IdOrigen + Codigo
                        bool existe = false;
                        foreach (DataGridViewRow fila in dgvPCot.Rows)
                        {
                            if (fila.IsNewRow) continue;
                            if (fila.Cells["Origen"].Value?.ToString() == "SC" &&
                                fila.Cells["IdOrigen"].Value?.ToString() == idSC.ToString() &&
                                fila.Cells["codigo_producto"].Value?.ToString() == codigo)
                            {
                                existe = true;
                                break;
                            }
                        }

                        if (!existe)
                        {
                            dgvPCot.Rows.Add(
                                "SC",
                                idSC,
                                codigo,
                                descripcion,
                                cantidad,
                                marca
                            );
                        }
                    }
                }
            }
        }

        private void CargarDetallePR(int idPR)
        {
            string sql = "SELECT codigo_producto, descripcion, cantidad_a_pedir, marca " +
                 "FROM PR_Detalle WHERE id_pr = @id";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", idPR);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string codigo = dr["codigo_producto"]?.ToString() ?? "";
                        string descripcion = dr["descripcion"]?.ToString() ?? "";
                        string cantidad = dr["cantidad_a_pedir"]?.ToString() ?? "";
                        string marca = dr["marca"]?.ToString() ?? "";

                        // Evitar duplicados: mismo Origen + IdOrigen + Codigo
                        bool existe = false;
                        foreach (DataGridViewRow fila in dgvPCot.Rows)
                        {
                            if (fila.IsNewRow) continue;
                            if (fila.Cells["Origen"].Value?.ToString() == "PR" &&
                                fila.Cells["IdOrigen"].Value?.ToString() == idPR.ToString() &&
                                fila.Cells["codigo_producto"].Value?.ToString() == codigo)
                            {
                                existe = true;
                                break;
                            }
                        }

                        if (!existe)
                        {
                            dgvPCot.Rows.Add(
                                "PR",
                                idPR,
                                codigo,
                                descripcion,
                                cantidad,
                                marca
                            );
                        }
                    }
                }
            }
        }

        private void dgvTemporal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvTemporal.Columns[e.ColumnIndex].Name != "Seleccionar") return;

            // Forzar commit del cambio en el checkbox para leer el valor real
            dgvTemporal.EndEdit();

            var checkCell = dgvTemporal.Rows[e.RowIndex].Cells["Seleccionar"] as DataGridViewCheckBoxCell;
            bool marcado = Convert.ToBoolean(checkCell.Value ?? false);

            string origen = dgvTemporal.Rows[e.RowIndex].Cells["Origen"].Value?.ToString();
            int id = Convert.ToInt32(dgvTemporal.Rows[e.RowIndex].Cells["IdOrigen"].Value);

            if (marcado)
            {
                // Agregar (append) sin borrar lo existente
                if (origen == "SC")
                    CargarDetalleSC(id);
                else if (origen == "PR")
                    CargarDetallePR(id);
            }
            else
            {
                // Quitar todas las filas en dgvPCot que pertenezcan a ese origen+id
                for (int i = dgvPCot.Rows.Count - 1; i >= 0; i--)
                {
                    var fila = dgvPCot.Rows[i];
                    if (fila.IsNewRow) continue;
                    string o = fila.Cells["Origen"].Value?.ToString();
                    string idOrigen = fila.Cells["IdOrigen"].Value?.ToString();
                    if (o == origen && idOrigen == id.ToString())
                    {
                        dgvPCot.Rows.RemoveAt(i);
                    }
                }
            }
        }  

        private void dgvPCot_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvPCot.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                dgvPCot.Rows.RemoveAt(e.RowIndex);
            }
        }


        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvPCot.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos cargados para guardar el pedido de cotización.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación antes de guardar
            DialogResult confirm = MessageBox.Show("¿Desea guardar este Pedido de Cotización?", "Confirmar Guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            int cantPR = 0;
            int cantSC = 0;

            foreach (DataGridViewRow fila in dgvPCot.Rows)
            {
                if (fila.IsNewRow) continue;
                string origen = fila.Cells["Origen"].Value?.ToString();
                if (origen == "PR") cantPR++;
                if (origen == "SC") cantSC++;
            }

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    int idPcot;

                    string estadoActual = "Solicitado";

                    if (cbAprobado.Checked)
                        estadoActual = "Aprobado";
                    else if (cbDeenegado.Checked)
                        estadoActual = "Denegado";
                    else if (cbCotizado.Checked)
                        estadoActual = "Cotizado";
                    else if (cbSolicitado.Checked)
                        estadoActual = "Solicitado";

                    if (idCotizacionActual == 0)
                    {
                        // Insertar master
                        string insertMaster = @"INSERT INTO Pcot_Master (fecha, rubro, estado, cantidad_SC_Adjuntos, cantidad_PR_adjuntos)
                                        OUTPUT INSERTED.id_Pcot
                                        VALUES (@Fecha, @Rubro, @Estado, @CantSC, @CantPR)";
                        using (SqlCommand cmdMaster = new SqlCommand(insertMaster, conexion, transaccion))
                        {
                            cmdMaster.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value);
                            cmdMaster.Parameters.AddWithValue("@Rubro", (int)cmbRubros.SelectedValue);
                            cmdMaster.Parameters.AddWithValue("@Estado", estadoActual);
                            cmdMaster.Parameters.AddWithValue("@CantSC", cantSC);
                            cmdMaster.Parameters.AddWithValue("@CantPR", cantPR);
                            idPcot = (int)cmdMaster.ExecuteScalar();
                            idCotizacionActual = idPcot;
                        }
                    }
                    else
                    {
                        // Actualizar master
                        string updateMaster = @"UPDATE Pcot_Master 
                                        SET fecha = @Fecha, rubro = @Rubro, estado = @Estado,
                                            cantidad_SC_Adjuntos = @CantSC, cantidad_PR_adjuntos = @CantPR
                                        WHERE id_Pcot = @Id";
                        using (SqlCommand cmdMaster = new SqlCommand(updateMaster, conexion, transaccion))
                        {
                            cmdMaster.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value);
                            cmdMaster.Parameters.AddWithValue("@Rubro", (int)cmbRubros.SelectedValue);
                            cmdMaster.Parameters.AddWithValue("@Estado", estadoActual);
                            cmdMaster.Parameters.AddWithValue("@CantSC", cantSC);
                            cmdMaster.Parameters.AddWithValue("@CantPR", cantPR);
                            cmdMaster.Parameters.AddWithValue("@Id", idCotizacionActual);
                            cmdMaster.ExecuteNonQuery();
                        }

                        // Borrar detalles anteriores
                        string deleteDetalle = "DELETE FROM Pcot_Detalle WHERE id_Pcot = @Id";
                        using (SqlCommand cmdDelete = new SqlCommand(deleteDetalle, conexion, transaccion))
                        {
                            cmdDelete.Parameters.AddWithValue("@Id", idCotizacionActual);
                            cmdDelete.ExecuteNonQuery();
                        }

                        idPcot = idCotizacionActual;
                    }

                    // Insertar detalles
                    foreach (DataGridViewRow row in dgvPCot.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string insertDetalle = @"INSERT INTO Pcot_Detalle 
                                         (id_Pcot, codigo_producto, descripcion, cantidad_a_cotizar, marca, origen_tipo, origen_id)
                                         VALUES (@Id, @Codigo, @Descripcion, @Cantidad, @Marca, @OrigenTipo, @OrigenId)";
                        using (SqlCommand cmdDetalle = new SqlCommand(insertDetalle, conexion, transaccion))
                        {
                            cmdDetalle.Parameters.AddWithValue("@Id", idPcot);
                            cmdDetalle.Parameters.AddWithValue("@Codigo", row.Cells["codigo_producto"].Value ?? DBNull.Value);
                            cmdDetalle.Parameters.AddWithValue("@Descripcion", row.Cells["descripcion"].Value ?? DBNull.Value);
                            cmdDetalle.Parameters.AddWithValue("@Cantidad", row.Cells["cantidad_a_cotizar"].Value ?? 0);
                            cmdDetalle.Parameters.AddWithValue("@Marca", row.Cells["marca"].Value ?? DBNull.Value);
                            cmdDetalle.Parameters.AddWithValue("@OrigenTipo", row.Cells["Origen"].Value ?? DBNull.Value);
                            cmdDetalle.Parameters.AddWithValue("@OrigenId", row.Cells["IdOrigen"].Value ?? DBNull.Value);
                            cmdDetalle.ExecuteNonQuery();
                        }
                    }

                    transaccion.Commit();
                    MessageBox.Show("Pedido de cotización guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    MessageBox.Show("Error al guardar el pedido de cotización: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de Pcot a buscar:", "Buscar Pcot", "");

            if (!int.TryParse(input, out int idBuscado))
            {
                MessageBox.Show("Número inválido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Cargar datos del master
                SqlCommand cmdMaster = new SqlCommand("SELECT * FROM Pcot_Master WHERE id_Pcot = @Id", conn);
                cmdMaster.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataReader reader = cmdMaster.ExecuteReader();

                if (reader.Read())
                {
                    idCotizacionActual = idBuscado;
                    dateTimePicker1.Value = Convert.ToDateTime(reader["fecha"]);
                    cmbRubros.SelectedValue = (int)reader["rubro"];
                    string estado = reader["estado"].ToString();

                    cbSolicitado.Checked = estado == "Solicitado";
                    cbAprobado.Checked = estado == "Aprobado";
                    cbDeenegado.Checked = estado == "Denegado";
                    cbCotizado.Checked = estado == "Cotizado";
                }
                else
                {
                    MessageBox.Show("No se encontró el pedido de cotización.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    return;
                }
                reader.Close();

                // Limpiar columnas antes de cargar detalles
                dgvPCot.DataSource = null;
                dgvPCot.Columns.Clear();

                // Cargar datos de detalle
                SqlDataAdapter da = new SqlDataAdapter("SELECT codigo_producto, descripcion, cantidad_a_cotizar, marca, origen_tipo, origen_id FROM Pcot_Detalle WHERE id_Pcot = @Id", conn);
                da.SelectCommand.Parameters.AddWithValue("@Id", idBuscado);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPCot.DataSource = dt;

                // Bloquear edición completa del DataGridView
                dgvPCot.ReadOnly = true;
                foreach (DataGridViewColumn col in dgvPCot.Columns)
                {
                    col.ReadOnly = true;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // Bloquear controles del formulario para solo lectura
                dateTimePicker1.Enabled = false;
                cmbRubros.Enabled = false;
                cbSolicitado.Enabled = false;
                cbAprobado.Enabled = false;
                cbDeenegado.Enabled = false;
                cbCotizado.Enabled = false;
                bttnEliminar.Enabled = false;
                bttnGuardar.Enabled = false;
            }
        }

        private void bttnModificar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de Pcot a modificar:", "Modificar Pcot", "");
            if (!int.TryParse(input, out int idBuscado))
            {
                MessageBox.Show("Número inválido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            idCotizacionActual = idBuscado;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Cargar datos del master
                SqlCommand cmdMaster = new SqlCommand("SELECT * FROM Pcot_Master WHERE id_Pcot = @Id", conn);
                cmdMaster.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataReader reader = cmdMaster.ExecuteReader();

                if (reader.Read())
                {
                    dateTimePicker1.Value = Convert.ToDateTime(reader["fecha"]);
                    cmbRubros.SelectedValue = (int)reader["rubro"];
                    string estado = reader["estado"].ToString();
                    cbSolicitado.Checked = estado == "Solicitado";
                    cbAprobado.Checked = estado == "Aprobado";
                    cbDeenegado.Checked = estado == "Denegado";
                    cbCotizado.Checked = estado == "Cotizado";
                }
                else
                {
                    MessageBox.Show("No se encontró el pedido de cotización.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    return;
                }
                reader.Close();

                // Limpiar y preparar DataGridView
                dgvPCot.DataSource = null;
                dgvPCot.Rows.Clear();

                if (dgvPCot.Columns.Count == 0)
                {
                    dgvPCot.Columns.Add("Origen", "Origen");
                    dgvPCot.Columns.Add("IdOrigen", "IdOrigen");
                    dgvPCot.Columns.Add("codigo_producto", "Código");
                    dgvPCot.Columns.Add("descripcion", "Descripción");
                    dgvPCot.Columns.Add("cantidad_a_cotizar", "Cantidad");
                    dgvPCot.Columns.Add("marca", "Marca");

                    DataGridViewButtonColumn bttnEliminar = new DataGridViewButtonColumn
                    {
                        Name = "Eliminar",
                        HeaderText = "Eliminar",
                        Text = "Eliminar",
                        UseColumnTextForButtonValue = true
                    };
                    dgvPCot.Columns.Add(bttnEliminar);
                }

                // Cargar detalles
                SqlCommand cmdDetalle = new SqlCommand("SELECT * FROM Pcot_Detalle WHERE id_Pcot = @Id", conn);
                cmdDetalle.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataReader readerDetalle = cmdDetalle.ExecuteReader();

                while (readerDetalle.Read())
                {
                    dgvPCot.Rows.Add(
                        readerDetalle["origen_tipo"].ToString(),
                        readerDetalle["origen_id"].ToString(),
                        readerDetalle["codigo_producto"].ToString(),
                        readerDetalle["descripcion"].ToString(),
                        Convert.ToInt32(readerDetalle["cantidad_a_cotizar"]),
                        readerDetalle["marca"].ToString()
                    );
                }
                readerDetalle.Close();

                // Permitir edición si querés modificar cantidades/marcas, etc.
                dgvPCot.ReadOnly = false;

                // Habilitar controles necesarios
                dateTimePicker1.Enabled = true;
                cmbRubros.Enabled = true;
                cbSolicitado.Enabled = true;
                cbAprobado.Enabled = true;
                cbDeenegado.Enabled = true;
                cbCotizado.Enabled = true;
                bttnGuardar.Enabled = true;
                bttnEliminar.Enabled = true;

                MessageBox.Show("Pedido listo para modificar.", "Información");
            }
        }


        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            if (idCotizacionActual == 0)
            {
                MessageBox.Show("Debe cargar un pedido existente antes de eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("¿Desea eliminar este Pedido de Cotización?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    SqlTransaction transaccion = conexion.BeginTransaction();

                    // Eliminar detalles
                    SqlCommand cmdDet = new SqlCommand("DELETE FROM Pcot_Detalle WHERE id_Pcot = @id", conexion, transaccion);
                    cmdDet.Parameters.AddWithValue("@id", idCotizacionActual);
                    cmdDet.ExecuteNonQuery();

                    // Eliminar master
                    SqlCommand cmdMaster = new SqlCommand("DELETE FROM Pcot_Master WHERE id_Pcot = @id", conexion, transaccion);
                    cmdMaster.Parameters.AddWithValue("@id", idCotizacionActual);
                    cmdMaster.ExecuteNonQuery();

                    transaccion.Commit();

                    dgvPCot.Rows.Clear();
                    idCotizacionActual = 0;

                    MessageBox.Show("Pedido de Cotización eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el pedido de cotización: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void cbSolicitado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSolicitado.Checked)
            {
                cbAprobado.Checked = false;
                cbDeenegado.Checked = false;
                cbCotizado.Checked = false;
            }
        }

        private void cbAprobado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAprobado.Checked)
            {
                cbSolicitado.Checked = false;
                cbDeenegado.Checked = false;
                cbCotizado.Checked = false;
            }
        }

        private void cbDeenegado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDeenegado.Checked)
            {
                cbSolicitado.Checked = false;
                cbAprobado.Checked = false;
                cbCotizado.Checked = false;
            }
        }

        private void cbCotizado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCotizado.Checked)
            {
                cbSolicitado.Checked = false;
                cbAprobado.Checked = false;
                cbDeenegado.Checked = false;
            }
        }
    }
}
