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

namespace Perceptron
{
    public partial class Form1 : Form
    {
        int[] arquitecturaRed;
        double[,] patronEntrada;
        double[,] patronSalida;

        int numCapa = 0, neuronaEntrada = 0, neuronaSalida = 0, Npatrones = 0;
        Int32 interacionesMax = 0;
        int[] n;
        double alfa = 0.0, errorMinimo =0.0, neuronaMaximas= 0.0;


        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            leerTxt();

        }

        private void leerTxt()
        {
            string[] datos;
            int numLinea = 0, posI = 0, posJ = 0;
            StreamReader sr = new StreamReader("grafica.txt");
            string linea = sr.ReadLine();
            int posicionSalida = 0;

            while(linea != null)
            {
                switch (numLinea)
                {
                    case 0:
                        datos = linea.Split(' ');
                        numCapa = Convert.ToInt16(datos[0]);
                        arquitecturaRed = new int[Convert.ToInt16(datos[0]) + 1];

                        for (int i = 0; i < arquitecturaRed.Length; i++)
                        {
                            arquitecturaRed[i] = Convert.ToInt16(datos[i]);
                        }

                        n = new int[numCapa + 1];
                        n = arquitecturaRed;
                        break;

                    case 1:
                        alfa = Convert.ToDouble(linea);
                        break;

                    case 2:
                        errorMinimo = Convert.ToDouble(linea);
                        break;

                    case 3:
                        interacionesMax = Convert.ToInt16(linea);
                        break;

                    case 4:
                        Npatrones = Convert.ToInt16(linea);
                        break;

                    default:
                        break;
                }

                if (numLinea == 4)
                {
                    patronEntrada = new double[Npatrones + 1, arquitecturaRed[1]];
                    patronSalida = new double[Npatrones + 1, arquitecturaRed[1]];


                }

                if (numLinea <= (Npatrones + 4) && numLinea > 4)
                {
                    if(linea != "")
                    {
                        datos = linea.Split('\t');
                        for (posJ = 0; posJ < arquitecturaRed[1]; posJ++)
                        {
                            patronEntrada[posI, posJ] = Convert.ToDouble(datos[posJ]);


                        }
                        posI++;

                    }
                }

                if(numLinea > Npatrones + 5 && posicionSalida <Npatrones)
                {
                    if (linea != "")
                    {
                        datos = linea.Split('\t');
                        for (posJ = 0; posJ < arquitecturaRed[1]; posJ++)
                        {
                            patronEntrada[posicionSalida, posJ] = Convert.ToDouble(datos[posJ]);


                        }
                        posicionSalida++;

                    }
                }
                numLinea++;



            }

            sr.Close();

        }
    }
}
