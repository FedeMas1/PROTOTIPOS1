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
    public partial class N_pr : Form
    {
        public N_pr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtnpr.Text == "1")
            {
                pe_cot pe_Cot = new pe_cot();
                pe_Cot.ShowDialog();
                this.Hide();
            }else
            {
                MessageBox.Show("no existe ese numero de PR");
                    
            }
        }
    }
}
