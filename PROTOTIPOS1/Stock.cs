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
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Hide();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            
            
            //cmb descripcion
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT Descripcion FROM Stock";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    cmbDescripcion.Items.Clear();
                    foreach(DataRow fila in tabla.Rows)
                    {
                        cmbDescripcion.Items.Add(fila["Descripcion"].ToString());
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }

            //cmb rubros
            string consulta = "SELECT id_Rubro, Descripcion FROM Rubros";
            using(SqlConnection conexion = new SqlConnection(connectionString))
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

        public void limpiarValores()
        {
            cmbDescripcion.Text = "";
            cmbRubros.SelectedIndex = -1;
            txtbMarca.Clear();
            txtbUMVenta.Clear();
            checkbActivo.Checked = false;
        }


        private void bttnAgregar_Click(object sender, EventArgs e)
        {
            string nuevoProducto = cmbDescripcion.Text.Trim();
            int rubro = Convert.ToInt32(cmbRubros.SelectedValue);
            string UMVenta = txtbUMVenta.Text;
            string marca = txtbMarca.Text;
            bool activo = checkbActivo.Checked;

            if (string.IsNullOrEmpty(nuevoProducto))
            {
                MessageBox.Show("La descripcion del producto esta vacía");
                return;
            }

            bool existe = false;

            foreach (var item in cmbDescripcion.Items)
            {
                if (item.ToString().Trim().ToLower() == nuevoProducto.ToLower())
                {
                    existe = true;
                    break;
                }
            }

            if (existe)
            {
                MessageBox.Show("El producto ya existe");
                return;
            }

            else
            {

                string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
                string query = "INSERT INTO Stock (Descripcion, id_Rubro, UM_venta, marca, activo) VALUES (@Descripcion, @id_Rubro, @UM_Venta, @marca, @activo)";

                using(SqlConnection conexion = new SqlConnection(connectionString))
                {
                    try
                    {
                        conexion.Open();
                        SqlCommand comando = new SqlCommand(query, conexion);
                        comando.Parameters.AddWithValue("@Descripcion", nuevoProducto);        
                        comando.Parameters.AddWithValue("@id_Rubro", rubro);
                        comando.Parameters.AddWithValue("@UM_venta", UMVenta);
                        comando.Parameters.AddWithValue("@marca", marca);
                        comando.Parameters.AddWithValue("@activo", activo);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Producto agregado correctamente");
                        cmbDescripcion.Items.Add(nuevoProducto);


                        limpiarValores();
                     } catch (SqlException ex)
                    {
                        if (ex.Number == 2627) MessageBox.Show("Ya existe un producto con ese nombre");
                        else MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
