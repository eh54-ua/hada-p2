using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Barco
    {

        public Dictionary<Coordenada, String> CoordenadasBarco { get; private set; } = new Dictionary<Coordenada, string>();
        public Eventos evento { get; private set; }

        public string Nombre;
        public int NumDanyos;

        //Constructor de la clase barco
        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {   
            for(int i = 0; i < longitud; i++)
            {
                //Si la orientacion es horizontal, se aumenta en una unidad la fila.
                if (orientacion == 'h')
                {
                    Coordenada coord = new Coordenada(coordenadaInicio.Fila + i,coordenadaInicio.Columna);
                    CoordenadasBarco.Add(coord, nombre);
                }//Si la horientacion es vertical se aumenta en una unidad la columna.
                else if (orientacion == 'v')
                {
                    Coordenada coord = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + 1);
                    CoordenadasBarco.Add(coord, nombre);
                }
            }

            this.Nombre = nombre;
            this.NumDanyos = 0;
        }

        //Modifica el estado de una coordenada pasada por parámetro a tocado, siempre y cuando exista esta coordenada.
        public void Disparo(Coordenada c)
        {
            string valor;

            //Intento obtener el valor asociado a la cordenada c.
            if(CoordenadasBarco.TryGetValue(c, out valor))
            {
                //Compruebo que el nombre de la casilla no tenga ya el _T.
                if (!valor.EndsWith("_T"))
                {
                    //Si no lo tiene añado el _T.
                    CoordenadasBarco[c] += "_T";
                    //Aumento en 1 el número de daños.
                    this.NumDanyos++;
                    //Evento tocado
                }
            }

            if(this.hundido())
            {
                //Evento hundido
            }
        }

        public bool hundido()
        {
            //Recorro todo el diccionario almacenando en nom el nombre de CoordenadaBarco asociado a la coordenada.
            foreach (string nom in CoordenadasBarco.Values)
            {
                //Si alguna de las coordenadas tiene asociado un nombre igual al nombre del barco, significa que no esta hundido.
                if (nom == this.Nombre)
                {
                    return false;
                }
            }

            //Si no hay ninguno que tenga asociado el nombre del barco, significa que esta hundido.
            return true;
        }

        public override string ToString()
        {
            string aux = $"[{Nombre}] - DAÑOS: [{NumDanyos}] - HUNDIDO: [{this.hundido()}] - COORDENADAS: ";
            foreach (KeyValuePair<Coordenada, string> kvp in CoordenadasBarco)
            {
                aux += "[" + kvp.Key + " :" + kvp.Value + "] ";
            }
            return aux;
        }
    }
}
