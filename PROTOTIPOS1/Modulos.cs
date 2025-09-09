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
        }
        public Modulos(string nUsuario)
        {
            InitializeComponent();
            lblNivel.Text = nUsuario;
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


                Login login = new Login();
                login.Show();
                Hide();
            }
        }

        private void bttnAdministrador_Click(object sender, EventArgs e)
        {
            Administrador admin = new Administrador();
            admin.Show();
            Hide();
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
