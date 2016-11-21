using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perceptron
{
    class Entradas
    {
        private int bias, Sd, So, digito;


        private int[] entrada = new int[7];
        

        public int[] Entrada
        {
            get { return entrada; }
            set { entrada = value; }
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

        public Entradas(int digito, int[] entrada, int bias, int Sd )//
        {
            this.digito = digito;

            this.entrada = entrada;
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
}
