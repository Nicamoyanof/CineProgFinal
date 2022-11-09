using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class TipoSala
    {

        public TipoSala(string nombre, string descripcion, float precio)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
        }
        public TipoSala()
        {
            Nombre = "SIN NOMBRE";
            Descripcion = "sin descripcion";
            Precio = 0;
        }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }

    }
}
