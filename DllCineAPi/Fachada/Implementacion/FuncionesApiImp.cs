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
    public class FuncionesApiImp:IFuncionesApi
    {
        private IDaoFunciones daoFunciones;

        public FuncionesApiImp()
        {
            daoFunciones = new FuncionesDao();
        }

        public bool Actualizar(Funciones oFuncion, int id)
        {
            return daoFunciones.Actualizar(oFuncion, id);
        }

        public bool Borrar(int nro)
        {
            return daoFunciones.Borrar(nro);
        }

        public List<Funciones> CargarFunciones()
        {
            return daoFunciones.CargarFunciones();
        }

        public DataTable ClienteFuncion()
        {
            return daoFunciones.ClienteFuncion();
        }

        public bool Crear(Funciones oFuncion)
        {
            return daoFunciones.Crear(oFuncion);
        }
    }
}
