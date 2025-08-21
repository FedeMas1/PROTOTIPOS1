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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (txtcorreo.Text=="1" && txtcontra.Text == "1")
            {
                Home home = new Home();
                home.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("credenciales incorrectas");
            }
        }
    }
}
