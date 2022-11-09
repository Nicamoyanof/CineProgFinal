using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Peliculas
    {

        public Peliculas(int idPelicula, string nombre, EdadesPermitidas edadMinima, GeneroPelicula genero, string descripcion, string nombrePoster)
        {
            IdPelicula = idPelicula;
            Nombre = nombre;
            EdadMinima = edadMinima;
            Genero = genero;
            Descripcion = descripcion;
            NombrePoster = nombrePoster;
        }

        public Peliculas()
        {
            Nombre = "sin nombre";
            EdadMinima = new EdadesPermitidas();
            Genero = new GeneroPelicula();
            Descripcion = "sin descripcion";
        }
        public int IdPelicula { get; set; }
        public string Nombre { get; set; }
        public EdadesPermitidas EdadMinima { get; set; }
        public GeneroPelicula Genero { get; set; }
        public string Descripcion { get; set; }

        public string NombrePoster { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
