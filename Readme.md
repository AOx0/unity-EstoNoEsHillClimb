# dinamITAM

Project for the 2020 ITAM's **dinamITAM** programming / design challenge.

Para correr el proyecto acceder a https://aox0.github.io/unity-EstoNoEsHillClimb/

**Cuando abran la página para jugarla pareciera que no está cargando, solo denle tiempo. No se ven los botones pero todo sigue ahí :v**


### Indice

1.  [El proyecto (código)](https://github.com/AOx0/unity-EstoNoEsHillClimb#el-proyecto)

2.  [Features (juego)](https://github.com/AOx0/unity-EstoNoEsHillClimb#features)

# El proyecto

El código está en su mayoría en inglés, al igual que el juego. Todo el proyecto se encuentra en la carpeta `Project`, si desean revisarlo solo clonen este repositorio y agreguen dicha carpeta a Unity Hub.

**Carpeta con el código**:  Project / Assets / [**Scripts**](https://github.com/AOx0/unity-EstoNoEsHillClimb/tree/master/Project/Assets/Scripts) /



La estructura del código es simple. Se cuenta con un objeto principal encargado de realizar todos los procesos que requiere el juego llamado `GameHandler` el cual funciona de manera casi independiendte. Además del 'Handler' principal se encuentran varios scripts que se encargan de escuchar los atributos estáticos de la clase `GameHandler` y actualizar cierto elemento en la vista del usuario



![img](https://github.com/AOx0/unity-EstoNoEsHillClimb/blob/master/res/imagen0.png)






# Features

-   Menú de Inicio
-   Musica
    -   Posibilidad de habilitar / des habilitar la música
-   Función de "spawn" de coches dependiendo del modo
-   Función de actualización del velocimetro de cada jugador
-   Función de actualización del indicador de posición en el recorrido
-   Función de detección de tiempo en el aire para habilitar la rotación del coche para realizar acrobacias
-   Stages  de juego para el control de los elementos
    -   Menu
    -   Seleccion de Coche
    -   Seleccion multijugador de Coche
    -   Seleccion de nivel
    -   Pre juego (cargado)
    -   Juego (a la escucha del Keyboard)
    -   Postjuego (resultado y reinicio)
-   Niveles del juego
    -   5 niveles
    -   Tutorial (1er Nivel)
    -   Guardado de mejor tiempo en solitario
    -   Indicador de nivel completado
-   Elección de coche
    -   Distintos tipos de coches
        -   Normales
        -   2x Speed (para acrobacias)
    -   Conductas distintas dependiendo del tipo de coche
        -   En velocidad máxima
        -   En aceleración máxima y velocidad de aceleración
-   Modo de juego solitario
    -   Ayuda sugiriendo reiniciar el nivel si se detecta que el coche está con una rotación de casi -1 o 1 (volteado)
    -   Seguimiento al jugador por parte de la cámara, fondo y UI.
    -   Indicador de velocidad dinámico
    -   "Minimapa": Indicador de posición en el track
    -   Reaparición
    -   Medición de tiempo
    -   Mensaje de completado
        -   Si fue o no mejor tiempo
        -   El numero del nivel completado
        -   El tiempo tomado para realizarlo
-   Modo de juego multijugador
    -   Elección por jugador del coche deseado
    -   Elección de nivel
    -   Reaparición para cada jugador
    -   Seguimiento a cada jugador de
        -   Camara individual
        -   UI individual
        -   Tracker (Indicador de posición en el recorrido) de los dos usuarios
        -   Velocimetro
    -   Velocimetro individual para cada jugador
    -   Keys de acción distintas para cada jugador
    -   Tracker de posición disponible para ambos jugadores
    -   Tiempo total de ambos jugadores
    -   Menú final con el jugador ganador y los tiempos
-   
-   Función de aceleración personalizada:
    -   Si el usuario presiona un Key de acelerar el coche acelera
    -   Si el usuario presiona un Key de reversa el coche
        -   Si está acelerando o cuenta con velocidad la reduce un poco
        -   Si no cuenta con velocidad va de reversa
    -   Si el usuario no presiona nada se acelera/desacelera automáticamente poco a poco hasta llegar a 0
    -   Si el usuario presiona un key de frenado el coche cambia a 0 de manera agresiva sin importar si va de reversa o acelerando
-   Imagenes con una paleta de colores bastante oscura.

![img](https://github.com/AOx0/unity-EstoNoEsHillClimb/blob/master/res/gif1.gif)
