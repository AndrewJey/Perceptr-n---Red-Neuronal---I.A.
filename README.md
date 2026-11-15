# Perceptr-n---Red-Neuronal---I.A

<!-- local-development:start -->
## Desarrollo local

Consulta [RUNNING.md](RUNNING.md) para ubicar cada componente, abrirlo en su IDE, compilarlo y conocer las comprobaciones realizadas y los requisitos pendientes.
<!-- local-development:end -->

<!-- build-repairs-20261115:start -->
## Reparación y comprobación — 2026-11-15

- Se sustituyó PowerPacks por un lienzo WinForms que dibuja líneas y óvalos en el fondo del formulario. Conserva las posiciones del diagrama y permite utilizar etiquetas, tabla y botón de cálculo sin una ventana opaca superpuesta.

Comprobado:

- Compilan los seis proyectos .NET y los cuatro proyectos Java/Ant.
- Se construyeron los seis formularios .NET. Las tres variantes modificadas se abrieron fuera de pantalla y se verificó su imagen con diagrama, etiquetas, tabla y botón visibles.

Comandos:

```powershell
Abre la solución de la variante deseada en Visual Studio 2022 y compila.
Para las variantes Java usa .\build-local.ps1.
```

Alcance pendiente:

- No se validó el entrenamiento completo con la base de datos histórica de las variantes que dependen de ella.

<!-- build-repairs-20261115:end -->
