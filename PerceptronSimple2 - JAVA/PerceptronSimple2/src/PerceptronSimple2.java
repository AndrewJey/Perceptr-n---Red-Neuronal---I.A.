import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JOptionPane;
import javax.swing.*;
import java.math.BigDecimal;
import java.math.RoundingMode;
 
 
public class PerceptronSimple2 implements ActionListener{
     
    JButton entrenar=new JButton("ENTRENAR");
    JMenuBar menu=new JMenuBar();
    JMenu compuertasLogicas=new JMenu("ELEGIR COMPUERTA LOGICA");    
    JMenuItem and=new JMenuItem("AND");
    JMenuItem or=new JMenuItem("OR");
    JButton probar=new JButton("PROBAR");
    ImageIcon icono=new ImageIcon("perceptronsimple.png");
    JLabel imagen=new JLabel(icono);
   
    static float funcionActivacion=0.0f;    
    static float error=1.0f;
    int iteraciones = 0;
    static float[][] entradas =    
        {
                //Entrada 1, Entrada 2, Umbral
                //  {x1 , x2  , -1 }
               
            {1f , 1f  , -1f},    
            {1f , -1f , -1f},
            {-1f, 1f  , -1f},
            {-1 , -1f , -1f}      
        };            
     // salidasDeseadas para el perceptron                            
     float[] salidasDeseadas=new float[4];
     int establecioSalidas=0;
     static float[] pesos = {1.2f,-1.2f,-0.4f};
     static float factorAprendizaje=.5f;
       
     public static void main(String args[]){
        new PerceptronSimple2();
    }
    PerceptronSimple2(){
        JFrame ventana=new JFrame();
        ventana.setVisible(true);
        ventana.setSize(500,300);
        ventana.setDefaultCloseOperation(ventana.EXIT_ON_CLOSE);
        ventana.setTitle("PERCEPTRON SIMPLE");
        ventana.setResizable(false);
        ventana.setLocationRelativeTo(null);
        JDesktopPane esc=new JDesktopPane();
        //les asignamos una posicion a los componentes
        entrenar.setBounds(140,220,100,20);
        probar.setBounds(260,220,100,20);        
        imagen.setBounds(5,10,500,250);
       
        //ponemos a escuchar por algun evento a los botones y menuitem
        entrenar.addActionListener(this);        
        probar.addActionListener(this);
        or.addActionListener(this);
        and.addActionListener(this);
       
        //agregamos los 2 menuitem al menu
        compuertasLogicas.add(and);
        compuertasLogicas.add(or);
        //agregamos el menu al menubar
        menu.add(compuertasLogicas);
        //agregamos todos los componentes al escritorio
        esc.add(entrenar);
        esc.add(probar);      
        esc.add(imagen);
        //indicamos cual es el menu y agreamos el escritorio a la ventana
        ventana.setJMenuBar(menu);
        ventana.add(esc);                    
    }    
     @Override
    public void actionPerformed(ActionEvent e){
        if(e.getSource()==entrenar){            
            if(establecioSalidas==0){
               JOptionPane.showMessageDialog(null,"Primero eliga la compuerta logica que debera aprender el perceptron");  
            }else{
               System.out.println("Pesos Iniciales");
                           //imprimimos los pesos que definimos anteriormente
               for(int i = 0; i < pesos.length; i++){
                 System.out.println(pesos[i]);
               }
               System.out.println("");        
               int contador = 0;        
                           /*recorremos las entradas y se le van pasando a la funcionActivacion() esta funcion nos regresa la salida para dichas entradas.
                             por ejemplo si elegimos la compuerta OR y se le manda la entradas:
                             x1 = 1  , x2 = 0 la salida que nos deberia de regresa la funcionActivacion() es 1 esta salida se le manda
                             al metodo error este verifica si hay o no error
                           */
               for(int i = 0; i <= entradas[0].length; i++){
                 System.out.println("ITERACION "+contador+":");
                 float funcionActivacion = funcionActivacion(entradas[i]);
                 System.out.println("activacion: "+funcionActivacion);
                 float error = error(salidasDeseadas[i]);
                 System.out.println("Error: "+error);
                 if(error==0f){      
                                   //Entra aqui si no hay error
                   System.out.println("--------------------------------------");
                   contador++;    
                 }else{
                    //Si hay error, recalcula los pesos
                    calculaPesos(entradas[i],salidasDeseadas[i]);
                    /*ponemos i=-1 para que empiece a sacar la funcion
                    de activacion  desde el inicio  con los nuevos pesos*/
                    i=-1;                      
                    contador = 0;  
                 }
               }
               JOptionPane.showMessageDialog(null,"LA RED YA ESTA ENTRENADA");              
               System.out.println("Pesos Finales");
               for(int i = 0; i < pesos.length; i++){
                 System.out.println(pesos[i]);
               }
       
           }                                      
                 // ya una ves que el perceptron este entrenado podemos dar clic en el boton probar y nos ira pidiendo las entradas
                 // y nos debera de dar la salida correcta para cada entrada
        }else if(e.getSource()==probar){                                                                      
              String x1=JOptionPane.showInputDialog(null,"Ingresa la primera entrada");                      
              String x2=JOptionPane.showInputDialog(null,"Ingresa la segunda entrada");                      
              float [] entradasPrueba=new float[3];
              entradasPrueba [0]=Float.parseFloat(x1); //entrada neurona 1
              entradasPrueba [1]=Float.parseFloat(x2); //entrada neurona 2
              entradasPrueba [2]=-1f;                  //entrada para el umbral
              float resultado = probarRed(entradasPrueba);              
              JOptionPane.showMessageDialog(null,resultado);
          }
        if(e.getSource()==or){
                 // definimos las salidas deseadas para la compuerta logica or
          salidasDeseadas[0]=1f;
          salidasDeseadas[1]=1f;
          salidasDeseadas[2]=1f;
          salidasDeseadas[3]=-1f;          
          establecioSalidas=1;
       }
        if(e.getSource()==and){
                // definimos las salidas deseadas para la compuerta logica and
          salidasDeseadas[0]=1f;
          salidasDeseadas[1]=-1f;
          salidasDeseadas[2]=-1f;
          salidasDeseadas[3]=-1f;          
          establecioSalidas=1;
        }
    }  
     
