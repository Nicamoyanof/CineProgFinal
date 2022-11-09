using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;
using DllCineApi.Datos.Implementacion;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Fachada.Interfaz;
using System.Data;

namespace DllCineAPi.Fachada.Implementacion
{
    public class TicketsApilmp:ITickets
    {
        private IDaoTickets daoTickets;

        public TicketsApilmp()
        {
            daoTickets = new TicketsDao();
        }

    
        public List<Tickets> ObtenerTickets()
        {
            return daoTickets.ObtenerTickets(); 
        }
        public List<Clientes> ObtenerClientes()
        {
            return daoTickets.ObtenerClientes();
        }
        public List<Personal> ObtenerPersonal()
        {
            return daoTickets.ObtenerPersonal();
        }
        
        public List<Reservas> ObtenerReservas()
        {
            return daoTickets.ObtenerReservas();
        }

    }
}
