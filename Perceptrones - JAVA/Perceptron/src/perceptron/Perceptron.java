/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package perceptron;
/**
 *
 * @author Arbay Fernandez Solano
 **/
public class Perceptron {
    /**
     * @param args the command line arguments
     **/
    public static void main(String[] args) {
        Calculo cl = new Calculo();
        double pesos[] = {0.0, 0.0, 0.0};
        for (int i = 0; i < pesos.length; i++) {
            pesos[i] = Math.random(); //pesos aleatorios
            System.out.println("Peso " + i + " = " + pesos[i]);
        }
        pesos[0] = 0.28714321804133325;
        pesos[1] = 0.5746590460845108;
        pesos[2] = 0.7228245598139071;
        System.out.println("**************************************************************");
        //Salidas que debe aprender
        double[] salidas = {1, 1, 1, -1};
        double[][] entradas = {{1, 1, -1}, {1, -1, -1}, {-1, 1, -1}, {-1, -1, -1}};
        double yi = 0; //salida calculada.
        int i = 0; //control del proceso.

        while (i < entradas.length) {
            yi = pesos[0] * entradas[i][0] + pesos[1] * entradas[i][1] + pesos[2] * entradas[i][2];
            if (yi >= 0) {
                yi = 1;
            } else {
                yi = -1;
            }
            if (yi == salidas[i]) {
                System.out.println("Entrada* (" + entradas[i][0] + "),(" + entradas[i][1] + ")Salida(" + salidas[i] + ")Calculada(Si)" + yi);
            } else {
                System.out.println("Entrada* (" + entradas[i][0] + "),(" + entradas[i][1] + ")Salida(" + salidas[i] + ")Calculada(No)" + yi);
                System.out.println("*******************************correcion de pesos**************************************");
                for (int j = 0; j < pesos.length; j++) {
                    pesos[j] = cl.RecalcularPesos(pesos[j], salidas[i], entradas[i][j]);
                }
                System.out.println("******************************************************************************");
                i = -1;//para que inicie el proceso de nuevo. 
            }
            i++;
        }
    }
}