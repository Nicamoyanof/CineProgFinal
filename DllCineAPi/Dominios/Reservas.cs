using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Reservas
    {
        public Reservas()
        {
            cliente = new Clientes();
            Pelicula = new Peliculas();
            FechaReserva = DateTime.Now;
            HoraConfirmacion = DateTime.Now;
        }

        public Clientes cliente { get; set; }
        public Peliculas Pelicula { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime HoraConfirmacion { get; set; }
    }
}
