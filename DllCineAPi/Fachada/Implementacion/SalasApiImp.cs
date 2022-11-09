using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using DllCineApi.Datos.Implementacion;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Fachada.Interfaz;

namespace DllCineApi.Fachada.Implementacion
{
    public class SalasApiImp : ISalasApi
    {

        private IDaoSalas daoSala;

        public SalasApiImp()
        {
            daoSala = new SalasDao();
        }


        public Salas CargarSalaPorId(int id)
        {
            return daoSala.CargarSalaPorId(id);
        }

        public List<TipoSala> CargarTipoSalas()
        {
            return daoSala.CargarTipoSalas();
        }

    }
}
