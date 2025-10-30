using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROTOTIPOS1
{
    public partial class Modulos : Form
    {
        public Modulos()
        {
            InitializeComponent();

            ToolTip tooltip = new ToolTip();


            tooltip.SetToolTip(bttnCSesion, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
            tooltip.SetToolTip(bttnAdministrador, "Accede a las funciones administrativas y de configuración del sistema.");
            tooltip.SetToolTip(button1, "Administra las compras: agregar nuevas, modificar o eliminar compras registradas.");
            tooltip.SetToolTip(button3, "Administra las ventas realizadas a clientes: registrar nuevas, modificar o eliminar ventas.");
            tooltip.SetToolTip(button2, "Gestiona los pagos a proveedores: agregar, modificar o eliminar registros de pago.");
            tooltip.SetToolTip(button4, "Gestiona los cobros de clientes: agregar, modificar o eliminar registros de cobro.");
        }
        public Modulos(string nUsuario)
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PPrincipal pp = new PPrincipal();
            pp.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PPrincipal_Pagos ppp = new PPrincipal_Pagos();
            ppp.Show();
            this.Close();
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

        private void bttnAdministrador_Click(object sender, EventArgs e)
        {
            PaginaAdministrador pAdmin = new PaginaAdministrador();
            pAdmin.Show();
            this.Hide();
        }

        private void Modulos_Load(object sender, EventArgs e)
        {
            if(Sesion.nivel == 3)
            {
                bttnAdministrador.Visible = true;
            }
            else
            {
                bttnAdministrador.Visible = false; 
            }
        }
    }
}
