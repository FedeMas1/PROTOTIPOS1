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
    public partial class Cotizacion_proveedor : Form
    {
        public Cotizacion_proveedor()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(button5, "Eliminar cotizacion.");
            toolTip.SetToolTip(button6, "Modificar cotizacion.");
            toolTip.SetToolTip(button2, "Buscar prdido de cotizacion.");
            toolTip.SetToolTip(button10, "Guardar un proveedor.");
            toolTip.SetToolTip(button9, "Comparar proveedores.");
            toolTip.SetToolTip(button1, "Vuelve al formulario anterior.");
            toolTip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            compacion_de_proveedores comparacion = new compacion_de_proveedores();
            comparacion.ShowDialog();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void Cotizacion_proveedor_Load(object sender, EventArgs e)
        {
            //Traer columnas de la base de datos
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT * FROM cot_proveedor";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            // boton de eliminar
            DataGridViewButtonColumn bttnEliminar = new DataGridViewButtonColumn();
            bttnEliminar.HeaderText = "Eliminar";
            bttnEliminar.Name = "bttnEliminar";
            bttnEliminar.Text = "X";
            bttnEliminar.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(bttnEliminar);
        }

            private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "bttnEliminar")
            {
                DialogResult resultado = MessageBox.Show("¿Desea eliminar este producto?", "Confirmacion", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
            
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
    }
    }
