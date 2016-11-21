using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Perceptron
{
    class Conexion
    {
        private SqlConnection con = new SqlConnection("Data Source=.\\NOMBRE DE LA CONEXION SQLSERVER; Database=NOMBRE DE LA BASE DE DATOS; Integrated Security=SSPI");// no modificar el Integrated Security=SSPI
        private List<Entradas> entradas = new List<Entradas>();
        //private 
        

        public List<Entradas> cargarTabla()
        {
            con.Open();
            SqlCommand comando = new SqlCommand("select digito, x1, x2, x3, x4, x5, x6, x7, bias, Sd from perceptron", con);
            SqlDataReader dr = comando.ExecuteReader();
            

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int[] x = new int[7];
                    x[0]= dr.GetInt32(1);
                    x[1]= dr.GetInt32(2);
                    x[2]= dr.GetInt32(3);
                    x[3]= dr.GetInt32(4);
                    x[4]= dr.GetInt32(5);
                    x[5]= dr.GetInt32(6);
                    x[6]= dr.GetInt32(7);
                    Entradas obj = new Entradas(dr.GetInt32(0), x, dr.GetInt32(8), dr.GetInt32(9));
                    entradas.Add(obj);
                }
            }
            con.Close();
            return entradas;
        }  //carga la tabla de la base de datos.
    }
}
