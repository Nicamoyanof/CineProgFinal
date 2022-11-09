using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineAPi.Dominios
{
    public class Tipos_pagos
    {
        public int id_tipo_cargo { get; set; }
        public string nombre_tipo_pago { get; set; }


        public override string ToString()
        {
            return nombre_tipo_pago;
        }

    }
}
