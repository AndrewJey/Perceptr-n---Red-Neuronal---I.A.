/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package perceptron;
/**
 *
 * @author Arbay Fernandez Solano.
 **/
public class Calculo {
    public Calculo() {
    }
    public double RecalcularPesos(double pesoj, double ti, double xi) {
        double wj = pesoj + 0.5 * ti * xi;
        return (wj);
    }
}
