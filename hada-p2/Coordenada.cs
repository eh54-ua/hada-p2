using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Coordenada
    {
        public int Fila { get; set; }
        public int Columna { get; set; }

        public Coordenada()
        {
            this.Fila = 0;
            this.Columna = 0;
        }
        public Coordenada(int fila, int columna)
        {
            this.Fila = fila;
            this.Columna = columna;
        }
        public Coordenada(string fila, string columna)
        {
            this.Fila = int.Parse(fila);
            this.Columna = int.Parse(columna);
        }
        public Coordenada(Coordenada c)
        {
            this.Fila = c.Fila;
            this.Columna = c.Columna;
        }

    }
}
