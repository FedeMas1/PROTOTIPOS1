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
    public partial class PPrincipal : Form
    {
        public PPrincipal(){
            InitializeComponent();
            }
        public PPrincipal(string nUsuario)
        {
            InitializeComponent();
            lblNivel.Text = nUsuario;
        }

        private void bttnStock_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            stock.Show();
            Hide();
        }

        private void bttnMateriales_Click(object sender, EventArgs e)
        {
            Materiales materiales = new Materiales();
            materiales.Show();
            Hide();
        }

        private void bttnPR_Click(object sender, EventArgs e)
        {
            PR Pr = new PR();
            Pr.Show();
            Hide();
        }

        private void bttnInventario_Click(object sender, EventArgs e)
        {
            Inventario inventario = new Inventario();
            inventario.Show();
            Hide();
        }

        private void bttnSC_Click(object sender, EventArgs e)
        {
            SC Sc = new SC();
            Sc.Show();
            Hide();
        }

        private void bttnPCot_Click(object sender, EventArgs e)
        {
            Ped_cotizacion Pc = new Ped_cotizacion();
            Pc.Show();
            Hide();
        }

        private void bttnProveedores_Click(object sender, EventArgs e)
        {
            Proveedores proveedores = new Proveedores();
            proveedores.Show();
            Hide();
        }

        private void bttnCotizacion_Click(object sender, EventArgs e)
        {
            Cotizacion_proveedor cotizacion = new Cotizacion_proveedor();
            cotizacion.Show();
            Hide();
        }

        private void bttnOC_Click(object sender, EventArgs e)
        {
            Orden_de_compra Oc = new Orden_de_compra();
            Oc.Show();
            Hide();
        }

        private void bttnIRM_Click(object sender, EventArgs e)
        {
            Informe_recepcion Irm = new Informe_recepcion();
            Irm.Show();
            Hide();
        }

        private void bttnFP_Click(object sender, EventArgs e)
        {
            Factura_prov fp = new Factura_prov();
            fp.Show();
            Hide();
        }

        private void bttnDevoluciones_Click(object sender, EventArgs e)
        {
            Devolucion dev = new Devolucion();
            dev.Show();
            Hide();
        }

        private void bttnCSesion_Click(object sender, EventArgs e)
        {
            Modulos mod = new Modulos();
            mod.Show();
            this.Hide();
        }

        private void lblNivel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
