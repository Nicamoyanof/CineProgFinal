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

        public List<Asientos> CargarAsientosOcupados(int idFunc)
        {
            return daoSala.CargarAsientosOcupados(idFunc);
        }

        public Salas CargarSalaPorId(int id)
        {
            return daoSala.CargarSalaPorId(id);
        }

        public List<Salas> CargarSalas()
        {
            return daoSala.CargarSalas();
        }

        public List<TipoSala> CargarTipoSalas()
        {
            return daoSala.CargarTipoSalas();
        }

    }
}
