using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PROTOTIPOS1
{
    public class GestorDV
    {
        private readonly string _connectionString;
        public GestorDV(string connectionString)
        {
            _connectionString = connectionString;

        }
        private string Sha256Hex(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        private string Normalizar(string s)
        {
            return (s ?? string.Empty).Trim().ToUpperInvariant();
        }


        public string CalcularDVHParaValores(decimal id_Usuario, string nombre_usuario, string email, string contrasenaHasheada, string nombre, string apellido, decimal nivel)
        {
            string concatenado =
                id_Usuario.ToString() + "|" +
                Normalizar(nombre_usuario) + "|" +
                Normalizar(email) + "|" +
                Normalizar(contrasenaHasheada) + "|" +
                Normalizar(nombre) + "|" +
                Normalizar(apellido) + "|" +
                nivel.ToString();

            return Sha256Hex(concatenado);
        }

        public void ActualizarDVHPorID(decimal idUsuario)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id_Usuario, nombre_Usuario, email, contraseña, nombre, apellido, nivel
                        FROM Usuarios
                        WHERE id_Usuario = @id";
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return; // no existe
                        decimal id = Convert.ToDecimal(r["id_Usuario"]);
                        string nombre_usuario = r["nombre_Usuario"]?.ToString();
                        string email = r["email"]?.ToString();
                        string contr = r["contraseña"]?.ToString();
                        string nombre = r["nombre"]?.ToString();
                        string apellido = r["apellido"]?.ToString();
                        decimal nivel = Convert.ToDecimal(r["nivel"]);

                        string dvh = CalcularDVHParaValores(id, nombre_usuario, email, contr, nombre, apellido, nivel);
                        r.Close();

                        using (SqlCommand u = cn.CreateCommand())
                        {
                            u.CommandText = "UPDATE Usuarios SET DVH = @dvh WHERE id_Usuario = @id";
                            u.Parameters.AddWithValue("@dvh", dvh);
                            u.Parameters.AddWithValue("@id", idUsuario);
                            u.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public void ActualizarDVHPorNombreDeUsuario(string nombreUsuario)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id_Usuario FROM Usuarios WHERE nombre_Usuario = @nombreUsuario";
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    object o = cmd.ExecuteScalar();
                    if (o == null) return;
                    decimal id = Convert.ToDecimal(o);
                    ActualizarDVHPorID(id);
                }
            }
        }


        public void RegenerarDVHCompleto()
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                string query = @"
            SELECT id_Usuario, nombre_Usuario, email, contraseña, nombre, apellido, nivel
            FROM Usuarios";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    List<(decimal id, string dvh)> lista = new List<(decimal, string)>();

                    while (dr.Read())
                    {
                        decimal id = Convert.ToDecimal(dr["id_Usuario"]);
                        string nombreUsuario = dr["nombre_Usuario"].ToString();
                        string email = dr["email"].ToString();
                        string contrasena = dr["contraseña"].ToString();
                        string nombre = dr["nombre"].ToString();
                        string apellido = dr["apellido"].ToString();
                        decimal nivel = Convert.ToDecimal(dr["nivel"]);

                        string dvh = CalcularDVHParaValores(
                            id,
                            nombreUsuario,
                            email,
                            contrasena,
                            nombre,
                            apellido,
                            nivel
                        );

                        lista.Add((id, dvh));
                    }

                    dr.Close();
                    foreach (var item in lista)
                    {
                        string update = @"UPDATE Usuarios SET DVH = @dvh WHERE id_Usuario = @id";

                        using (SqlCommand cmdUpdate = new SqlCommand(update, cn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@dvh", item.dvh);
                            cmdUpdate.Parameters.AddWithValue("@id", item.id);
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                }
                ActualizarDVV("REGENERACION_TOTAL");
            }
        }

        public bool InicializarDVV()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    // 1️⃣ Obtener todos los DVH de Usuarios
                    SqlCommand cmd = new SqlCommand("SELECT DVH FROM Usuarios ORDER BY id_Usuario", conexion);
                    SqlDataReader reader = cmd.ExecuteReader();

                    StringBuilder concatenado = new StringBuilder();

                    while (reader.Read())
                    {
                        concatenado.Append(reader["DVH"].ToString());
                    }

                    reader.Close();

                    // 2️⃣ Generar el DVV final
                    string dvv = Hasheo.generarHash(concatenado.ToString());

                    // 3️⃣ Borrar el anterior si existiera (por seguridad)
                    SqlCommand borrar = new SqlCommand("DELETE FROM DigitosVerificadores WHERE Tabla = 'Usuarios'", conexion);
                    borrar.ExecuteNonQuery();

                    // 4️⃣ Insertar el nuevo DVV
                    SqlCommand insertar = new SqlCommand(
                        "INSERT INTO DigitosVerificadores (Tabla, DVV) VALUES ('Usuarios', @dvv)", conexion);

                    insertar.Parameters.AddWithValue("@dvv", dvv);
                    insertar.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ExisteDVV()
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM DigitosVerificadores WHERE TablaNombre = 'Usuarios'",
                    conexion);

                int cantidad = (int)cmd.ExecuteScalar();
                return cantidad > 0;
            }
        }

        public void CrearDVVInicial()
        {
            string dvv = CalcularDVV();

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = @"
                INSERT INTO DigitosVerificadores (TablaNombre, DVV, FechaCalculo)
                VALUES ('Usuarios', @dvv, GETDATE())";

                    cmd.Parameters.AddWithValue("@dvv", dvv);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string CalcularDVV()
        {
            StringBuilder sb = new StringBuilder();
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "SELECT DVH FROM Usuarios ORDER BY id_Usuario";
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            string dvh = r["DVH"] == DBNull.Value ? string.Empty : r["DVH"].ToString();
                            // separador fijo para evitar colisiones
                            sb.Append(dvh);
                            sb.Append("|");
                        }
                    }
                }
            }
            return Sha256Hex(sb.ToString());
        }

        public void ActualizarDVV(string usuarioQueActualiza = null)
        {
            string dvv = CalcularDVV();
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO DigitosVerificadores (TablaNombre, DVV, FechaCalculo, UsuarioQueActualizo)
                        VALUES (@tabla, @dvv, GETDATE(), @usuario)";
                    cmd.Parameters.AddWithValue("@tabla", "Usuarios");
                    cmd.Parameters.AddWithValue("@dvv", dvv);
                    cmd.Parameters.AddWithValue("@usuario", (object)usuarioQueActualiza ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public bool VerificarDVV(out string dvvActual, out string dvvGuardado)
        {
            dvvActual = CalcularDVV();
            dvvGuardado = null;

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT TOP 1 DVV FROM DigitosVerificadores
                        WHERE TablaNombre = @tabla
                        ORDER BY FechaCalculo DESC, Id DESC";
                    cmd.Parameters.AddWithValue("@tabla", "Usuarios");
                    object o = cmd.ExecuteScalar();
                    if (o == null)
                    {
                        dvvGuardado = null;
                        return false; // no hay DVV guardado aún
                    }
                    dvvGuardado = o.ToString();
                }
            }
            return string.Equals(dvvActual, dvvGuardado, StringComparison.OrdinalIgnoreCase);
        }


        public bool VerificarDVH(decimal idUsuario, out string dvhActual, out string dvhGuardado)
        {
            dvhActual = null;
            dvhGuardado = null;

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id_Usuario, nombre_Usuario, email, contraseña, nombre, apellido, nivel, DVH
                        FROM Usuarios WHERE id_Usuario = @id";
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return false;
                        decimal id = Convert.ToDecimal(r["id_Usuario"]);
                        string nombre_usuario = r["nombre_Usuario"]?.ToString();
                        string email = r["email"]?.ToString();
                        string contr = r["contraseña"]?.ToString();
                        string nombre = r["nombre"]?.ToString();
                        string apellido = r["apellido"]?.ToString();
                        decimal nivel = Convert.ToDecimal(r["nivel"]);
                        dvhGuardado = r["DVH"] == DBNull.Value ? null : r["DVH"].ToString();

                        dvhActual = CalcularDVHParaValores(id, nombre_usuario, email, contr, nombre, apellido, nivel);
                    }
                }
            }

            if (dvhGuardado == null) return false;
            return string.Equals(dvhActual, dvhGuardado, StringComparison.OrdinalIgnoreCase);
        }
    }

}



