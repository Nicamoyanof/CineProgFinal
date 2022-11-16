using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using DllCineApi.Datos.Implementacion ;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Fachada.Interfaz;


namespace DllCineApi.Fachada.Implementacion
{
    public class PersonalApiImp : IPersonalApi
    {
        private IDaoPersonal daoPersonal;

        public PersonalApiImp()
        {
            daoPersonal = new PersonalDao();
        }
        public List<Personal> CargarPersonal()
        {
            return daoPersonal.ObtenerPersonal();
        }
        public List<Ciudades> CargarCiudades()
        {
            return daoPersonal.ObtenerCiudades();
        }
        public List<TiposCargos> ObtenerCargos()
        {
            return daoPersonal.ObtenerCargos();
        }
        public bool Crear(Personal personal)
        {
            return daoPersonal.Crear(personal);
        }

        public bool Actualizar(Personal personal, int id)
        {
            return daoPersonal.Actualizar(personal, id);
        }

        public bool Borrar(int nro)
        {
           return daoPersonal.Borrar(nro);
        }

        public Personal ObtenerPersonalById(int id)
        {
            return daoPersonal.ObtenerPersonalById(id);
        }
    }
}
