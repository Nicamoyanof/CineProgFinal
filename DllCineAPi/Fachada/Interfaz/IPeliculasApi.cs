using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using DllCineApi.Utils;
using System.Data;

namespace DllCineApi.Fachada.Interfaz
{
    public interface IPeliculasApi
    {
        public List<Peliculas> ObtenerPeliculas();
        public List<GeneroPelicula> ObtenerGeneros();
        public List<EdadesPermitidas> ObtenerEdadesPermitidas();
        bool Crear(Peliculas oPelicula);
        bool Actualizar(Peliculas oPelicula, int id);
        public Peliculas CargarPeliculaPorId(int id);
        public DataTable CargarPeliculasRecaudacion();

        public bool Borrar(int id);

    }
}
