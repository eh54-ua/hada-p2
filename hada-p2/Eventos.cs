using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Eventos
    {
        //public event EventHandler<HundidoArgs> eventoHundido;

        public class TocadoArgs : EventArgs
        {
            public string Nombre { get; set; }
            public string Etiqueta { get; set; }
            public Coordenada CoordenadaImpacto { get; set; }

            public TocadoArgs(string nombre, string etiqueta, Coordenada coordenadaImpacto)
            {
                this.Nombre = nombre;
                this.Etiqueta = etiqueta;
                this.CoordenadaImpacto = coordenadaImpacto;
            }
        }

        public class HundidoArgs : EventArgs
        {
            public string Nombre { get; set; }

            public HundidoArgs(string nombre)
            {
                this.Nombre = nombre;
            }
        }
    }
}
