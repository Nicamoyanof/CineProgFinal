using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Salas
    {
        public int IdSala { get; set; }
        public int NumeroSala { get; set; }
        public TipoSala TipoSala { get; set; }

        public Salas(int idSala, int numeroSala, TipoSala tipoSala)
        {
            IdSala = idSala;
            NumeroSala = numeroSala;
            TipoSala = tipoSala;
        }

        public Salas()
        {
            IdSala = 0;
            NumeroSala = 0;
            TipoSala = new TipoSala();
        }

        public override string ToString()
        {
            return NumeroSala.ToString();
        }

    }
}
