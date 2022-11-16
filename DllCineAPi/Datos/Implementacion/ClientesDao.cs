using DllCineApi.Datos.Interfaz;
using DllCineApi.Dominios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineApi.Datos.Implementacion
{
    public class ClientesDao : IDaoClientes
    {
        public List<Clientes> ObtenerClientes()
        {
            List<Clientes> list = new List<Clientes>();

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript
                ("SELECT id_cliente,nombre,email,fecha_nac,nombre_ciudad,socio, c.id_ciudad FROM clientes c join ciudades ciu on c.id_ciudad = ciu.id_ciudad");
            foreach (DataRow dr in table.Rows)
            {
                Clientes cliente = new Clientes();
                cliente.Id_Cliente = Convert.ToInt32(dr["id_cliente"]);
                cliente.Nombre = dr["nombre"].ToString();
                cliente.FechaNac = !string.IsNullOrEmpty(dr["fecha_nac"].ToString()) ? Convert.ToDateTime(dr["fecha_nac"].ToString()) : new DateTime();
                cliente.Ciudad.Id_Ciudad = Convert.ToInt32(dr["id_ciudad"]);
                cliente.Ciudad.Nombre = dr["nombre_ciudad"].ToString();
                cliente.Email = dr["email"].ToString();
                cliente.Socio = Convert.ToBoolean(dr["socio"].ToString());
                list.Add(cliente);
            }
            return list;
        }

        public Clientes ObtenerClienteById(int id)

        {
            Clientes clientes = new Clientes();

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript($"select * from Clientes where id_cliente = {id}");

            foreach (DataRow dr in table.Rows)
            {
                clientes.Id_Cliente = Convert.ToInt32(dr["id_cliente"]);
                clientes.Nombre = dr["nombre"].ToString();
                clientes.FechaNac = !string.IsNullOrEmpty(dr["fecha_nac"].ToString()) ? Convert.ToDateTime(dr["fecha_nac"].ToString()) : new DateTime();
                clientes.Ciudad.Id_Ciudad = Convert.ToInt32(dr["id_ciudad"]);
                clientes.Email = dr["email"].ToString();
                clientes.Socio = Convert.ToBoolean(dr["socio"].ToString());
            }
            return clientes;
        }
        public DataTable CargarSociosPorProvincia()
        {
            return Helper.ObtenerInstancia().ConsultaSQL("pa_socios_por_provincia", null);
        }
        public List<Ciudades> ObtenerCiudades()
        {
            List<Ciudades> list = new List<Ciudades>();

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM CIUDADES");
            foreach (DataRow dr in table.Rows)
            {
                Ciudades ciudad = new Ciudades();
                ciudad.Nombre = dr["nombre_ciudad"].ToString();
                ciudad.Id_Ciudad = int.Parse(dr["id_ciudad"].ToString());
                list.Add(ciudad);
            }
            return list;
        }
        public bool Crear(Clientes cliente)
        {
            bool isCreated = false;
            string script = "INSERT INTO Clientes  VALUES (@nombre, @id_ciudad, @email, @fecha_nac, @socio)";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@nombre", cliente.Nombre));
            lParametros.Add(new Parametro("@id_ciudad", cliente.Ciudad.Id_Ciudad));
            lParametros.Add(new Parametro("@email", cliente.Email));
            lParametros.Add(new Parametro("@fecha_nac", cliente.FechaNac));
            lParametros.Add(new Parametro("@socio", cliente.Socio?1:0));

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

        public bool Actualizar(Clientes cliente, int id)
        {

            bool isCreated = false;

            string script = "update clientes set nombre = @nombre, id_ciudad = @id_ciudad , email = @email, fecha_nac = @fecha_nac, socio = @socio where id_cliente = @id_cliente";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_cliente", id));
            lParametros.Add(new Parametro("@nombre", cliente.Nombre));
            lParametros.Add(new Parametro("@id_ciudad", cliente.Ciudad.Id_Ciudad));
            lParametros.Add(new Parametro("@email", cliente.Email));
            lParametros.Add(new Parametro("@fecha_nac", cliente.FechaNac));
            lParametros.Add(new Parametro("@socio", cliente.Socio));


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

            string script = "delete from clientes where id_cliente = @id_cliente";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_cliente", nro));

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
