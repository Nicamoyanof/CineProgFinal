using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Personal
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Telefono { get; set; }
        public string Cuil { get; set; }
        public DateTime FechaNac { get; set; }
        public Ciudades Ciudad { get; set; }
        public TiposCargos Cargo { get; set; }

        public Personal()
        {
            IdEmpleado = 0;
            Nombre = String.Empty;
            FechaIngreso = DateTime.Now;
            Telefono = String.Empty; ;
            Cuil = String.Empty; ;
            FechaNac = DateTime.Now;
            Ciudad = new Ciudades();
            Cargo = new TiposCargos();
        }
        public override string ToString()
        {
            return Nombre;
        }

    }
}
