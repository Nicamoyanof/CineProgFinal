using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DllCineApi.Dominios;
using DllCineApi.Datos;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Utils;

namespace DllCineApi.Datos.Implementacion
{
    public class PeliculasDao : IDaoPeliculas
    {


        public List<Peliculas> ObtenerPeliculas()
        {
            List<Peliculas> lst = new List<Peliculas>();

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM PELICULAS");
            foreach (DataRow dr in table.Rows)
            {
                Peliculas pelicula = new Peliculas();
                pelicula.IdPelicula = int.Parse(dr["id_pelicula"].ToString());
                pelicula.Descripcion = dr["descripcion_pelicula"].ToString();
                pelicula.Nombre = dr["nombre_pelicula"].ToString();
                pelicula.Genero = ObtenerGeneros()[(Convert.ToInt32(dr["id_genero_pelicula"].ToString())) - 1];
                pelicula.EdadMinima = ObtenerEdadesPermitidas()[(Convert.ToInt32(dr["id_edad_permitida"].ToString())) - 1];
                pelicula.NombrePoster = dr["nombre_imagen"].ToString();

                lst.Add(pelicula);

            }

            return lst;
        }

        public List<GeneroPelicula> ObtenerGeneros()
        {
            List<GeneroPelicula> lst = new List<GeneroPelicula>();

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Generos_peliculas");
            foreach (DataRow dr in table.Rows)
            {
                GeneroPelicula genero = new GeneroPelicula();
                genero.IdGeneroPelicula = int.Parse(dr["id_genero_pelicula"].ToString());
                genero.Nombre = dr["nombre_genero"].ToString();
                genero.Descripcion = dr["descripcion_genero"].ToString();

                lst.Add(genero);
            }
            return lst;
        }

        public List<EdadesPermitidas> ObtenerEdadesPermitidas()
        {
            List<EdadesPermitidas> lst = new List<EdadesPermitidas>();

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Edades_permitidas");
            foreach (DataRow dr in table.Rows)
            {
                EdadesPermitidas edades = new EdadesPermitidas();
                edades.IdEdadMinima = int.Parse(dr["id_edad_permitida"].ToString());
                edades.Nombre = dr["nombre_edad"].ToString();
                edades.Edad = int.Parse(dr["minimo_edad"].ToString());

                lst.Add(edades);

            }
            return lst;
        }

        public bool Crear(Peliculas oPelicula)
        {
            bool isCreated = false;
            string script = "INSERT INTO Peliculas  VALUES (@nombre_pelicula, @id_edad_permitida, @id_genero_pelicula, @descripcion_pelicula, @nombre_imagen)";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@nombre_pelicula", oPelicula.Nombre));
            lParametros.Add(new Parametro("@id_edad_permitida", oPelicula.EdadMinima.IdEdadMinima));
            lParametros.Add(new Parametro("@id_genero_pelicula", oPelicula.Genero.IdGeneroPelicula));
            lParametros.Add(new Parametro("@descripcion_pelicula", oPelicula.Descripcion));
            lParametros.Add(new Parametro("@nombre_imagen", oPelicula.NombrePoster));

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

        public bool Actualizar(Peliculas oPelicula, int id)
        {

            bool isCreated = false;

            string script = "update Peliculas set nombre_pelicula = @nombre_pelicula, id_edad_permitida = @id_edad_permitida , id_genero_pelicula = @id_genero_pelicula, descripcion_pelicula = @descripcion_pelicula, nombre_imagen = @nombre_imagen where id_pelicula = @id_pelicula";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_pelicula", id));
            lParametros.Add(new Parametro("@nombre_pelicula", oPelicula.Nombre));
            lParametros.Add(new Parametro("@id_edad_permitida", oPelicula.EdadMinima.IdEdadMinima));
            lParametros.Add(new Parametro("@id_genero_pelicula", oPelicula.Genero.IdGeneroPelicula));
            lParametros.Add(new Parametro("@descripcion_pelicula", oPelicula.Descripcion));
            lParametros.Add(new Parametro("@nombre_imagen", oPelicula.NombrePoster));


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

            string script = "delete from peliculas where id_pelicula = @id_pelicula";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_pelicula", nro));

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

        public Peliculas CargarPeliculaPorId(int id)
        {
            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Peliculas WHERE id_pelicula=" + id);

            Peliculas pelicula = new Peliculas();

            foreach (DataRow dr in table.Rows)
            {
                pelicula.IdPelicula = int.Parse(dr["id_pelicula"].ToString());
                pelicula.Descripcion = dr["descripcion_pelicula"].ToString();
                pelicula.Nombre = dr["nombre_pelicula"].ToString();
                pelicula.Genero = ObtenerGeneros()[(Convert.ToInt32(dr["id_genero_pelicula"].ToString())) - 1];
                pelicula.EdadMinima = ObtenerEdadesPermitidas()[(Convert.ToInt32(dr["id_edad_permitida"].ToString())) - 1];
                pelicula.NombrePoster = dr["nombre_imagen"].ToString();
            }

            return pelicula;
        }

        public DataTable CargarPeliculasRecaudacion()
        {

            return Helper.ObtenerInstancia().ConsultaSQL("SP_TOTAL_RECAUDADO_POR_PELICULA", null);

        }
    }
}
