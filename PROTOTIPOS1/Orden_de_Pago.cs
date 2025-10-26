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
    public partial class Orden_de_Pago : Form
    {
        public Orden_de_Pago()
        {
            InitializeComponent();
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(button1, "Eliminar una Orden de Pago.");
            toolTip.SetToolTip(button2, "Modificar una Orden de Pago.");
            toolTip.SetToolTip(button3, "Guardar una Orden de Pago.");
            toolTip.SetToolTip(button4, "Buscar numero de Factura.");
            toolTip.SetToolTip(button5, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
            toolTip.SetToolTip(bttnCSesion, "Vuelve al formulario anterior.");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void bttnCSesion_Click(object sender, EventArgs e)
        {
            PPrincipal_Pagos ppp = new PPrincipal_Pagos();
            ppp.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
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
