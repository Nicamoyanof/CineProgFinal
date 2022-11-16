using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Dominios;
using DllCineApi.Datos;
using System.Data;

namespace DllCineApi.Datos.Implementacion
{
    public class SalasDao : IDaoSalas
    {
        public List<Asientos> CargarAsientosOcupados(int idFunc)
        {
            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript($"SELECT dt.id_asiento_sala 'asientos' FROM tickets t join Funciones f on f.id_funcion = t.id_funcion join Detalles_Tickets dt on dt.id_ticket = t.id_ticket  where f.id_funcion = {idFunc}");

            List<Asientos> lAsientos = new List<Asientos>();

            for (int i = 0; i < 20; i++)
            {

                Asientos asiento = new Asientos(i + 1, true);

                foreach (DataRow dr in table.Rows)
                {

                    if (int.Parse(dr["asientos"].ToString()).Equals(i + 1))
                    {
                        asiento.Disponible = false;
                        break;
                    }
                }
                lAsientos.Add(asiento);

            }

            return lAsientos;
        }

        public Salas CargarSalaPorId(int id)
        {
            DataTable salas = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Salas WHERE id_sala =" + id);
            Salas sala = new Salas();
            foreach (DataRow dr in salas.Rows)
            {
                sala.TipoSala = CargarTipoSalas()[Convert.ToInt32(dr["id_tipo_sala"].ToString()) - 1];
                sala.NumeroSala = Convert.ToInt32(dr["numero_sala"].ToString());
                sala.IdSala = int.Parse(dr["id_sala"].ToString());
            }
            return sala;
        }

        public List<TipoSala> CargarTipoSalas()
        {
            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Tipos_Salas");

            List<TipoSala> lTipoSalas = new List<TipoSala>();

            foreach (DataRow dr in table.Rows)
            {
                TipoSala tipoSala = new TipoSala();
                tipoSala.Nombre = dr["nombre_tipo_sala"].ToString();
                tipoSala.Descripcion = dr["descripcion_tipo_sala"].ToString();
                tipoSala.Precio = float.Parse(dr["precio_sala"].ToString());

                lTipoSalas.Add(tipoSala);
            }

            return lTipoSalas;

        }

        public List<Salas> CargarSalas()
        {
            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM salas");

            List<Salas> lSala = new List<Salas>();

            foreach (DataRow dr in table.Rows)
            {
                Salas sala = new Salas();
                sala.TipoSala = CargarTipoSalas()[Convert.ToInt32(dr["id_tipo_sala"].ToString()) - 1];
                sala.NumeroSala = Convert.ToInt32(dr["numero_sala"].ToString());
                sala.IdSala = int.Parse(dr["id_sala"].ToString());

                lSala.Add(sala);
            }

            return lSala;

        }

    }
}
