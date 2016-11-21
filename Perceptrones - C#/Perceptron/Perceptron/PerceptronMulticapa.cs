using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    public class PerceptronMulticapa
    {
        public int C { get; set; }
        public int[] N { get; set; }
        public double[,] x { get; set; }
        public double[,] y { get; set; }
        public double[,] s { get; set; }
        public double[,] a { get; set; }
        public double[, ,] w { get; set; }
        public double[,] u { get; set; }
        public int numeroPatrones { get; set; }
        public double maximoEntradas { get; set; }
        public double minimoEntradas { get; set; }
        public double salidas { get; set; }
        public double minimoSalidas { get; set; }
        public double errorCuadratico { get; set; }
        public double erroeEntranamiento { get; set; }
        public double Alfa { get; set; }
        public double [,] Delta { get; set; }

        Random rand = new Random();
        double sumarErrores = 0.0;

        public PerceptronMulticapa()
        {
            sumarErrores = 0.0;
            errorCuadratico = 0.0;
        }

        public double Sigmoidal(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        public void CrearUmbrales()
        {
            u = new double[C + 1, N.Max() + 1];

            for (int c = 2; c <= C; c++)
            {
                for (int i = 1; i <= N[c]; i++)
                {
                    u[c, i] = rand.NextDouble();

                }
            }
        }



    }
}
