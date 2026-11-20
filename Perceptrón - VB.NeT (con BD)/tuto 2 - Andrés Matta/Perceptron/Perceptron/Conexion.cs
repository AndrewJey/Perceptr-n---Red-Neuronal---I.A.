using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Perceptron
{
    public class Conexion
    {
        public bool UsandoDatosLocales { get; private set; }

        public static string GetConnectionString()
        {
            string env = Environment.GetEnvironmentVariable("PERCEPTRON_CONNECTION_STRING");
            return string.IsNullOrWhiteSpace(env) ? null : env.Trim();
        }

        public static List<Entradas> CrearDatosLocales()
        {
            // Display 7 segmentos (a,b,c,d,e,f,g) para dígitos 0..9;
            // Sd=1 para dígitos que usan el segmento central 'g' (2,3,4,5,6,8,9), linealmente separable.
            return new List<Entradas>
            {
                new Entradas(0, new[] { 1, 1, 1, 1, 1, 1, 0 }, 1, 0),
                new Entradas(1, new[] { 0, 1, 1, 0, 0, 0, 0 }, 1, 0),
                new Entradas(2, new[] { 1, 1, 0, 1, 1, 0, 1 }, 1, 1),
                new Entradas(3, new[] { 1, 1, 1, 1, 0, 0, 1 }, 1, 1),
                new Entradas(4, new[] { 0, 1, 1, 0, 0, 1, 1 }, 1, 1),
                new Entradas(5, new[] { 1, 0, 1, 1, 0, 1, 1 }, 1, 1),
                new Entradas(6, new[] { 1, 0, 1, 1, 1, 1, 1 }, 1, 1),
                new Entradas(7, new[] { 1, 1, 1, 0, 0, 0, 0 }, 1, 0),
                new Entradas(8, new[] { 1, 1, 1, 1, 1, 1, 1 }, 1, 1),
                new Entradas(9, new[] { 1, 1, 1, 1, 0, 1, 1 }, 1, 1),
            };
        }

        public List<Entradas> cargarTabla()
        {
            string connStr = GetConnectionString();
            if (!string.IsNullOrEmpty(connStr))
            {
                try
                {
                    var fromDb = new List<Entradas>();
                    using (SqlConnection con = new SqlConnection(connStr))
                    using (SqlCommand comando = new SqlCommand("select digito, x1, x2, x3, x4, x5, x6, x7, bias, Sd from perceptron", con))
                    {
                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int[] x = new int[7];
                                for (int k = 0; k < 7; k++)
                                {
                                    x[k] = dr.GetInt32(k + 1);
                                }
                                fromDb.Add(new Entradas(dr.GetInt32(0), x, dr.GetInt32(8), dr.GetInt32(9)));
                            }
                        }
                    }

                    if (fromDb.Count > 0)
                    {
                        UsandoDatosLocales = false;
                        return fromDb;
                    }
                }
                catch
                {
                    // Continúa con el conjunto local si SQL Server no está disponible.
                }
            }

            UsandoDatosLocales = true;
            return CrearDatosLocales();
        }
    }
}
