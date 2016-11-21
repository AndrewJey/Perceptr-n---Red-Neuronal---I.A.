clear all; close all; clc;
puntosVerdesx={1 1  1.5};
puntosVerdesy={1 1.2  .8};
puntosAzulesx={1    1  2   2.2  2.6 3  3.1};
puntosAzulesy={2.1  3  2.8 2.1  1.5 3  1.8};
hold on;
plot(puntosVerdesx, puntosVerdesy, 'og', 'MarkerFaceColor', 'g')
plot(puntosAzulesx, puntosAzulesy, 'ob', 'MarkerFaceColor', 'b')
xlabel('P1')
ylabel('P2')
axis([0 3.5 0 .35])
plot([0 3],[2 0])
hold off
PVerdes=[puntosVerdesx;puntosVerdesy;];
PAzules=[puntosAzulesx;puntosAzulesy;];
b=1;
w1=[-b/3;-b/2]
W=[w1']
n=W*PVerdes+b
a=hardlim(n)
a=hardlim(W*PAzules+b)
figure(2)
hold on
plot([0 3],[2 0])
xlabel('P1')
ylabel('P2')
axis([0 3.5 0 3.5])
while (b==1)
	p1=input('Ingrese la coordenada en P1(x)');
	p2=input('Ingrese la coordenada en P2(y)');
	p=[p1;p2];
	a=hardlim(W*P+b)
	if (a==1)
		disp('¡El punto es verde!')
		plot(p1,p2,'og','MarkerFaceColor','g')
	else disp('¡El punto es azul!');
		plot(p1,p2,'ob','MarkerFaceColor','b')
	end
end
hold off			