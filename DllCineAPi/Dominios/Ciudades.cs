using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Ciudades
    {
        public int Id_Ciudad { get; set; }
        public string Nombre { get; set; }

        public Ciudades()
        {
            Id_Ciudad = 0;
            Nombre = String.Empty;
        }
        public override string ToString()
        {
            return Nombre;
        }

    }
}
