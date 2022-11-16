using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Dominios
{
    public class Funciones
    {

        public Funciones(int idFuncion, Peliculas pelicula, Salas sala, DateTime horario)
        {
            IdFuncion = idFuncion;
            Pelicula = pelicula;
            Sala = sala;
            Horario = horario;
        }
        public Funciones()
        {
            IdFuncion = 0;
            Pelicula = new Peliculas();
            Sala = new Salas();
            Horario = new DateTime();
        }

        public int IdFuncion { get; set; }
        public Peliculas Pelicula { get; set; }
        public Salas Sala { get; set; }
        public DateTime Horario { get; set; }

        public override string ToString()
        {
            return Pelicula.ToString() + " " + Horario;
        }

    }
}
