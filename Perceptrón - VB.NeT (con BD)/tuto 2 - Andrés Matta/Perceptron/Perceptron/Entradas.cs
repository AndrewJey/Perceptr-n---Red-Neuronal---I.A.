using System;
using System.Collections.Generic;

namespace Perceptron
{
    public class Entradas
    {
        private int bias, Sd, So, digito;
        private int[] entrada = new int[7];

        public int[] Entrada
        {
            get { return entrada; }
            set { entrada = value ?? new int[7]; }
        }

        public int Digito
        {
            get { return digito; }
            set { digito = value; }
        }

        public int Bias
        {
            get { return bias; }
            set { bias = value; }
        }

        public Entradas(int digito, int[] entrada, int bias, int Sd)
        {
            this.digito = digito;
            this.entrada = entrada ?? new int[7];
            this.bias = bias;
            this.Sd = Sd;
        }

        public int Sd1
        {
            get { return Sd; }
            set { Sd = value; }
        }

        public int So1
        {
            get { return So; }
            set { So = value; }
        }
    }

    public class TrainingResult
    {
        public bool Converged { get; set; }
        public int Epochs { get; set; }
        public float[] Weights { get; set; }
    }

    public static class PerceptronTrainer
    {
        public static int Predict(int[] entrada, int bias, float[] weights)
        {
            if (entrada == null || weights == null || weights.Length < entrada.Length + 1)
            {
                throw new ArgumentException("Dimensiones de entrada o pesos inválidas.");
            }

            float suma = 0f;
            for (int j = 0; j < entrada.Length; j++)
            {
                suma += entrada[j] * weights[j];
            }
            suma += bias * weights[entrada.Length];
            return suma > 0f ? 1 : 0;
        }

        public static void EvaluateAll(IList<Entradas> entradas, float[] weights)
        {
            if (entradas == null) return;
            for (int i = 0; i < entradas.Count; i++)
            {
                entradas[i].So1 = Predict(entradas[i].Entrada, entradas[i].Bias, weights);
            }
        }

        public static bool HasError(IList<Entradas> entradas)
        {
            if (entradas == null) return false;
            for (int i = 0; i < entradas.Count; i++)
            {
                if (entradas[i].Sd1 != entradas[i].So1)
                {
                    return true;
                }
            }
            return false;
        }

        public static TrainingResult Train(IList<Entradas> entradas, float[] initialWeights, int maxEpochs = 1000, float learningRate = 1.0f)
        {
            if (entradas == null || entradas.Count == 0)
            {
                return new TrainingResult { Converged = true, Epochs = 0, Weights = initialWeights ?? new float[8] };
            }

            int inputLen = entradas[0].Entrada.Length;
            float[] weights = new float[inputLen + 1];
            if (initialWeights != null)
            {
                Array.Copy(initialWeights, weights, Math.Min(initialWeights.Length, weights.Length));
            }

            if (maxEpochs < 1) maxEpochs = 1;
            EvaluateAll(entradas, weights);
            int epochs = 0;

            while (HasError(entradas) && epochs < maxEpochs)
            {
                for (int i = 0; i < entradas.Count; i++)
                {
                    int actual = Predict(entradas[i].Entrada, entradas[i].Bias, weights);
                    int diff = entradas[i].Sd1 - actual;
                    if (diff != 0)
                    {
                        for (int j = 0; j < inputLen; j++)
                        {
                            weights[j] += learningRate * diff * entradas[i].Entrada[j];
                        }
                        weights[inputLen] += learningRate * diff * entradas[i].Bias;
                    }
                }

                epochs++;
                EvaluateAll(entradas, weights);
            }

            return new TrainingResult
            {
                Converged = !HasError(entradas),
                Epochs = epochs,
                Weights = weights
            };
        }
    }
}
