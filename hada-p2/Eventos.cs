using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Eventos
    {
        public class TocadoArgs : EventArgs
        {
            public string nombre { get; set; }
            public string etiqueta { get; set; }
            public Coordenada coordenadaImpacto { get; set; }

            public TocadoArgs(string nombre, string etiqueta, Coordenada coordenadaImpacto)
            {
                this.nombre = nombre;
                this.etiqueta = etiqueta;
                this.coordenadaImpacto = coordenadaImpacto;
            }
        }

        public class HundidoArgs : EventArgs
        {
            public string nombre;

            public HundidoArgs(string nombre)
            {
                this.nombre = nombre;
            }
        }

        public class FinalPartidaArgs : EventArgs
        {
            public bool fin;
            public FinalPartidaArgs(bool fin)
            {
                this.fin = fin;
            }
        }
    }
}
