using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using System.Data; 

namespace DllCineApi.Fachada.Interfaz
{
    public interface IFuncionesApi
    {
        public List<Funciones> CargarFunciones();
        public DataTable ClienteFuncion();
        public bool Crear(Funciones oFuncion);
        public bool Actualizar(Funciones oFuncion, int id);
        public bool Borrar(int nro);

    }
}
