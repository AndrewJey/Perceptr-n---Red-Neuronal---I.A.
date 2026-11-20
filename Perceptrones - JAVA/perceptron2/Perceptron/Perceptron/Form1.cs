using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        private const int MaxEpochs = 1000;
        private List<Entradas> entradas = new List<Entradas>();
        private readonly Conexion datos = new Conexion();
        private float[] W = new float[8];

        public Form1()
        {
            InitializeComponent();
        }

        private void crearVectorW()
        {
            W[0] = 2f;
            W[1] = 0f;
            W[2] = 4f;
            W[3] = -6f;
            W[4] = 4f;
            W[5] = 0f;
            W[6] = 5f;
            W[7] = -10f;
        }

        private void actualizarEtiquetasPesos()
        {
            label8.Text = W[0].ToString("0.###");
            label9.Text = W[1].ToString("0.###");
            label10.Text = W[2].ToString("0.###");
            label11.Text = W[3].ToString("0.###");
            label12.Text = W[4].ToString("0.###");
            label13.Text = W[5].ToString("0.###");
            label14.Text = W[6].ToString("0.###");
            label15.Text = W[7].ToString("0.###");
        }

        private void aplicarAprendizaje()
        {
            TrainingResult result = PerceptronTrainer.Train(entradas, W, MaxEpochs, 1.0f);
            W = result.Weights;
            label19.Text = result.Epochs.ToString();
            actualizarEtiquetasPesos();
            cargarTabla();

            string fuente = datos.UsandoDatosLocales ? "dataset local de 7 segmentos" : "base de datos SQL";
            string estado = result.Converged
                ? string.Format("Entrenamiento completado con convergencia en {0} épocas ({1}).", result.Epochs, fuente)
                : string.Format("Se alcanzó el límite de {0} épocas sin convergencia total ({1}).", MaxEpochs, fuente);
            MessageBox.Show(this, estado, "Perceptrón", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool error()
        {
            return PerceptronTrainer.HasError(entradas);
        }

        private void solucionObtenida()
        {
            PerceptronTrainer.EvaluateAll(entradas, W);
            cargarTabla();
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
        }

        public void cargarTabla()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < entradas.Count; i++)
            {
                dataGridView1.Rows.Add(new object[] { entradas[i].Sd1, entradas[i].So1 });
            }
        }
    }
}
