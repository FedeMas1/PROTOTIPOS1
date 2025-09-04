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
    public partial class PPrincipal_Pagos : Form
    {
        public PPrincipal_Pagos()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Modulos mod = new Modulos();
            mod.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Orden_de_Pago op = new Orden_de_Pago();
            op.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pago pago = new Pago();
            pago.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Recibo rec = new Recibo();
            rec.Show();
            this.Hide();
        }
    }
}
