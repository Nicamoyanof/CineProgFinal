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
        public Personal ObtenerPersonalById(int id);
        public List<Ciudades> CargarCiudades();
        public List<TiposCargos> ObtenerCargos();
        public bool Crear(Personal personal);
        public bool Actualizar(Personal personal, int id);
        public bool Borrar(int nro);
    }
}
