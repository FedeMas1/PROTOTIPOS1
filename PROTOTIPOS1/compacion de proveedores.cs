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
    public partial class compacion_de_proveedores : Form
    {
        public compacion_de_proveedores()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void compacion_de_proveedores_Load(object sender, EventArgs e)
        {
            //Traer columnas de la bdd
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT * FROM comparacion_proveedores";

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

            //Boton de eliminar
            DataGridViewButtonColumn bttnEliminar = new DataGridViewButtonColumn();
            bttnEliminar.HeaderText = "Eliminar";
            bttnEliminar.Name = "bttnEliminar";
            bttnEliminar.Text = "X";
            bttnEliminar.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(bttnEliminar);

            //Checkbox seleccionar
            DataGridViewCheckBoxColumn checkSeleccionar = new DataGridViewCheckBoxColumn();
            checkSeleccionar.HeaderText = "Seleccionar";
            checkSeleccionar.Name = "checkSeleccionar";
            dataGridView1.Columns.Add(checkSeleccionar);


        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // funcionalidad boton eliminar
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "bttnEliminar")
            {
                DialogResult resultado = MessageBox.Show("¿Desea eliminar este producto?", "Confirmacion", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }

            // funcionalidad checkbox seleccionar
            List<DataGridViewRow> seleccionados = new List<DataGridViewRow>();
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                bool seleccionado = Convert.ToBoolean(fila.Cells["checkSeleccionar"].Value);
                if (seleccionado)
                {
                    seleccionados.Add(fila);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cotizacion_proveedor cotP = new Cotizacion_proveedor();
            cotP.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Orden_de_compra oc = new Orden_de_compra();
            oc.Show();
            Hide();
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
