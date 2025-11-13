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
    public partial class PR : Form
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        private int idPrActual = 0;
        public PR()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(bttnEliminar, "Eliminar un bien de uso.");
            toolTip.SetToolTip(bttnGuardar, "Guardar pedido de reaprovisionamiento.");
            toolTip.SetToolTip(bttnBuscar, "Buscar pedido de reaprovisionamiento.");
            toolTip.SetToolTip(button5, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignora clics fuera de celdas válidas
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Verifica que la columna "Eliminar" exista y coincida
            if (dgvProductos.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                // Verifica que el índice de fila sea válido
                if (e.RowIndex >= 0 && e.RowIndex < dgvProductos.Rows.Count)
                {
                    // Evita eliminar la fila "nueva" (la que aparece vacía al final)
                    if (!dgvProductos.Rows[e.RowIndex].IsNewRow)
                    {
                        DialogResult confirm = MessageBox.Show(
                            "¿Desea eliminar este producto?",
                            "Confirmar eliminación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (confirm == DialogResult.Yes)
                        {
                            dgvProductos.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }
            }
        }

        private void PR_Load(object sender, EventArgs e)
        {
            CargarRubros();
            lblNPedido.Text = "XXXX";

            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.CellContentClick += dataGridView1_CellContentClick;

            if (Sesion.nivel == 1)
            {
                bttnEliminar.Enabled = false;
                bttnGuardar.Enabled = false;
            }
        }

        private void Limpiar()
        {
            dgvProductos.DataSource = null;
            cmbRubros.SelectedIndex = -1;

            lblNPedido.Text = "XXXX";
            dateTimePicker1.Value = DateTime.Now;
            cbSolicitado.Checked = false;
            cbDenegado.Checked = false;
            cbAprobado.Checked = false;
            cbCotizado.Checked = false;
        }
        private void CargarRubros()
        {
            string consulta = "SELECT id_Rubro, Descripcion FROM Rubros";
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(consulta, conexion);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbRubros.DisplayMember = "Descripcion";
                cmbRubros.ValueMember = "id_Rubro";
                cmbRubros.DataSource = dt;
                cmbRubros.SelectedIndex = -1;
            }
        }

        private void CargarProductos(int rubroSeleccionado)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                string query = "SELECT codigo_Producto, Descripcion, (cantidad_maxima - cantidad) AS cantidad_a_pedir, marca, id_Rubro FROM Stock WHERE id_Rubro = @Rubro AND cantidad < punto_pedido" +
                    " UNION ALL " +
                    "SELECT codigo_Producto, Descripcion, (cantidad_maxima - cantidad_compra) AS cantidad_a_pedir, marca, id_Rubro FROM Materiales WHERE id_Rubro = @Rubro AND cantidad_compra < punto_pedido";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Rubro", rubroSeleccionado);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvProductos.DataSource = dt;
                }
            }

            if (!dgvProductos.Columns.Contains("Eliminar"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.Name = "Eliminar";
                btnEliminar.Text = "Eliminar";
                btnEliminar.UseColumnTextForButtonValue = true;
                dgvProductos.Columns.Add(btnEliminar);
            }
        }


        private void bttnCargar_Click(object sender, EventArgs e)
        {
            if (cmbRubros.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un rubro");
                return;
            }

            int rubroSeleccionado = Convert.ToInt32(cmbRubros.SelectedValue);
            CargarProductos(rubroSeleccionado);
        }

        private string ObtenerEstado()
        {
            if (cbAprobado.Checked)
                return "Aprobado";
            else if (cbDenegado.Checked)
                return "Denegado";
            else if (cbCotizado.Checked)
                return "Cotizado";
            else
                return "Solicitado"; // valor por defecto
        }
        private int GuardarPedido()
        {
            int idPrGenerado = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string queryMaster = @"INSERT INTO PR_Master (fecha, rubro,estado) " +
                "OUTPUT INSERTED.id_PR " +                                             // OUTPUT INSERTED.id_Pr hace que SQL devuelva el valor autogenerado del campo id_Pr.               
                "VALUES (@Fecha, @Rubro,@Estado)";

                using (SqlCommand cmd = new SqlCommand(queryMaster, conn))
                {
                  cmd.Parameters.AddWithValue(@"Fecha", dateTimePicker1.Value);
                  cmd.Parameters.AddWithValue(@"Rubro", cmbRubros.SelectedValue);
                  cmd.Parameters.AddWithValue("@Estado", ObtenerEstado());

                  idPrGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }          
            }
            return idPrGenerado;
        }

        

        private void GuardarProductos(int idPr)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach(DataGridViewRow row in dgvProductos.Rows)
                {
                    if (row.IsNewRow) continue;
                    string queryDetalle = @"INSERT INTO PR_Detalle (id_Pr, codigo_producto, descripcion, cantidad_a_pedir, marca) VALUES (@idPr, @Codigo, @Descripcion, @Cantidad, @Marca)";
                    
                    using (SqlCommand cmd = new SqlCommand(queryDetalle, conn))
                    {
                        cmd.Parameters.AddWithValue("@idPr", idPr);
                        cmd.Parameters.AddWithValue("@Codigo", row.Cells["codigo_producto"].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Descripcion", row.Cells["Descripcion"].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cantidad", row.Cells["cantidad_a_pedir"].Value ?? 0);
                        cmd.Parameters.AddWithValue("@Marca", row.Cells["marca"].Value ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        
        private void ActualizarPedido()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string UpdateMaster = "UPDATE PR_Master SET fecha = @Fecha, rubro = @Rubro, estado = @Estado WHERE id_Pr = @Id";
                using (SqlCommand cmd = new SqlCommand(UpdateMaster, conn))
                {
                    cmd.Parameters.AddWithValue("@Fecha", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Rubro", cmbRubros.SelectedValue);
                    cmd.Parameters.AddWithValue("@Estado", ObtenerEstado());
                    cmd.Parameters.AddWithValue("@Id", idPrActual);
                    cmd.ExecuteNonQuery();
                }

                string DeleteDetalle = "DELETE FROM PR_Detalle WHERE id_Pr = @Id";
                using (SqlCommand cmd = new SqlCommand(DeleteDetalle, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", idPrActual);
                    cmd.ExecuteNonQuery();
                }
                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    if (row.IsNewRow) continue;

                    string queryDetalle = @"INSERT INTO PR_Detalle (id_Pr, codigo_producto, descripcion, cantidad_a_pedir, marca) 
                                            VALUES (@idPr, @Codigo, @Descripcion, @Cantidad, @Marca)";

                    using (SqlCommand cmd = new SqlCommand(queryDetalle, conn))
                    {
                        cmd.Parameters.AddWithValue("@idPr", idPrActual);
                        cmd.Parameters.AddWithValue("@Codigo", row.Cells["codigo_producto"].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Descripcion", row.Cells["descripcion"].Value ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cantidad", row.Cells["cantidad_a_pedir"].Value ?? 0);
                        cmd.Parameters.AddWithValue("@Marca", row.Cells["marca"].Value ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private bool ValidarProductos()
        {
            foreach(DataGridViewRow row in dgvProductos.Rows)
            {
                if (row.IsNewRow) continue;
                object cantidadobj = row.Cells["cantidad_a_pedir"].Value;

                if(cantidadobj == null || !int.TryParse(cantidadobj.ToString(), out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Existen productos con cantidad inválida (vacía, nula o 0). Corrija antes de guardar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        
        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if(dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos para guardar.");
                return;
            }

            if(cmbRubros.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un rubro antes de guardar");
                return;
            }

            if (!ValidarProductos()) return;

            if(idPrActual == 0)
            {
                int idPr = GuardarPedido();
                GuardarProductos(idPr);
                idPrActual = idPr;
                lblNPedido.Text = idPrActual.ToString();
                Bitacora bit = new Bitacora();
                bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, "Agregó un pedido de reapvisionamiento");
                MessageBox.Show("Pedido guardado correctamente");
            }
            else
            {
                ActualizarPedido();
                lblNPedido.Text = idPrActual.ToString();
                Bitacora bit = new Bitacora();
                bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, $"Eliminó un producto del stock: {idPrActual}");
                MessageBox.Show("Pedido modificado correctamente");
                
            }

            dgvProductos.DataSource = null;
            dgvProductos.Columns.Clear();
            cmbRubros.SelectedIndex = -1;
            Limpiar();
        }    
        

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el número de PR a cargar:", "Buscar PR", "");
            if (string.IsNullOrEmpty(input)) return;

            if(!int.TryParse(input, out int idBuscado))
            {
                MessageBox.Show("Numero de Pr inavilido");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string queryMaster = "SELECT * FROM PR_Master WHERE id_Pr = @Id";
                SqlCommand cmd = new SqlCommand(queryMaster, conn);
                cmd.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el PR deseado");
                    return;
                }

                idPrActual = idBuscado;
                lblNPedido.Text = idBuscado.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0]["fecha"]);
                cmbRubros.SelectedValue = Convert.ToInt32(dt.Rows[0]["rubro"]);

                string estado = dt.Rows[0]["estado"].ToString();
                cbSolicitado.Checked = estado == "Solicitado";
                cbAprobado.Checked = estado == "Aprobado";
                cbDenegado.Checked = estado == "Denegado";
                cbCotizado.Checked = estado == "Cotizado";

                string queryDetalle = "SELECT codigo_producto, descripcion, cantidad_a_pedir, marca FROM PR_Detalle WHERE id_Pr = @id";
                SqlCommand cmdProductos = new SqlCommand(queryDetalle, conn);
                cmdProductos.Parameters.AddWithValue("@Id", idBuscado);
                SqlDataAdapter daProd = new SqlDataAdapter(cmdProductos);
                DataTable dtProducto = new DataTable();
                daProd.Fill(dtProducto);
                dgvProductos.DataSource = dtProducto;

                if (!dgvProductos.Columns.Contains("Eliminar"))
                {
                    DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                    btnEliminar.Name = "Eliminar";
                    btnEliminar.Text = "Eliminar";
                    btnEliminar.UseColumnTextForButtonValue = true;
                    dgvProductos.Columns.Add(btnEliminar);
                }
            }
        }

        private void bttnEliminar_Click(object sender, EventArgs e)
        {
            if (idPrActual == 0)
            {
                MessageBox.Show("Primero debe cargar un PR antes de eliminarlo");
                return;
            }

            DialogResult dr = MessageBox.Show("¿Desea eliminar este pedido de reaprovisionamiento?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string deleteDetalle = "DELETE FROM PR_Detalle WHERE id_Pr = @Id";
                SqlCommand cmdDetalle = new SqlCommand(deleteDetalle, conn);
                cmdDetalle.Parameters.AddWithValue("@Id", idPrActual);
                cmdDetalle.ExecuteNonQuery();

                string deleteMaster = "DELETE FROM PR_Master WHERE id_Pr = @Id";
                SqlCommand cmdMaster = new SqlCommand(deleteMaster, conn);
                cmdMaster.Parameters.AddWithValue("@Id", idPrActual);
                cmdMaster.ExecuteNonQuery();
            }

            Bitacora bit = new Bitacora();
            bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, $"Eliminó un pedido de reaprovisacion:{idPrActual} ");
            MessageBox.Show("Pedido de reaprovisionamiento eliminado correctamente");
            idPrActual = 0;
            lblNPedido.Text = "XXXX";
            dgvProductos.DataSource = null;
            dgvProductos.Columns.Clear();
            cmbRubros.SelectedIndex = -1;
            Limpiar();
        }

        private void button5_Click(object sender, EventArgs e)
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
                cbDenegado.Checked = false;
                cbCotizado.Checked = false;
            }
        }

        private void cbAprobado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAprobado.Checked)
            {
                cbSolicitado.Checked = false;
                cbDenegado.Checked = false;
                cbCotizado.Checked = false;
            }
        }

        private void cbDenegado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDenegado.Checked)
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
                cbDenegado.Checked = false;
            }
        }
    }
}
