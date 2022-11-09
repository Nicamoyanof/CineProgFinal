using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DllCineApi.Dominios;
using DllCineApi.Datos;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;

namespace DllCineApi.Datos.Implementacion
{
    public class FuncionesDao : IDaoFunciones
    {

        IPeliculasApi dao = new PeliculasApiImp();
        ISalasApi daoSala = new SalasApiImp();

        public bool Actualizar(Funciones oFuncion, int id)
        {
            bool isCreated = false;
            
            string script = "update Funciones set id_pelicula = @id_pelicula, id_sala = @id_sala, horario = @horario where id_funcion = @id_funcion";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_funcion", id));
            lParametros.Add(new Parametro("@id_pelicula", oFuncion.Pelicula.IdPelicula));
            lParametros.Add(new Parametro("@id_sala", oFuncion.Sala.NumeroSala));
            lParametros.Add(new Parametro("@horario", oFuncion.Horario));

            try
            {
                int filasAfectadas = Helper.ObtenerInstancia().EjecutarSQLScript(script, lParametros);
                if (filasAfectadas > 0)
                {
                    isCreated = true;
                }
                else
                {
                    isCreated = false;
                }
            }
            catch (Exception)
            {
                isCreated = false;
            }

            return isCreated;
        }

        public bool Borrar(int nro)
        {
            bool isCreated = false;

            string script = "delete from Funciones where id_funcion = @id_funcion";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_funcion", nro));

            try
            {
                int filasAfectadas = Helper.ObtenerInstancia().EjecutarSQLScript(script, lParametros);
                if (filasAfectadas > 0)
                {
                    isCreated = true;
                }
                else
                {
                    isCreated = false;
                }
            }
            catch (Exception)
            {
                isCreated = false;
            }

            return isCreated;
        }

        public List<Funciones> CargarFunciones()
        {
            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Funciones WHERE DAY(horario) = 01");

            List<Funciones> lFunciones = new List<Funciones>();

            foreach (DataRow dataRow in table.Rows)
            {
                Funciones funcion = new Funciones();

                funcion.IdFuncion = int.Parse(dataRow["id_funcion"].ToString());
                funcion.Pelicula = dao.CargarPeliculaPorId(int.Parse(dataRow["id_pelicula"].ToString()));
                funcion.Sala = daoSala.CargarSalaPorId(int.Parse(dataRow["id_sala"].ToString()));
                funcion.Horario = Convert.ToDateTime(dataRow["horario"].ToString());
                lFunciones.Add(funcion);

            }

            return lFunciones;
        }

        public DataTable ClienteFuncion()
        {
            return Helper.ObtenerInstancia().ConsultaSQL("SP_CLIENTE_FUNCION", null);
        }

        public bool Crear(Funciones oFuncion)
        {
            bool isCreated = false;
            string script = "insert into Funciones(id_pelicula, id_sala, horario) values(@id_pelicula, @id_sala, @horario)";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_pelicula", oFuncion.Pelicula.IdPelicula));
            lParametros.Add(new Parametro("@id_sala", oFuncion.Sala.NumeroSala));
            lParametros.Add(new Parametro("@horario", oFuncion.Horario));

            try
            {
                int filasAfectadas = Helper.ObtenerInstancia().EjecutarSQLScript(script, lParametros);
                if (filasAfectadas > 0)
                {
                    isCreated = true;
                }
                else
                {
                    isCreated = false;
                }
            }
            catch (Exception)
            {
                isCreated = false;
            }

            return isCreated;
        }
    }
}
