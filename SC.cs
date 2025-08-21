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
    public partial class SC : Form
    {
        public SC()
        {
            InitializeComponent();
        }

        private void SC_Load(object sender, EventArgs e)
        {
            string connectionString = @"Server=DESKTOP-1N5CLIH\MSQLSERVER2022;Database=panaderia;Trusted_Connection=True;";
            string query = "SELECT * FROM Sc";

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
        }
    }
}
