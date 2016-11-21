using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerceptronNeuronal
{
    public partial class Form_Perceptron : Form
    {
        /// <summary>
        /// Atributos Arrays para la estructura de la neurona
        /// </summary>
        int[] arquitecturaRed;
        double[,] patronEntrada;
        double[,] patronSalida;
        /// <summary>
        /// Atributos de interaccion con los componentes array para la intreaccion de la neurona
        /// </summary>
        int numCapas = 0; //numero de capas de la neurona
        int neuronaEntrada = 0;
        int neuronaSalida = 0;
        int numPatrones = 0;
        Int32 interacciones=0;
        int[] numArquitecturatRed; //Campo donde estara la arquitectura
        double alfa = 0.0;
        double errorMinimo = 0.0;
        double neuronasMaximas = 0.0;

        /// <summary>
        /// Constructor
        /// </summary>
        public Form_Perceptron()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Metodo boton para llamar a lecturaTxt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Entrenar_Click(object sender, EventArgs e)
        {
            lecturaTxt();
            try
            {
                int[] auxiliar = new int[numCapas+1];
                Array.Copy(arquitecturaRed,auxiliar,arquitecturaRed.Length);
                Array.Sort(auxiliar);

                neuronasMaximas = auxiliar[auxiliar.Length - 1];
                PerceptronMulticapa neuronas = new PerceptronMulticapa();
                neuronas.numeroPatrones = numPatrones;
                neuronas.nArquitectura = numArquitecturatRed;
                neuronas.xEntrada = patronEntrada;
                
                neuronas.errorEntrenamiento = 1;
                neuronas.Capas = numCapas;
                neuronas.aActivacion = new double[neuronas.Capas + 1, (int) neuronasMaximas + 1];
                neuronas.ySalida = new double[neuronas.numeroPatrones + 1, numArquitecturatRed[neuronas.Capas] + 1];
                neuronas.Delta = new double[neuronas.Capas+1,(int)neuronasMaximas+1];
                neuronas.sPatronesDeseados = new double[neuronas.numeroPatrones+1,numArquitecturatRed[neuronas.Capas]+1];
                neuronas.errorCuadratico = 0;
                neuronas.Alfa = alfa;
                neuronas.sPatronesDeseados = patronSalida;
                neuronas.crearPesos();
                neuronas.crearUmbrales();
                neuronas.encuentraMaxMinEntradas();
                neuronas.encuentraMaxMinSalidas();
                neuronas.normalizarEntrada();
                neuronas.normalizarSalida();

                while (neuronas.errorEntrenamiento>=errorMinimo&& interacciones>=0)
                {
                    neuronas.errorCuadratico = 0;
                    for (int i = 0; i < numPatrones; i++)
                    {
                        neuronas.activarEntrada(i);
                        neuronas.propagacionNeuronal();
                        neuronas.errorCuadrat(i);
                        neuronas.retroPropagacion(i);

                    }
                    neuronas.errorAprendizaje();
                    interacciones--;
                }
                GuardarSalida(neuronas);
            }
            catch (Exception ex)
            {

                MessageBox.Show("--> "+ex);
            }
        }

        private void GuardarSalida(PerceptronMulticapa neurona)
        {
            try
            {
                StreamWriter archivo = new StreamWriter("entrenamiento.txt");
                string auxiliar = "";
                for (int i = 1; i <neurona.nArquitectura.Length; i++)
                {
                    auxiliar += " " + neurona.nArquitectura[i];
                }
                archivo.WriteLine(neurona.nArquitectura.Length-1+auxiliar);
                archivo.WriteLine(neurona.Alfa);
                archivo.WriteLine(errorMinimo);

                for (int i = 1; i <= neurona.Capas - 1; i++)
                {
                    for (int j = 1; j <= neurona.nArquitectura[i + 1]; j++)
                    {
                        for (int k = 1; k <= neurona.nArquitectura[i]; k++)
                        {
                            archivo.WriteLine(neurona.wPesos[i, k, j]);
                        }
                    }
                }

                archivo.WriteLine("\n");

                for (int i = 2; i <= neurona.Capas; i++)
                {
                    for (int j = 1; j <= neurona.nArquitectura[i]; j++)
                    {
                        archivo.WriteLine(neurona.uUmbralesRed[i, j]);
                    }
                }

                archivo.WriteLine("\n");

                for (int i = 0; i < neurona.numeroPatrones; i++)
                {
                    for (int j = 0; j < neurona.nArquitectura[i]; j++)
                    {
                        archivo.WriteLine(neurona.sPatronesDeseados[i,j]+"\t"+neurona.ySalida[i,j]);
                    }
                }
                archivo.WriteLine("\n");
                archivo.WriteLine(neurona.errorEntrenamiento);
                archivo.Close();

                MessageBox.Show("Archivo Creado","Mensaje del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }   
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Metodo lectura y procedimiento del txt
        /// </summary>
        private void lecturaTxt()
        {
            string[] datos;
            int numLineas = 0; //numero de las lineas del txt
            int i=0,j=0; //indices paa las posiciones
            StreamReader str = new StreamReader("grafico.txt");
            string linea = str.ReadLine();
            int posicionSalida = 0;

            while (linea!=null) {
                switch (numLineas) {
                    case 0:
                        datos = linea.Split(' ');
                        numCapas = Convert.ToInt16(datos[0]);
                        arquitecturaRed = new int[Convert.ToInt16(datos[0]) + 1];
                        for (int x =1; x < arquitecturaRed.Length;x++) {
                            arquitecturaRed[x] = Convert.ToInt16(datos[x]);
                        }
                        numArquitecturatRed = new int[numCapas + 1];
                        numArquitecturatRed = arquitecturaRed;
                        break;
                    case 1:
                        alfa = Convert.ToDouble(linea);
                        break;
                    case 2:
                        errorMinimo = Convert.ToDouble(linea);
                        break;
                    case 3:
                        interacciones = Convert.ToInt32(linea);
                        break;
                    case 4:
                        numPatrones = Convert.ToInt16(linea);
                        break;
                    default:
                        break;
                }
                if (numLineas==4) {
                    patronEntrada = new double[numPatrones + 1, arquitecturaRed[1]];
                    patronSalida = new double[numPatrones + 1, arquitecturaRed[1]];
                }
                if (numLineas<=(numPatrones+4) && numLineas>4) {
                    if (linea != "") {
                        datos = linea.Split('\t');
                        for (j=0;j<arquitecturaRed[1];j++) {
                            patronEntrada[i, j] = Convert.ToDouble(datos[j]);
                        }
                        i++;
                    }
                }
                if ((numLineas>numPatrones+5) && (posicionSalida<numPatrones)) {
                    if (linea != "")
                    {
                        datos = linea.Split('\t');
                        for (j = 0; j < arquitecturaRed[1]; j++)
                        {
                            patronSalida[posicionSalida, j] = Convert.ToDouble(datos[j]);
                        }
                        posicionSalida++;
                    }
                }
                numLineas++;
                linea = str.ReadLine();
            }
            str.Close();
        }
        /// <summary>
        /// Salida del sistema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
