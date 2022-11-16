using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class DetallesTickets
    {
        public int id_detalle_ticket { get; set; }
        public int id_ticket { get; set; }
        public Tipos_pagos id_tipo_pago { get; set; }
        public float precio_venta { get; set; }
        public int Descuentos { get; set; }
        public Asientos id_asiento_sala { get; set; }

        public DetallesTickets(int idDT, int idTick, Tipos_pagos id_tipo_p, float precio,  int desc, Asientos asiento)
        {
            id_detalle_ticket = idDT;
            id_ticket = idTick;
            id_tipo_pago = id_tipo_p;
            precio_venta = precio;
            Descuentos = desc;
            id_asiento_sala = asiento;
        }
        public DetallesTickets()
        {
            id_detalle_ticket = 0;
            id_ticket = 0;
            id_tipo_pago = new Tipos_pagos();
            precio_venta = 0;
            Descuentos = 0;
            id_asiento_sala = new Asientos();
        }

    }
}
