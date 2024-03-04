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

            try
            {
                do
                {
                    Console.Write("Introduce el tamaño del tablero (entre 4 y 9): ");
                    esNumero = int.TryParse(Console.ReadLine(), out tam);
                    if(!esNumero)
                    {
                        Console.WriteLine("No se ha introducido un número.");
                    }
                }
                while (!esNumero);
            }
            catch
            {
                Console.WriteLine("Tamaño del tablero no válido, saliendo del juego...");
                System.Environment.Exit(0);
            }

            // Barcos de ejemplo
            listBarcos.Add(new Barco("THOR", 1, 'h', new Coordenada(0, 0)));
            listBarcos.Add(new Barco("LOKI", 2, 'v', new Coordenada(1, 2)));
            listBarcos.Add(new Barco("MAYA", 3, 'h', new Coordenada(2, 1)));
            Tablero tab = new Tablero(tam, listBarcos);

            bool coordenadaValida = false;
            int posComa = 0;
            string cadena;
            string coordenada1 = "";
            string coordenada2 = "";

            while(!finPartida)
            {
                // Verificar que se introduce una coordenada en formato válido
                do
                {
                    Console.Write("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir): ");
                    cadena = Console.ReadLine();
                    coordenadaValida = verificarCoordenada(cadena);
                }
                while(!coordenadaValida);

                // Una vez se tiene una coordenada válida extraer los valores
                for(int i = 0; i < cadena.Length; i++)
                {
                    if(cadena[i] == ',') posComa = i;
                }
                coordenada1 = cadena.Substring(0, posComa);
                coordenada2 = cadena.Substring(posComa+1, cadena.Length - posComa);

                // Disparar y mostrar el estado del juego
                tab.Disparar(new Coordenada(coordenada1, coordenada2));
                Console.Write(tab.ToString());
            }
        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("La partida se ha acabado.");
            System.Environment.Exit(0);
        }

        private bool verificarCoordenada(string cadena)
        {
            // Si se introduce 'S' o 's' entonces salir
            if (cadena.Length == 1 && cadena[0] == 'S' || cadena[0] == 's')
            {
                System.Environment.Exit(0);
            }

            int comas = 0;
            bool coordenadaValida = true;

            for (int i = 0; i < cadena.Length && coordenadaValida; i++)
            {
                // Si no es un número o una coma no es válido
                if (cadena[i] < '0' || cadena[i] > '9' && cadena[i] != ',')
                {
                    coordenadaValida = false;
                }

                if (cadena[i] == ',')
                {
                    comas++;
                }

                if (comas > 1)
                {
                    coordenadaValida = false;
                }
            }

            if (!coordenadaValida)
            {
                Console.WriteLine("Se ha introducido una coordenada no válida.");
            }

            return coordenadaValida;
        }

    }
}