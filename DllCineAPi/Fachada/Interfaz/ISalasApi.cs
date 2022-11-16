using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;

namespace DllCineApi.Fachada.Interfaz
{
    public interface ISalasApi
    {
        public List<TipoSala> CargarTipoSalas();

        public Salas CargarSalaPorId(int id);

        public List<Asientos> CargarAsientosOcupados(int idFunc);

        public List<Salas> CargarSalas();

    }
}
