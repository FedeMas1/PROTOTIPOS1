using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PROTOTIPOS1
{
    internal class Hasheo
    {
        public static string generarHash(string texto)
        {
            byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                byte[] hashbytes = sha1.ComputeHash(bytesTexto);
                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashbytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
