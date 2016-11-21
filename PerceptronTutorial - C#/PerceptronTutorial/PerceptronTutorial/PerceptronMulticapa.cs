using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronTutorial
{
    class PerceptronMulticapa
    {
        public int C { get; set; }
        public int[] n { get; set; }
        public double[,] x { get; set; }
        public double[,] y { get; set; }
        public double[,] s { get; set; }
        public double[,] a { get; set; }
        public double[, ,] w { get; set; }
        public double[,] u { get; set; }
        public int numeroPatrones { get; set; }
        public double maximoEntradas { get; set; }
        public double minimoEntradas { get; set; }
        public double maximoSalidas { get; set; }
        public double minimoSalidas { get; set; }
        public double errorCuadratico { get; set; }
        public double errorEntrenamiento { get; set; }
        public double alfa { get; set; }
        public double[,] delta { get; set; }

        Random rand = new Random();
        double sumaErrores = 0.0;
        /// <summary>
        /// 
        /// </summary>
        public PerceptronMulticapa()
        {
            sumaErrores = 0.0;
            errorCuadratico = 0.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double sigmoidal(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        /// <summary>
        /// 
        /// </summary>
        public void crearUmbrales()
        {
            u = new double[C + 1, n.Max() + 1];

            for (int c = 2; c <= C; c++)
            {
                for (int i = 0; i <= n[C]; i++)
                {
                    u[c, i] = rand.NextDouble();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void crearPesos()
        {
            w = new double[C + 1, n.Max() + 1, n.Max() + 1];

            for (int c = 1; c <= C - 1; c++)
            {
                for (int i = 1; i <= n[c + 1]; i++)
                {
                    for (int j = 1; j <= n[c]; j++)
                    {
                        w[c, j, i] = rand.NextDouble();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="_maximo"></param>
        /// <param name="_minimo"></param>
        /// <returns></returns>
        public double normalizacion(double valor, double _maximo, double _minimo)
        {
            return (1 / (_maximo - _minimo) * (valor - _minimo));
        }

        /// <summary>
        /// 
        /// </summary>
        public void normalizarEntradas()
        {
            double[] numero = new double[numeroPatrones];

            for (int i = 0; i < numero.Length; i++)
            {
                x[i, 0] = normalizacion(x[i, 0], maximoEntradas, minimoEntradas);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void normalizarSalidas()
        {
            double[] numero = new double[numeroPatrones];

            for (int i = 0; i < numero.Length; i++)
            {
                s[i, 0] = normalizacion(s[i, 0], maximoSalidas, minimoSalidas);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void encuentraMaxMinSalidas()
        {
            double[] numero = new double[numeroPatrones];
            for (int i = 0; i < numero.Length; i++)
            {
                numero[i] = s[i, 0];
            }

            Array.Sort(numero);

            maximoSalidas = numero[numero.Length - 1];
            minimoSalidas = numero[0];
        }

        /// <summary>
        /// 
        /// </summary>
        public void encuentraMaxMinEntradas()
        {
            double[] numero = new double[numeroPatrones];
            for (int i = 0; i < numero.Length; i++)
            {
                numero[i] = x[i, 0];
            }

            Array.Sort(numero);

            maximoEntradas = numero[numero.Length - 1];
            minimoEntradas = numero[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patron"></param>
        public void activacionEntrada(int patron)
        {
            for (int i = 1; i <= n[1]; i++)
            {
                a[1, i] = x[patron, i - 1];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void propafacionNeuronas()
        {
            double suma = 0.0;
            for (int c = 2; c <= C; c++)
            {
                for (int i = 1; i <= n[c]; i++)
                {
                    suma = 0.0;
                    for (int j = 1; j <= n[c - 1]; j++)
                    {
                        suma += w[c - 1, j, i] * a[c - 1, j];
                    }
                    a[c, i] = suma + u[c, i];
                    a[c, i] = sigmoidal(a[c, i]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPatron"></param>
        public void errorCuadraticos(int nPatron)
        {
            double temp = 0.0;
            for (int i = 1; i <= n[C]; i++)
            {
                temp += Math.Pow(s[nPatron, 0] - a[C, i], 2);
                y[nPatron, 0] = a[C, i];
            }
            errorCuadratico = temp / 2;
            sumaErrores = errorCuadratico + sumaErrores;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patron"></param>
        public void retroPorpagacion(int patron)
        {
            calcularDeltas(patron);
            calcularPesosYUmbrales();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patron"></param>
        public void calcularDeltas(int patron)
        {
            double suma = 0.0;

            for (int i = 1; i <= n[C]; i++)
            {
                delta[C, i] = (s[patron, i - 1] - a[C, i] * a[C, i] * (1 - a[C, i]));
            }

            for (int c = C - 1; c > 1; c--)
            {
                for (int j = 1; j <= n[c]; j++)
                {
                    suma = 0.0;
                    for (int i = 1; i <= n[c + 1]; i++)
                    {
                        suma += delta[c + 1, i] * w[c, j, i];
                    }
                    delta[c, j] = a[c, j] * (1 - a[c, j]) * suma;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void calcularPesosYUmbrales()
        {
            for (int c = 1; c <= C - 1; c++)
            {
                for (int i = 1; i <= n[c + 1]; i++)
                {
                    for (int j = 1; j <= n[c]; j++)
                    {
                        w[c, j, i] = w[c, j, i] + alfa * delta[c + 1, i] * a[c, j];
                    }
                    u[c + 1, i] = u[c + 1, i] + alfa * delta[c + 1, i];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void errorAprendizaje()
        {
            errorEntrenamiento = sumaErrores / numeroPatrones;
            sumaErrores = 0;
        }
    }
}
