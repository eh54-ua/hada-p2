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
            bool esNumero = false;
            int tam = 0;
            List<Barco> listBarcos = new List<Barco>();

            // Barcos de ejemplo
            listBarcos.Add(new Barco("THOR", 1, 'h', new Coordenada(0, 0)));
            listBarcos.Add(new Barco("LOKI", 2, 'v', new Coordenada(1, 2)));
            listBarcos.Add(new Barco("MAYA", 3, 'h', new Coordenada(3, 1)));

            //Compruebo si el tamaño es correcto
            do
            {
                Console.Write("Introduce el tamaño del tablero (entre 4 y 9): ");
                esNumero = int.TryParse(Console.ReadLine(), out tam);
                if(!esNumero)
                {
                    Console.WriteLine("No se ha introducido un número.");
                }
                else
                {
                    if (tam < 4 || tam > 9)
                    {
                        esNumero = false;
                        Console.WriteLine("Tamaño de tablero incorrecto.");
                    }
                }
            }
            while (!esNumero);

            Tablero tab = new Tablero(tam, listBarcos);

            bool coordenadaValida = false;
            string cadena;
            tab.eventoFinPartida += cuandoEventoFinPartida;

            Console.WriteLine(tab.ToString());

            while (!finPartida)
            {
                 // Verificar que se introduce una coordenada en formato válido
                 do
                 {
                     Console.Write("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir): \n");
                     cadena = Console.ReadLine();
                     coordenadaValida = verificarCoordenada(cadena);
                 }
                 while(!coordenadaValida);

                 // Una vez se tiene una coordenada válida extraer los valores
                 string[] partes = cadena.Split(',');

                 // Disparar y mostrar el estado del juego
                 tab.Disparar(new Coordenada(partes[0], partes[1]));
                Console.Write(tab.ToString());

                if (finPartida)
                {
                    Console.WriteLine("..................................................................\nTODOS LOS BARCOS SE HAN HUNDIDO.\nGAME OVER");
                }

            }
        }

        private void cuandoEventoFinPartida(object sender, Eventos.FinalPartidaArgs e)
        {
            this.finPartida = e.fin;
        }

        private bool verificarCoordenada(string cadena)
        {
            // Si se introduce 'S' o 's' entonces salir
            if (cadena.Length == 1 && (cadena[0] == 'S' || cadena[0] == 's'))
            {
                System.Environment.Exit(0);
            }

            bool coordenadaValida = true;

            if(cadena.Length != 3)
            {
                coordenadaValida = false;
            }
            // Si no es un número o una coma no es válido
            else if ((cadena[0] < '0' || cadena[0] > '9') || (cadena[2] < '0' || cadena[2] > '9'))
            {
                coordenadaValida = false;
            }
            else if (cadena[1] != ',')
            {
                coordenadaValida = false;
            }

            if (!coordenadaValida)
            {
                Console.WriteLine("Se ha introducido una coordenada no válida.");
            }

            return coordenadaValida;
        }
    }
}