using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Program
    {
        static void Main(string[] args)
        {
            Coordenada coord = new Coordenada(1,1);
            Barco barco = new Barco("Daniel", 3, 'h', coord);
            barco.eventoTocado += Eventos.;
            barco.Disparo(coord);

        }
    }
}
