using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;


namespace DllCineApi.Fachada.Interfaz
{
    public interface ITickets
    {
        public List<Tickets> ObtenerTickets();
        public bool Crear(Tickets tickets);
        //public List<Clientes> ObtenerClientes();
        //public List<Personal> ObtenerPersonal();
        
        //public List<Reservas> ObtenerReservas();


    }
}
