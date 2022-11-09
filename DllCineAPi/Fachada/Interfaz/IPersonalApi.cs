using DllCineApi.Datos;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Fachada.Interfaz
{
    public interface IPersonalApi
    {
        public List<Personal> CargarPersonal();
        public List<Ciudades> CargarCiudades();
        public List<TiposCargos> ObtenerCargos();
        bool Crear(Personal personal);
        bool Actualizar(Personal personal, int id);
        bool Borrar(int nro);
    }
}
