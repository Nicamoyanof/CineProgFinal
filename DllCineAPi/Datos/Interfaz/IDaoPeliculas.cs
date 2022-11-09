using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using System.Data;

namespace DllCineApi.Datos.Interfaz
{
    public interface IDaoPeliculas
    {

        List<Peliculas> ObtenerPeliculas();
        bool Crear(Peliculas oPelicula);
        bool Actualizar(Peliculas oPelicula, int id);
        bool Borrar(int nro);

        List<GeneroPelicula> ObtenerGeneros();
        List<EdadesPermitidas> ObtenerEdadesPermitidas();

        Peliculas CargarPeliculaPorId(int id);

        DataTable CargarPeliculasRecaudacion();

    }
}
