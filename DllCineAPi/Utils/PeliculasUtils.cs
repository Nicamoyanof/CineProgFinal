

namespace DllCineApi.Utils
{
    public class PeliculasUtils
    {

        public PeliculasUtils(int idPelicula, string nombre, int edadMinima, int genero, string descripcion, string nombrePoster)
        {
            IdPelicula = idPelicula;
            Nombre = nombre;
            EdadMinima = edadMinima;
            Genero = genero;
            Descripcion = descripcion;
            NombrePoster = nombrePoster;
        }

        public PeliculasUtils()
        {
            Nombre = "sin nombre";
            EdadMinima = 0;
            Genero =0;
            Descripcion = "sin descripcion";
            NombrePoster = "sin nombre";
        }
        public int IdPelicula { get; set; }
        public string Nombre { get; set; }
        public int EdadMinima { get; set; }
        public int Genero { get; set; }
        public string Descripcion { get; set; }

        public string NombrePoster { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
