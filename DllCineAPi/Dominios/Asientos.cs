using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DllCineApi.Dominios
{
    public class Asientos
    {

        public Asientos(int numeroAsiento, bool disponible)
        {
            NumeroAsiento = numeroAsiento;
            Disponible = disponible;
        }
        public Asientos()
        {
            NumeroAsiento = 0;
            Disponible = true;
        }

        public int NumeroAsiento { get; set; }
        public bool Disponible { get; set; }


    }
}
