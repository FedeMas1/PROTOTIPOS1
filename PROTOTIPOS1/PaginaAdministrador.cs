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
    public partial class PaginaAdministrador : Form
    {
        public PaginaAdministrador()
        {
            InitializeComponent();

            Estilos estilos = new Estilos();
            estilos.AplicarEstilos(this);

            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(button1, "Ir a control de usuarios");
            tooltip.SetToolTip(button2, "Ir a backup y restore");
            tooltip.SetToolTip(bttnCSesion, "Cierra la sesion");
            tooltip.SetToolTip(button3, "Ir al formulario anterior");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Administrador admin = new Administrador();
            admin.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            backup.Show();
            this.Hide();    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Modulos modulos = new Modulos();
            modulos.Show();
            this.Hide();
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

        private void bttnBitacora_Click(object sender, EventArgs e)
        {
            FrmBitacora bit = new FrmBitacora();
            bit.Show();
            this.Hide();
        }
    }
}
