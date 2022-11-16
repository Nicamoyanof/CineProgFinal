using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Tickets
    {
        public int id_ticket { get; set; }
        public Reservas Reservas { get; set; }
        public Funciones Funciones { get; set; }
        public Personal Personal { get; set; }
        public Clientes Clientes { get; set; }
        public DateTime fecha_ticket { get; set; }
        public List<DetallesTickets> DetallesTickets { get; set; }

        public Tickets(int id, Reservas reservas, Funciones funciones, Personal personal, Clientes clientes,DateTime fecha, List<DetallesTickets> detallesTickets)
        {
            id_ticket = id;
            Reservas = reservas;
            Funciones = funciones;
            Personal = personal;
            Clientes = clientes;
            fecha_ticket = fecha;
            DetallesTickets = detallesTickets;
        }
        public Tickets()
        {
            id_ticket = 0;
            Reservas = new Reservas();
            Funciones = new Funciones();
            Personal = new Personal();
            Clientes = new Clientes();
            fecha_ticket = DateTime.Now;
            DetallesTickets = new List<DetallesTickets>();
        }
    }
}
