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
    public partial class Pago : Form
    {
        public Pago()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(button1, "Eliminar un Pago.");
            toolTip.SetToolTip(button2, "Modificar un Pago.");
            toolTip.SetToolTip(button3, "Realizar un pago.");
            toolTip.SetToolTip(button4, "Seleccionar una Orden de Pago.");
            toolTip.SetToolTip(button5, "Cierra tu sesión actual y vuelve a la pantalla de inicio.");
            toolTip.SetToolTip(bttnCSesion, "Vuelve al formulario anterior.");
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
