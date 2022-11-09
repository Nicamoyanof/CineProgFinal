using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using DllCineApi.Datos.Implementacion;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Fachada.Interfaz;
using System.Data;

namespace DllCineApi.Fachada.Implementacion
{
    public class PeliculasApiImp : IPeliculasApi
    {

        private IDaoPeliculas daoPelicula;

        public PeliculasApiImp()
        {
            daoPelicula = new PeliculasDao();
        }

        public bool Actualizar(Peliculas oPelicula, int id)
        {
           return daoPelicula.Actualizar(oPelicula, id);
        }

        public bool Borrar(int id)
        {
            return daoPelicula.Borrar(id);
        }

        public Peliculas CargarPeliculaPorId(int id)
        {
            return daoPelicula.CargarPeliculaPorId(id);
        }

        public DataTable CargarPeliculasRecaudacion()
        {
            return daoPelicula.CargarPeliculasRecaudacion();
        }

        public bool Crear(Peliculas oPelicula)
        {
            return daoPelicula.Crear(oPelicula);
        }

        public List<EdadesPermitidas> ObtenerEdadesPermitidas()
        {
            return daoPelicula.ObtenerEdadesPermitidas();
        }

        public List<GeneroPelicula> ObtenerGeneros()
        {
            return daoPelicula.ObtenerGeneros();
        }

        public List<Peliculas> ObtenerPeliculas()
        {
            return daoPelicula.ObtenerPeliculas();
        }
    }
}
