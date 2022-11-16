using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class GeneroPelicula
    {

        public GeneroPelicula(int idGenero, string nombre, string descripcion)
        {
            IdGeneroPelicula = idGenero;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public GeneroPelicula()
        {
            IdGeneroPelicula = 0;
            Nombre = "sin nombre";
            Descripcion = "sin descripcion";
        }

        public int IdGeneroPelicula { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
