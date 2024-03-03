using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Game
    {
        private bool finPartida {get; set;}

        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        private void gameLoop()
        {
            Tablero tab = new Tablero();

            // Barcos de ejemplo
            tab.barcos.Add(new Barco("THOR", 1, 'h', new Coordenada(0, 0)));
            tab.barcos.Add(new Barco("LOKI", 2, 'v', new Coordenada(1, 2)));
            tab.barcos.Add(new Barco("MAYA", 3, 'h', new Coordenada(3, 1)));

            try
            {
                esNumero = false;
                do
                {
                    int resultado;
                    Console.Write("Introduce el tamaño del tablero (entre 4 y 9): ");
                    bool esNumero = int.TryParse(Console.ReadLine(), out resultado);
                    if(!esNumero)
                    {
                        Console.WriteLine("No se ha introducido un número.");
                    }
                    else
                    {
                        tab.TamTablero = resultado;
                    }
                }
                while (!esNumero);
            }
            catch
            {
                Console.WriteLine("Tamaño del tablero no válido, saliendo del juego...");
                System.Environment.Exit(0);
            }
            finally
            {
                tab.inicializarCasillasTablero();
            }

            bool coordenadaValida = false;
            int posComa;
            string cadena;
            string coordenada[2];

            while(!finPartida)
            {
                // Verificar que se introduce una coordenada en formato válido
                do
                {
                    Console.Write("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir): ");
                    cadena = Console.ReadLine();
                    coordenadaValida = Hada.verificarCoordenada(cadena);
                }
                while(!coordenadaValida);

                // Una vez se tiene una coordenada válida extraer los valores
                for(int i = 0; i < cadena.length(); i++)
                {
                    if(cadena[i] == ',') posComa = i;
                }
                coordenada[0] = cadena.Substring(0, posComa);
                coordenada[1] = cadena.Substring(posComa+1, cadena.length() - posComa);

                // Disparar y mostrar el estado del juego
                tab.Disparar(new Coordenada(coordenada[0], coordenada[1]));
                Console.Write(tab.ToString());
            }
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("La partida se ha acabado.");
            System.Environment.Exit(0);
        }
    }

    bool verificarCoordenada(string cadena)
    {
        // Si se introduce 'S' o 's' entonces salir
        if(cadena.length() == 1 && cadena[0] == 'S' || cadena[0] == 's')
        {
            System.Environment.Exit(0);
        }

        int comas = 0;
        bool coordenadaValida = true;

        for(int i=0; i<cadena.length() && coordenadaValida; i++)
        {            
            // Si no es un número o una coma no es válido
            if(cadena[i] < '0' || cadena[i] > '9' && cadena[i] != ',')
            {
                coordenadaValida = false;
            }

            if(cadena[i] == ',')
            {
                comas++;
            }

            if(comas > 1)
            {
                coordenadaValida = false;
            }
        }

        if(!coordenadaValida)
        {
            Console.WriteLine("Se ha introducido una coordenada no válida.");   
        }

        return coordenadaValida;
    }
}