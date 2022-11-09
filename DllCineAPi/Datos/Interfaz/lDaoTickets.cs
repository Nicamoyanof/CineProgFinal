using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Dominios;

namespace DllCineApi.Datos.Interfaz
{
    public interface IDaoTickets
    {
        List<Tickets> ObtenerTickets();
        bool Crear(Tickets oTickets);
        bool Actualizar(Tickets oTickets);
        bool Borrar(int nro);

        List<Clientes> ObtenerClientes();
        List<Personal> ObtenerPersonal();
        
        List<Reservas> ObtenerReservas();


  
    }
}
