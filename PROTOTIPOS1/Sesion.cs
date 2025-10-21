using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROTOTIPOS1
{
    internal class Sesion
    {
        public static int id_Usuario { get; set; }
        public static string email { get; set; }
        public static int nivel { get; set; }

        public static void CargarSesion(int id_UsuarioP, string emailP, int nivelP)
        {
            id_Usuario = id_UsuarioP;
            email = emailP;
            nivel = nivelP;
        }

        public static void CerrarSesion()
        {
            id_Usuario = 0;
            email = string.Empty;
            nivel = 0;

            
        }
    }
}
