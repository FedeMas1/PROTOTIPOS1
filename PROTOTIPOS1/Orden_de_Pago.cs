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
    }
}
