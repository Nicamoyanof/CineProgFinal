using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Clientes
    {
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNac { get; set; }
        public string Email { get; set; }
        public bool Socio { get; set; }
        public Ciudades Ciudad { get; set; }
        public Clientes()
        {
            Id_Cliente = 0;
            Nombre = String.Empty;
            Apellido = String.Empty;
            FechaNac = DateTime.Now;
            Email = String.Empty; ;
            Socio = true;
            Ciudad = new Ciudades();
        }
        public override string ToString()
        {
            return Apellido + " " + Nombre;
        }


    }
}
