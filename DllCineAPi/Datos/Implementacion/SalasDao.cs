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
        public Salas CargarSalaPorId(int id)
        {
            DataTable salas = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Salas WHERE id_sala =" + id);
            Salas sala = new Salas();
            foreach (DataRow dr in salas.Rows)
            {
                sala.TipoSala = CargarTipoSalas()[Convert.ToInt32(dr["id_tipo_sala"].ToString()) - 1];
                sala.NumeroSala = Convert.ToInt32(dr["numero_sala"].ToString());
                sala.LstAsientos = new List<Asientos>();

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
    }
}
