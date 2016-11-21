using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        private List<Entradas> entradas = new List<Entradas>();
        private Conexion datos = new Conexion();
        private float[] W = new float[8];

        public Form1()
        {
            InitializeComponent();
           
        }

        private void crearVectorW()
        {
            Random r = new Random();
            W[0] =2;//(float)r.NextDouble();
            W[1] =0;//(float)r.NextDouble();
            W[2] =4;//(float)r.NextDouble();
            W[3] =-6;//(float)r.NextDouble();
            W[4] =4;//(float)r.NextDouble();
            W[5] =0;//(float)r.NextDouble();
            W[6] =5;//(float)r.NextDouble();
            W[7] = -10;//(float)r.NextDouble();
        }

        private void aplicarAprendizaje()
        {
            int epocas = 0;
            while (error())
            {
                for (int i = 0; i < entradas.Count; i++)
                {
                    if (entradas[i].Sd1 != entradas[i].So1)
                    {
                        MessageBox.Show("Error del Perceptron");
                        if (entradas[i].So1 == 0 && entradas[i].Sd1==1)
                        {
                            for (int j = 0; j < W.Length-1; j++)
                            {
                                W[j] += entradas[i].Entrada[j];
                            }
                            W[7] += entradas[i].Bias;
                            break;
                        }

                        if (entradas[i].So1 == 1 && entradas[i].Sd1 == 0)
                        {
                            for (int j = 0; j < W.Length - 1; j++)
                            {
                                W[j] -= entradas[i].Entrada[j];
                            }
                            W[7] -= entradas[i].Bias;
                            break;
                        }

                    }
                }

                epocas += 1;
                label19.Text = epocas.ToString();
                solucionObtenida();
                label8.Text = W[0].ToString();
                label9.Text = W[1].ToString();
                label10.Text = W[2].ToString();
                label11.Text = W[3].ToString();
                label12.Text = W[4].ToString();
                label13.Text = W[5].ToString();
                label14.Text = W[6].ToString();
                label15.Text = W[7].ToString();
                //textBox1.Text = "Epoca: "+epocas+"\nW1:"+W[0].ToString()+"\n"+
                //                "W2:"+W[1].ToString()+"\n"+
                //                "W3:"+W[2].ToString()+"\n"+
                //                "W4:"+W[3].ToString()+"\n"+
                //                "W5:"+W[4].ToString()+"\n"+
                //                "W6:"+W[5].ToString()+"\n"+
                //                "W7:"+W[6].ToString()+"\n"+
                //                "W8:"+W[7].ToString()+"\n";
            }

            label8.Text = W[0].ToString();
            label9.Text = W[1].ToString();
            label10.Text = W[2].ToString();
            label11.Text = W[3].ToString();
            label12.Text = W[4].ToString();
            label13.Text = W[5].ToString();
            label14.Text = W[6].ToString();
            label15.Text = W[7].ToString();
            MessageBox.Show("Tarea terminada");
        }

        public Boolean error()
        {
            for (int i = 0; i < entradas.Count; i++)
            {
                if (entradas[i].Sd1 != entradas[i].So1)
                    return true;
                
            }

            return false;
        }

        private void solucionObtenida()
        {
            float suma=0;

            for (int i = 0; i < entradas.Count; i++)
            {
                for (int j = 0; j < 7; j++)
			    {
                    suma += entradas[i].Entrada[j] * W[j];
			    }

                suma += entradas[i].Bias * W[7];

                if (suma > 0)
                    entradas[i].So1 = 1;
                else
                    entradas[i].So1 = 0;
                suma = 0;
            }

           
            cargarTabla();
            
            suma = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void cargarDatos()
        {
            entradas = datos.cargarTabla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crearVectorW();
            cargarDatos();
            solucionObtenida();
            aplicarAprendizaje();
           // cargarTabla();
        }

        public void cargarTabla()
        {
            
            dataGridView1.Rows.Clear();
            label15.Text = "a";
            for (int i = 0; i < entradas.Count; i++)
            {
                dataGridView1.Rows.Add(new object[] { entradas[i].Sd1, entradas[i].So1 });//carga la lista de colores en el datagrid
            }
            
           // Thread.Sleep(5000);

        }



    }
}
