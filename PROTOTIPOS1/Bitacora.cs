using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROTOTIPOS1
{
    internal class Bitacora
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS10;Initial Catalog=Panaderia;Integrated Security=True;TrustServerCertificate=True;";
        public bool RegistrarEvento(int idUsuario, string nombreUsuario, string accion)
        {
            try
            {
                string query = "INSERT INTO Bitacora (id_Usuario, nombre_Usuario, accion, fecha_Hora) " +
                                "VALUES (@id, @nombre, @accion, @fecha)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.Parameters.AddWithValue("@nombre", nombreUsuario);
                    cmd.Parameters.AddWithValue("@accion", accion);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return true;
            } catch 
            {
                return false;
            }
        }
    }
}
