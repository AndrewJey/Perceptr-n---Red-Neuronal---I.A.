 clear all; close all; clc;
 puntosVerdesx =[1  1   1.5];
 puntosVerdesy =[1  1.2  .8];
 puntosAzulesx =[1    1   2    2.2  2.6 3  3.1];
 puntosAzulesy =[2.1  3  2.8  2.1  1.5 3  1.8];
 hold on;
 plot (puntosVerdesx, puntosVerdesy, 'og','MarkerFaceColor', 'g')
 plot (puntosVerdesx, puntosVerdesy, 'og','MarkerFaceColor', 'g')
xlabel ('P1')
xlabel ('P2')
axis([0 3.5 0 3.5])
plot([0 3], [2 0])
holf off
w1 = [-1/3;-1/2];
w= [ w1'];
PVerdes=[puntosVerdesx;puntosVerdesy];
PAzules=[puntosAzulesx;puntosAzulesy];
b=1
w1=[-b/3;-b/2;]
w=[w1']
n=w*PVerdes+b
a=hardlim(n)
a=hardlim(w*PAzules+b)
figure(2)
hold on
plot([0 3],[2 0])
xlabel('P1')
ylabel('P2')
axis([0 3.5 0 3.5])
while (b==1)
	p1=input('Introduzca la cordenada en p1 (x)');
	p1=input('Introduzca la cordenada en p2 (y)');
	p=[p1;p2];
	a=hardlim(w*p+b)
	if (a==1)
		disp('el punto es verde');
		plot(p1,p2,'og','MarkerFaceColor','b')
	else disp('el punto es azul')
		plot(p1,p2,'ob', 'MarkerFaceColor','b')
	end
end
 hold off
 	





