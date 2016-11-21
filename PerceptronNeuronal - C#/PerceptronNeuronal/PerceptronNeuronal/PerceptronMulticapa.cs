using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronNeuronal
{
    public class PerceptronMulticapa
    {
        /// <summary>
        /// Propiedades para utilizar en el proceso del perceptron Neuronal Multicapa
        /// </summary>
        public int Capas { get; set; }
        public int [] nArquitectura { get; set; }
        public double[,] xEntrada { get; set; }
        public double[,] ySalida { get; set; }
        public double[,] sPatronesDeseados { get; set; }
        public double[,] aActivacion { get; set; }
        public double[, ,] wPesos { get; set; }
        public double[,] uUmbralesRed { get; set; }
        public int numeroPatrones { get; set; }
        public double maximoEntradas { get; set; }
        public double minimoEntradas { get; set; }
        public double maximoSalidas { get; set; }
        public double minimoSalidas { get; set; }
        public double errorCuadratico { get; set; }
        public double errorEntrenamiento { get; set; }
        public double Alfa { get; set; }
        public double[,] Delta { get; set; }

        Random aleatorio = new Random();
        double sumaErrores = 0.0;

        public PerceptronMulticapa() {
            sumaErrores = 0.0;
            errorCuadratico=0.0;
        }

        public double signoidal(double x) {
            return 1 / (1+Math.Exp(-x));
        }
        public void crearUmbrales() {
            uUmbralesRed = new double[Capas + 1, nArquitectura.Max() + 1];
            for (int i = 2; i <= Capas; i++) {
                for (int j = 1; j <=nArquitectura[i]; j++)
                {
                    uUmbralesRed[i, j] = aleatorio.NextDouble();
                }                
            }
        }
        public void crearPesos() {
            uUmbralesRed = new double[Capas + 1, nArquitectura.Max() + 1];
            for (int i = 1; i <=Capas-1; i++)
            {
                for (int j = 1; j <= nArquitectura[i+1]; j++)
                {
                    for (int k = 1; k <=nArquitectura[Capas]; k++)
                    {
                        wPesos[i, k, j] = aleatorio.NextDouble();
                    }
                }
            }
        }

        public double normalizacion(double valor, double maximo, double minimo) {
            return (1/(maximo-minimo)*(valor-minimo));
        }
        public void normalizarEntrada() {
            double[] numeros = new double[numeroPatrones];
            for (int i = 0; i < numeros.Length; i++)
            {
                xEntrada[i, 0] = normalizacion(xEntrada[i,0], maximoEntradas,minimoEntradas);
            }
        }
        public void normalizarSalida()
        {
            double[] numeros = new double[numeroPatrones];
            for (int i = 0; i < numeros.Length; i++)
            {
                sPatronesDeseados[i, 0] = normalizacion(sPatronesDeseados[i, 0], maximoSalidas, minimoSalidas);
            }
        }
        public void encuentraMaxMinEntradas() {
            double[] numeros = new double[numeroPatrones];
            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = xEntrada[i, 0];
            }
            Array.Sort(numeros);
            maximoEntradas = numeros[numeros.Length-1];
            minimoEntradas = numeros[0];
        }
        public void encuentraMaxMinSalidas()
        {
            double[] numeros = new double[numeroPatrones];
            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = sPatronesDeseados[i, 0];
            }
            Array.Sort(numeros);
            maximoSalidas = numeros[numeros.Length - 1];
            minimoSalidas = numeros[0];
        }
        public void activarEntrada(int patron) {
            for (int i = 1; i <=nArquitectura[i]; i++)
            {
                aActivacion[1, i] = xEntrada[patron, i - 1];
            }
        }
        public void propagacionNeuronal()
        {
            double suma = 0.0;
            for (int i = 2; i <=Capas; i++)
            {
                for (int j = 1; j <=nArquitectura[Capas]; j++)
                {
                    suma = 0.0;
                    for (int k = 1; k <=  nArquitectura[i - 1]; k++)
                    {
                        suma += wPesos[i - 1, k, j] * aActivacion[i - 1, k];
                    }
                    aActivacion[i, j] = suma + uUmbralesRed[i,j];
                    aActivacion[i, j] = signoidal(aActivacion[i, j]);
                }
            }
        }
        public void errorCuadrat(int nPatron) {
            double auxiliar = 0.0;
            for (int i = 0; i <=nArquitectura[Capas]; i++)
            {
                auxiliar += Math.Pow(sPatronesDeseados[nPatron,0]-aActivacion[Capas,i],2);
                ySalida[nPatron, 0] = aActivacion[Capas, i];
            }
            errorCuadratico = auxiliar / 2;
            sumaErrores = errorCuadratico + sumaErrores;
        }
        public void errorAprendizaje() {
            errorEntrenamiento = sumaErrores / numeroPatrones;
            sumaErrores = 0;
        }
        public void retroPropagacion(int patron) {
            calcularDeltas(patron);
            calcularPesosYUmbrales();
        }

        private void calcularDeltas(int patron)
        {
            double suma = 0.0;
            //Caso A
            for (int i = 1; i <= nArquitectura[Capas]; i++)
            {
                Delta[Capas, i] = (sPatronesDeseados[patron, i - 1]-aActivacion[Capas,i]* aActivacion[Capas, i]*(1-aActivacion[Capas,i]));
            }//Caso B
            for (int i = Capas-1; i > 1; i--)
            {
                for (int j = 1; j <= nArquitectura[i]; j++)
                {
                    for (int k = 1;  k <= nArquitectura[i+1];  k++)
                    {
                        suma += Delta[i + 1, k] * wPesos[i, j, k];
                    }
                    Delta[i, j] = aActivacion[i, j] * (1 - aActivacion[i, j]) * suma;
                }
            }
        }

        public void calcularPesosYUmbrales() {
            for (int i = 1; i <=Capas-1; i++)
            {
                for (int j = 1; j <=nArquitectura[i+1] ; j++)
                {
                    for (int k = 1; k <=nArquitectura[Capas] ; k++)
                    {
                        wPesos[i, k, j] = wPesos[i, k, j] + Alfa * Delta[i + 1, j] * aActivacion[i, k];
                    }
                    uUmbralesRed[i + 1, j] = uUmbralesRed[i + 1, j] + Alfa * Delta[i + 1, j];
                }
            }
        }
    }
}
