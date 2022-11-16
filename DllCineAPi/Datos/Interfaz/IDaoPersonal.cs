using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;

namespace DllCineApi.Datos.Interfaz
{
    internal interface IDaoPersonal
    {
        List<Personal> ObtenerPersonal();
        List<Ciudades> ObtenerCiudades();
        List<TiposCargos> ObtenerCargos();

        Personal ObtenerPersonalById(int id);
        bool Crear(Personal personal);
        bool Actualizar(Personal personal, int id);
        bool Borrar(int nro);

    }
}