     public static float funcionActivacion(float[] entradas){
        funcionActivacion = 0.0f;
        System.out.println("metodo funcionActivacion");
        for(int i = 0; i < entradas.length; i++){
           
            // se multiplica cada peso por cada entrada y se suma
            funcionActivacion += pesos[i] * entradas[i];
           
            //redondeamos a 2 decimales el valor de la funcion activacion
            String val = funcionActivacion+"";
            BigDecimal big = new BigDecimal(val);
            big = big.setScale(2, RoundingMode.HALF_UP);    
            funcionActivacion=big.floatValue();
            System.out.println("Multiplicacion");
            System.out.println("w"+i+" * "+"x "+i);
            System.out.println(pesos[i] +"*" +entradas[i]);
           
        }
        System.out.println("y = "+funcionActivacion);
        //se determina el valor de la salida
        if(funcionActivacion >= 0)
            funcionActivacion = 1;
        else if(funcionActivacion < 0)
            funcionActivacion = -1;
       
        return funcionActivacion;
    }
     //metodo para verificar si hay o no error
        public static float error(float salidaDeseada){
          System.out.println("Salida deseada - salida");
          error = salidaDeseada - funcionActivacion;
          System.out.println(salidaDeseada+" - "+funcionActivacion);
          return error;
        }
      //metodo para el reajuste de pesos
    public void calculaPesos(float[] entradas,float salidas){
     if(error != 0){            
       for(int i = 0; i < entradas.length; i++){                
         System.out.println(pesos[i]+" + (2 * .5) * "+salidas+" * "+entradas[i]);                              
         this.pesos[i]=pesos[i]+(2.0f*.5f)*(salidas*entradas[i]);                  
         String val = this.pesos[i]+"";
         BigDecimal big = new BigDecimal(val);
         big = big.setScale(2, RoundingMode.HALF_UP);    
         funcionActivacion=big.floatValue();                
         System.out.println("salida");
         System.out.println("AHORA LOS PESOS CAMBIARON A :"+ this.pesos[i]);
            }
        }
    }        
      public float probarRed(float [] entradasPrueba){
        float result;        
        funcionActivacion = 0.0f;
        System.out.println("----------PROBANDO EL PERCEPTRON ---------");
        for(int i = 0; i <=2 ; i++){
            funcionActivacion += pesos[i] * entradasPrueba[i];            
            String val = funcionActivacion+"";
            BigDecimal big = new BigDecimal(val);
            big = big.setScale(2, RoundingMode.HALF_UP);    
            funcionActivacion=big.floatValue();
            System.out.println("Multiplicacion");
            System.out.println("w"+i+" * "+"x "+i);
            System.out.println(pesos[i] +"*" +entradasPrueba[i]);
        }
        System.out.println("y = "+funcionActivacion);        
         if(funcionActivacion >= 0)
            funcionActivacion = 1;
        else if(funcionActivacion < 0)
            funcionActivacion = -1;
         
        result=funcionActivacion;        
        return result;
    }
 
}