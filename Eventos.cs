using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Eventos
    {

        public event EventHandler<TocadoArgs> eventoTocado;
        //public event EventHandler<HundidoArgs> eventoHundido;

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

        public void manejadorEventoTocado(object sender, TocadoArgs e)
        {
            Console.WriteLine("Evento tocado");
        }

    }
}