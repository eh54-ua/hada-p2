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

        // Constructor por defecto
        public Coordenada()
        {
            this.Fila = 0;
            this.Columna = 0;
        }
        // Constructor con dos números
        public Coordenada(int fila, int columna)
        {
            //Cambiar para solucionar la excepcion de coordenadas negativas.
            this.Fila = fila;
            this.Columna = columna;
        }
        // Constructor con dos números en formato cadena
        public Coordenada(string fila, string columna)
        {
            int numero;
            if(int.TryParse(fila, out numero))
            {
                this.Fila = numero;
            }
            else
            {
                //TODO: A poder ser cambiar esto por una excepcion
                Console.WriteLine("Argumento invalido de fila.");
            }

            if(int.TryParse(columna, out numero))
            {
                this.Columna = numero;
            }
            else
            {
                //TODO: A poder ser cambiar esto por una excepcion
                Console.WriteLine("Argumento invalido de columna.");
            }
        }
        // Constructor de copia
        public Coordenada(Coordenada c)
        {
            this.Fila = c.Fila;
            this.Columna = c.Columna;
        }
        
        // Muestra la coordenada por consola con el formato (fila,columna)
        public override string ToString()
        {
            return "(" + Fila + "," + Columna + ")";
        }
        // Devuelve un código hash para poder hacer comparaciones en un diccionario
        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }
        // Comparación con un objeto tipo Object
        public override bool Equals(object obj)
        {
            if (obj != null && (obj is Coordenada))
                return this.Fila == ((Coordenada)obj).Fila && this.Columna == ((Coordenada)obj).Columna;
            else
                return false;
        }
        // Comparación con un objeto tipo Coordenada
        public bool Equals(Coordenada c)
        {
            return this.Fila == c.Fila && this.Columna == c.Columna;
        }
    }
}
