using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Tipos_pagos
    {
        public int id_tipo_pago { get; set; }
        public string nombre_tipo_pago { get; set; }

        public Tipos_pagos()
        {
            id_tipo_pago = 0;
            nombre_tipo_pago = "s/n";
        }
        public Tipos_pagos(int id_tipo_pago, string nombre_tipo_pago)
        {
            this.id_tipo_pago = id_tipo_pago;
            this.nombre_tipo_pago = nombre_tipo_pago;
        }

        public override string ToString()
        {
            return nombre_tipo_pago;
        }

    }
}
