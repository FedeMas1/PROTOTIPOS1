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
    public partial class FrmBitacora : Form
    {
        private bool filtroUsuarioActivo = false;
        private bool filtroFechaActiva = false;
        public FrmBitacora()
        {
            InitializeComponent();
            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);
        }

        private void CargarBitacora()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
            string query = "SELECT id_Log, id_Usuario, nombre_Usuario, accion, fecha_Hora FROM Bitacora ORDER BY fecha_Hora DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();

                dgvBitacora.DataSource = dt;
            }
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            CargarBitacora();
        }

        private void bttnActualizar_Click(object sender, EventArgs e)
        {
            CargarBitacora();
        
        }

        private void bttnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea limpiar toda la bitácora?",
            "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
                string query = "DELETE FROM Bitacora";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                Bitacora bit = new Bitacora();
                bit.RegistrarEvento(Sesion.id_Usuario, Sesion.nombreUsuario, "Limpió la bitácora");

                CargarBitacora();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PaginaAdministrador pa = new PaginaAdministrador();
            pa.Show();
            this.Hide();
        }

        private void AplicarFiltros()
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";

            string query = "SELECT id_Log, id_Usuario, nombre_Usuario, accion, fecha_Hora FROM Bitacora WHERE 1=1";

            List<SqlParameter> parametros = new List<SqlParameter>();
            
            if(filtroUsuarioActivo && !string.IsNullOrWhiteSpace(txtbUsuario.Text))
            {
                query += "AND nombre_Usuario = @usuario";
                parametros.Add(new SqlParameter("@usuario", txtbUsuario.Text.Trim()));    
            }

            if (filtroFechaActiva)
            {
                DateTime fecha = dtp.Value.Date;

                query += " AND CAST(fecha_Hora AS DATE) = @fecha";
                parametros.Add(new SqlParameter("@fecha", fecha));
            }

            query += " ORDER BY fecha_Hora DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using(SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddRange(parametros.ToArray());
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                conn.Close();

                dgvBitacora.DataSource = dt;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbUsuario.Text))
            {
                MessageBox.Show("No se ha ingresado ningun nombre");
                return;
            }
            else
            {
               filtroUsuarioActivo = true;
               AplicarFiltros();
            }
        }

        private void bttnQuitar_Click(object sender, EventArgs e)
        {
            txtbUsuario.Text = string.Empty;
            dtp.Value = DateTime.Now;
            CargarBitacora();
        }

       


        private void bttnFFecha_Click(object sender, EventArgs e)
        {
            filtroFechaActiva = true;
            AplicarFiltros();
        }

       
    }
    }

