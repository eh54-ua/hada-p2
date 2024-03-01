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
                    throw new ArgumentOutOfRangeException(nameof(value), "El rango valido es entre 4 y 9.");
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
            }

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
                            casillasTablero.Add(b.Key, "AUA");
                        }
                    }
                }
            }
        }

        private void cuandoEventoTocado(object sender, Eventos.TocadoArgs args)
        {
        //Actualizar las casillas del tablero e imprimir
        //TABLERO: Barco [NOMBRE_BARCO] tocado en Coordenada:[(FILA, COLUMNA)]
        }

        private void cuandoEventoHundido(object sender, Eventos.HundidoArgs args)
        {
            Console.WriteLine("Tablero: Barco {1} hundido!!", args.Nombre);

            if (barcos.Count() == barcosEliminados.Count())
            {
                //evento fin partida
            }
        }
    }
}
