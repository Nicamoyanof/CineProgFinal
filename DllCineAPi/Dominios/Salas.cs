using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Salas
    {

        public int NumeroSala { get; set; }
        public TipoSala TipoSala { get; set; }
        public List<Asientos> LstAsientos{ get; set; }


        public override string ToString()
        {
            return NumeroSala.ToString();
        }

    }
}
