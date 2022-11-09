using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using System.Data;

namespace DllCineApi.Datos.Interfaz
{
    public interface IDaoSalas
    {

        List<TipoSala> CargarTipoSalas();

        Salas CargarSalaPorId(int id);

    }
}
