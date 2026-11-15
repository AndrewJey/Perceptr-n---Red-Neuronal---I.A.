

<!-- local-development:start -->
# Desarrollo local

Puedes abrir este repositorio en VS Code, Antigravity o Sublime Text. El compilador y los servicios necesarios dependen del componente indicado abajo.

## Java / NetBeans

En NetBeans 25, abre la carpeta que contiene `build.xml` como proyecto Ant. Selecciona un JDK igual o posterior al nivel de fuente del proyecto; las compilaciones de esta revisión usan JDK 24.0.2. El asistente busca NetBeans y el JDK y mantiene la configuración de usuario en `.local-build/`.

```powershell
.uild-local.ps1
.\build-local.ps1 -Project 'Casi Pacman - JAVA/juegos/build.xml' -Target run
```

Si hay varios proyectos, elige el deseado con `-Project`. Puedes indicar `-JavaHome` y `-NetBeansHome`. `-Target test` ejecuta las pruebas que declare el proyecto; un objetivo de pruebas vacío no acredita cobertura.

## Visual Studio 2022 / .NET Framework

Instala la carga de trabajo de escritorio .NET y el paquete de destino indicado en la tabla. Los proyectos ASP.NET también requieren la carga web e IIS Express. Abre la solución `.sln` correspondiente y selecciona el proyecto de aplicación para iniciar.

```powershell
# Ejecutar desde Developer PowerShell de Visual Studio 2022
$solutionDir = (Resolve-Path 'PerceptronNeuronal - C#/PerceptronNeuronal').Path + '\'
MSBuild.exe 'PerceptronNeuronal - C#/PerceptronNeuronal/PerceptronNeuronal/PerceptronNeuronal.csproj' /restore /p:RestorePackagesConfig=true /p:Configuration=Debug /p:DeployOnBuild=false "/p:SolutionDir=$solutionDir"
```

## Componentes y validación

Inventario y compilaciones locales del 2026-11-15. Una compilación correcta no acredita todavía interacción, compatibilidad de base de datos, instalador ni despliegue.

| Componente | Tipo / destino | Validación |
| --- | --- | --- |
| [Casi Pacman - JAVA/juegos/build.xml](<Casi Pacman - JAVA/juegos/build.xml>) | netbeans-ant / 1.8 | Compila; prueba funcional pendiente |
| [PerceptronNeuronal - C#/PerceptronNeuronal/PerceptronNeuronal.sln](<PerceptronNeuronal - C#/PerceptronNeuronal/PerceptronNeuronal.sln>) | solution | Sin ejecutar |
| [PerceptronNeuronal - C#/PerceptronNeuronal/PerceptronNeuronal/PerceptronNeuronal.csproj](<PerceptronNeuronal - C#/PerceptronNeuronal/PerceptronNeuronal/PerceptronNeuronal.csproj>) | dotnet / v4.8.1 | Compila; prueba funcional pendiente |
| [PerceptronSimple2 - JAVA/PerceptronSimple2/build.xml](<PerceptronSimple2 - JAVA/PerceptronSimple2/build.xml>) | netbeans-ant / 1.8 | Compila; prueba funcional pendiente |
| [PerceptronTutorial - C#/PerceptronTutorial/PerceptronTutorial.sln](<PerceptronTutorial - C#/PerceptronTutorial/PerceptronTutorial.sln>) | solution | Sin ejecutar |
| [PerceptronTutorial - C#/PerceptronTutorial/PerceptronTutorial/PerceptronTutorial.csproj](<PerceptronTutorial - C#/PerceptronTutorial/PerceptronTutorial/PerceptronTutorial.csproj>) | dotnet / v4.8.1 | Compila; prueba funcional pendiente |
| [Perceptrones - C#/Perceptron/Perceptron.sln](<Perceptrones - C#/Perceptron/Perceptron.sln>) | solution | Sin ejecutar |
| [Perceptrones - C#/Perceptron/Perceptron/Perceptron.csproj](<Perceptrones - C#/Perceptron/Perceptron/Perceptron.csproj>) | dotnet / v4.8.1 | Compila; prueba funcional pendiente |
| [Perceptrones - JAVA/Perceptron/build.xml](<Perceptrones - JAVA/Perceptron/build.xml>) | netbeans-ant / 1.8 | Compila; prueba funcional pendiente |
| [Perceptrones - JAVA/perceptron2/Perceptron/Perceptron.sln](<Perceptrones - JAVA/perceptron2/Perceptron/Perceptron.sln>) | solution | Sin ejecutar |
| [Perceptrones - JAVA/perceptron2/Perceptron/Perceptron/Perceptron.csproj](<Perceptrones - JAVA/perceptron2/Perceptron/Perceptron/Perceptron.csproj>) | dotnet / v4.8.1 | Bloqueado: CS0234 |
| [Perceptrones - JAVA/perceptron2/build.xml](<Perceptrones - JAVA/perceptron2/build.xml>) | netbeans-ant / 1.8 | Compila; prueba funcional pendiente |
| [Perceptrón - VB.NeT (con BD)/tuto 2 - Andrés Matta/Perceptron/Perceptron.sln](<Perceptrón - VB.NeT (con BD)/tuto 2 - Andrés Matta/Perceptron/Perceptron.sln>) | solution | Sin ejecutar |
| [Perceptrón - VB.NeT (con BD)/tuto 2 - Andrés Matta/Perceptron/Perceptron/Perceptron.csproj](<Perceptrón - VB.NeT (con BD)/tuto 2 - Andrés Matta/Perceptron/Perceptron/Perceptron.csproj>) | dotnet / v4.8.1 | Bloqueado: CS0234 |
| [perceptron - VB.NeT/perceptron/Perceptron/Perceptron.sln](<perceptron - VB.NeT/perceptron/Perceptron/Perceptron.sln>) | solution | Sin ejecutar |
| [perceptron - VB.NeT/perceptron/Perceptron/Perceptron/Perceptron.csproj](<perceptron - VB.NeT/perceptron/Perceptron/Perceptron/Perceptron.csproj>) | dotnet / v4.8.1 | Bloqueado: CS0426 |

## Archivos ignorados

`.gitignore` excluye credenciales locales, estado del editor y salidas de las herramientas detectadas. No deja de versionar archivos que ya estaban en Git. Los manifiestos, archivos de bloqueo, recursos fuente y scripts de esquema forman parte del código.
<!-- local-development:end -->
