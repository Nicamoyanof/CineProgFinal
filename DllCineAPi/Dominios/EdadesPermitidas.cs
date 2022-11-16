using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class EdadesPermitidas
    {

        public EdadesPermitidas(int idEdades, string nombre, int edad)
        {
            IdEdadMinima = idEdades;
            Nombre = nombre;
            Edad = edad;
        }
        public EdadesPermitidas()
        {
            IdEdadMinima = 0;
            Nombre = "sin nombre";
            Edad = 0;
        }

        public int IdEdadMinima { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public override string ToString()
        {
            switch (Edad)
            {
                case 0:
                    return "Apto para todo publico";
                case 13:
                    return "Apto mayores de 13 años";
                case 15:
                    return "Apto mayores de 15 años";
                case 18:
                    return "Apto mayores de 18 años";
                default:
                    return "Error";
            }
        }

    }
}
