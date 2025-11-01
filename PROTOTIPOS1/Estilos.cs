using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROTOTIPOS1
{
    internal class Estilos
    {
        public void AplicarEstilos(Form formulario)
        {

            formulario.BackColor = Color.Beige;

            foreach (Control ctrl in formulario.Controls)
            {

                if (ctrl is LinkLabel link)
                {
                    link.LinkColor = Color.Gold;
                    link.VisitedLinkColor = Color.Gold;
                    link.ActiveLinkColor = Color.Gold;
                }

                if(ctrl is CheckBox cb) cb.BackColor = Color.Beige;
                if (ctrl is DataGridView dgv) dgv.BackColor = Color.White;
                

                if (ctrl is Button boton)
                {

                    boton.BackColor = Color.BurlyWood;
                    boton.ForeColor = Color.Black;
                    boton.FlatStyle = FlatStyle.Flat;
                    boton.FlatAppearance.BorderSize = 0;


                    Color colorOriginal = boton.BackColor;


                    boton.MouseEnter += (s, e) =>
                    {
                        if (boton.Name.Equals("bttnEliminar", StringComparison.OrdinalIgnoreCase))
                        {
                            boton.BackColor = Color.Red;
                        }
                        else if (boton.Name.Equals("bttnGuardar", StringComparison.OrdinalIgnoreCase))
                        {
                            boton.BackColor = Color.Green;
                        }
                        else
                        {
                            boton.BackColor = Color.Gold;
                        }
                    };


                    boton.MouseLeave += (s, e) =>
                    {
                        boton.BackColor = colorOriginal;
                    };
                }
            }
        }
    }
}
