using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class TiposCargos
    {
        public int Id_Tipo_Cargo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public TiposCargos()
        {
            Id_Tipo_Cargo = 0;
            Nombre = String.Empty;
            Descripcion = String.Empty;

        }
        public override string ToString()
        {
            return Nombre + Descripcion;
        }

    }
}
