using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Tablero
    {

        public EventHandler<EventArgs> eventoFinPartida;
        private int _TamTablero;
        public int TamTablero { 
            get
            {
                return _TamTablero;
            }
            set
            {
                if(value < 4 || value > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El rango válido es entre 4 y 9.");
                }
                else
                {
                    _TamTablero = value;
                }
            } 
        }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            this._TamTablero = tamTablero;
            this.barcos = barcos;

            foreach (Barco b in barcos)
            {
                b.eventoTocado += cuandoEventoTocado;
                b.eventoHundido += cuandoEventoHundido;
            }
            eventoFinPartida += Game.cuandoEventoFinPartida;

            inicializarCasillasTablero();
        }

        private void inicializarCasillasTablero()
        {
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    foreach (KeyValuePair<Coordenada, string> b in casillasTablero)
                    {
                        if (i == b.Key.Columna && j == b.Key.Fila)
                        {
                            casillasTablero.Add(b.Key, b.Value);
                        }
                        else
                        {
                            casillasTablero.Add(b.Key, "AGUA");
                        }
                    }
                }
            }
        }

        public void Disparar(Coordenada c){
            if(c.Fila < 0 || c.Fila > TamTablero-1 || c.Columna < 0 || c.Columna > TamTablero-1)
            {
                Console.WriteLine("La coordenada ({1}, {2}) está fuera de las dimensiones del tablero", c.Fila, c.Columna);
            }
            else
            {
                coordenadasDisparadas.Add(c);
                foreach(Barco b in barcos)
                {
                    foreach(KeyValuePair<Coordenada, string> kvp in b.CoordenadasBarco)
                    {
                        if(kvp.CoordenadasBarco.Fila == c.Fila && kvp.CoordenadaBarco.Columna == c.Columna)
                        {
                            b.cuandoEventoTocado(b, new TocadoArgs(b.nombre, kvp.Key, "_T"));
                        }
                    }
                }
            }
        }

        public string DibujarTablero()
        {
            string tablero;
            int mostrados = 0;

            foreach(KeyValuePair<Coordenada, string> kvp in casillasTablero)
            {
                tablero += "[" + kvp.Value + "]";
                mostrados++;
                if(mostrados % TamTablero == 0)
                {
                    tablero += "\n";
                }
            }

            return tablero;
        }

        public override string ToString()
        {
            string resultado;
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
                if(b.Value.Fila == args.coordenadaImpacto.Fila && b.Value.Columna == args.coordenadaImpacto.Columna)
                {
                    coordenadasTocadas.Add(b.Key, b.Value);
                }
            }
            Console.WriteLine("TABLERO: Barco {1} tocado en Coordenada: [{2}, {3}]", args.nombre, args.coordenadaImpacto.Fila, args.coordenadaImpacto.Columna);
        }

        private void cuandoEventoHundido(object sender, Eventos.HundidoArgs args)
        {
            Console.WriteLine("Tablero: Barco {1} hundido!!", args.Nombre);

            if (barcos.Count() == barcosEliminados.Count())
            {
                Game.cuandoEventoFinPartida(this, EventArgs.Empty);
            }
        }
    }
}
