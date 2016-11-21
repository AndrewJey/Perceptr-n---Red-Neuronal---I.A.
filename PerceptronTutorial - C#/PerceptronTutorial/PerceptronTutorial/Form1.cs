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

namespace PerceptronTutorial
{
    public partial class Form1 : Form
    {
        int[] arquitecturaRed;
        double[,] patronesEntrada;
        double[,] patronesSalida;

        int numeroCapas = 0, neuronasEntrada = 0, neuronasSalida = 0, nPatrones = 0;
        Int32 iteracionesMaximas = 0;
        int[] n;
        double alfa = 0.0, errorMinimo = 0.0, neuronasMaximas = 0.0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            leerTXT();

            try
            {
                int[] temp = new int[numeroCapas + 1];
                Array.Copy(arquitecturaRed, temp, arquitecturaRed.Length);
                Array.Sort(temp);

                neuronasMaximas = temp[temp.Length - 1];

                PerceptronMulticapa neurona = new PerceptronMulticapa();
                neurona.numeroPatrones = nPatrones;
                neurona.n = n;
                neurona.x = patronesEntrada;
                neurona.s = patronesSalida;
                neurona.errorEntrenamiento = 1;
                neurona.C = numeroCapas;
                neurona.a = new double[neurona.C + 1, (int)neuronasMaximas + 1];
                neurona.y = new double[neurona.numeroPatrones + 1, n[neurona.C] + 1];
                neurona.delta = new double[neurona.C + 1, (int)neuronasMaximas + 1];
                neurona.s = new double[neurona.numeroPatrones + 1, n[neurona.C] + 1];
                neurona.errorCuadratico = 0;
                neurona.alfa = alfa;
                neurona.s = patronesSalida;
                neurona.crearPesos();
                neurona.crearUmbrales();
                neurona.encuentraMaxMinEntradas();
                neurona.encuentraMaxMinSalidas();
                neurona.normalizarEntradas();
                neurona.normalizarSalidas();

                while (neurona.errorEntrenamiento >= errorMinimo && iteracionesMaximas >= 0)
                {
                    neurona.errorCuadratico = 0;
                    for (int i = 0; i < nPatrones; i++)
                    {
                        neurona.activacionEntrada(i);
                        neurona.propafacionNeuronas();
                        neurona.errorCuadraticos(i);
                        neurona.retroPorpagacion(i);
                    }
                    neurona.errorAprendizaje();
                    iteracionesMaximas--;
                }

                guardarSalida(neurona);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
        private void guardarSalida(PerceptronMulticapa neuronas)
        {
            try
            {
                StreamWriter archivo = new StreamWriter("Entrenamiento.txt");

                string temp = "";
                for (int i = 1; i < neuronas.n.Length; i++)
                {
                    temp += " " + neuronas.n[i];
                }

                archivo.WriteLine(neuronas.n.Length - 1 + temp);
                archivo.WriteLine(neuronas.alfa);
                archivo.WriteLine(errorMinimo);

                for (int c = 1; c <= neuronas.C - 1; c++)
                {
                    for (int i = 1; i <= neuronas.n[c + 1]; i++)
                    {
                        for (int j = 1; j <= neuronas.n[c]; j++)
                        {
                            archivo.WriteLine(neuronas.w[c, j, i]);
                        }
                    }
                }

                archivo.WriteLine("\n");

                for (int c = 2; c <= neuronas.C; c++)
                {
                    for (int i = 1; i <= neuronas.n[c]; i++)
                    {
                        archivo.WriteLine(neuronas.u[c, i]);
                    }
                }

                archivo.WriteLine("\n");

                for (int i = 0; i < neuronas.numeroPatrones; i++)
                {
                    for (int j = 0; j < neuronas.n[1]; j++)
                    {
                        archivo.WriteLine(neuronas.s[i, j] + "\t" + neuronas.y[i, j]);
                    }
                }

                archivo.WriteLine("\n");
                archivo.WriteLine(neuronas.errorEntrenamiento);
                archivo.Close();
                MessageBox.Show("Archivo creado");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
        public void leerTXT()
        {
            string[] datos;
            int numeroLinea = 0, posI = 0, posJ = 0;
            int posicionSalida = 0;

            StreamReader sr = new StreamReader("Grafico.txt");
            string linea = sr.ReadLine();

            while (linea != null)
            {
                switch (numeroLinea)
                {
                    case 0:
                        datos = linea.Split(' ');
                        numeroCapas = Convert.ToInt16(datos[0]);
                        arquitecturaRed = new int[Convert.ToInt16(datos[0]) + 1];
                        for (int i = 0; i < arquitecturaRed.Length; i++)
                        {
                            arquitecturaRed[i] = Convert.ToInt16(datos[i]);
                        }
                        n = new int[numeroCapas + 1];
                        n = arquitecturaRed;
                        break;
                    case 1:
                        alfa = Convert.ToDouble(linea);
                        break;
                    case 2:
                        errorMinimo = Convert.ToDouble(linea);
                        break;
                    case 3:
                        iteracionesMaximas = Convert.ToInt32(linea);
                        break;
                    case 4:
                        nPatrones = Convert.ToInt16(linea);
                        break;
                }
                if (numeroLinea == 4)
                {
                    patronesEntrada = new double[nPatrones + 1, arquitecturaRed[1]];
                    patronesSalida = new double[nPatrones + 1, arquitecturaRed[1]];
                }

                if (numeroLinea <= (nPatrones + 4) && numeroLinea > 4)
                {
                    if (linea != "")
                    {
                        datos = linea.Split('\t');
                        for (posJ = 0; posJ < arquitecturaRed[1]; posJ++)
                        {
                            patronesEntrada[posI, posJ] = Convert.ToDouble(datos[posJ]);
                        }
                        posI++;
                    }
                }
                if (numeroLinea > nPatrones + 5 && posicionSalida < nPatrones)
                {
                    if (linea != "")
                    {
                        datos = linea.Split('\t');
                        for (posJ = 0; posJ < arquitecturaRed[1]; posJ++)
                        {
                            patronesSalida[posicionSalida, posJ] = Convert.ToDouble(datos[posJ]);
                        }
                        posicionSalida++;
                    }
                }
                numeroLinea++;
                linea = sr.ReadLine();
            }
            sr.Close();
        }
    }
}
