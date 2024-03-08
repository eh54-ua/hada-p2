using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Tablero
    {

        public EventHandler<Eventos.FinalPartidaArgs> eventoFinPartida;
        private int TamTablero { get; set; }
        
        private List<Coordenada> coordenadasDisparadas = new List<Coordenada>();
        private List<Coordenada> coordenadasTocadas = new List<Coordenada>();
        private List<Barco> barcos = new List<Barco>();
        private List<Barco> barcosEliminados = new List<Barco>();
        private Dictionary<Coordenada, string> casillasTablero = new Dictionary<Coordenada, string>();

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            this.TamTablero = tamTablero;
            this.barcos = barcos;

            foreach (Barco b in barcos)
            {
                b.eventoTocado += cuandoEventoTocado;
                b.eventoHundido += cuandoEventoHundido;
            }
            
            inicializarCasillasTablero();
        }

        private void inicializarCasillasTablero()
        {
            //Añado todas las casillas dependiendo el tamaño del tablero.
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    casillasTablero.Add(new Coordenada(i,j), "AGUA");
                }
            }

            //Encuentro las casillas donde hay barco y le cambio el value por el nombre del barco.
            foreach (Barco barco in barcos)
            {
                foreach (Coordenada coord in barco.CoordenadasBarco.Keys)
                {
                    casillasTablero[coord] = barco.Nombre;
                }
            }

        }

        public void Disparar(Coordenada c){
            if(c.Fila < 0 || c.Fila > TamTablero-1 || c.Columna < 0 || c.Columna > TamTablero-1)
            {
                Console.WriteLine("La coordenada ({0}, {1}) está fuera de las dimensiones del tablero", c.Fila, c.Columna);
            }
            else
            {
                coordenadasDisparadas.Add(c);
                //Recorro la lista de barcos, creo una lista auxiliar para almacenar las coordenadas de los barcos.
                foreach(Barco b in barcos)
                {
                    List<Coordenada> claves = new List<Coordenada>(b.CoordenadasBarco.Keys);
                    //Recorro estas coordenadas y las comparo con la coordenada de disparo. Tengo que crear la lista aux porque si no da error.
                    //No se puede modificar CoordenadasBarco mientras la clase barco se esta ejecutando.
                    foreach(Coordenada coord in claves)
                    {
                        if(coord.Fila == c.Fila && coord.Columna == c.Columna)
                        {
                            b.Disparo(c);
                            casillasTablero[c] = b.CoordenadasBarco[c];
                        }
                    }
                }
            }
        }

        public string DibujarTablero()
        {
            string tablero = "";
            int mostrados = 0;

            for(int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    Coordenada coord = new Coordenada(i,j);
                    tablero += "[" + casillasTablero[coord] + "]";
                    mostrados++;
                    if (mostrados % TamTablero == 0)
                    {
                        tablero += "\n";
                    }
                }
            }

            return tablero;
        }

        public override string ToString()
        {
            string resultado = "";
            foreach(Barco b in barcos)
            {
                resultado += b.ToString();
                resultado += "\n";
            }

            resultado += "\nCoordenadas disparadas: ";
            foreach(Coordenada c in coordenadasDisparadas)
            {
                resultado += "(" + c.Fila + "," + c.Columna + ") ";
            }

            resultado += "\nCoordenadas tocadas: ";
            foreach(Coordenada c in coordenadasTocadas)
            {
                resultado += "(" + c.Fila + "," + c.Columna + ") ";
            }

            resultado += "\n\n\n";
            resultado += "CASILLAS TABLERO\n";
            resultado += "-------\n";

            resultado += DibujarTablero() + "\n";

            return resultado;
        }

        private void cuandoEventoTocado(object sender, Eventos.TocadoArgs args)
        {
            //Actualizar las casillas del tablero e imprimir
            foreach(KeyValuePair<Coordenada, string> b in casillasTablero)
            {
                if(b.Key.Fila == args.coordenadaImpacto.Fila && b.Key.Columna == args.coordenadaImpacto.Columna)
                {
                    coordenadasTocadas.Add(b.Key);
                }
            }
            Console.WriteLine("TABLERO: Barco {0} tocado en Coordenada: [{1}, {2}]", args.nombre, args.coordenadaImpacto.Fila, args.coordenadaImpacto.Columna);
        }

        private void cuandoEventoHundido(object sender, Eventos.HundidoArgs args)
        {
            Console.WriteLine("Tablero: Barco {0} hundido!!", args.nombre);
            barcosEliminados.Add(barcos.Find(barco => barco.Nombre == args.nombre));

            if (barcos.Count() == barcosEliminados.Count())
            {
                eventoFinPartida(this, new Eventos.FinalPartidaArgs(true));
            }
        }
    }
}
