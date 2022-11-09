using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using System.Data;

namespace DllCineApi.Datos.Interfaz
{
    public interface IDaoFunciones
    {

        List<Funciones> CargarFunciones();

        DataTable ClienteFuncion();

        bool Crear(Funciones oFuncion);
        bool Actualizar(Funciones oFuncion, int id);
        bool Borrar(int nro);

    }
}
