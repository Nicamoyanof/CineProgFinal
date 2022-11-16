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
    public class TicketsDao : IDaoTickets
    {
        public List<Tickets> ObtenerTickets()
        {
            List<Tickets> lst = new List<Tickets>();
            IFuncionesApi funcionesApi = new FuncionesApiImp();
            IClientesApi clientesApi = new ClientesApiImp();
            IPersonalApi personalApi = new PersonalApiImp();
            List<Funciones> lFunciones = funcionesApi.CargarFunciones();


            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Tickets");
            foreach (DataRow dr in table.Rows)
            {
                Tickets tickets = new Tickets();
                tickets.id_ticket = int.Parse(dr["id_ticket"].ToString());
                tickets.Clientes = clientesApi.ObtenerClienteById(Convert.ToInt32(dr["id_cliente"].ToString()));
                tickets.Funciones = lFunciones[(Convert.ToInt32(dr["id_funcion"].ToString())) - 1];
                tickets.Reservas = new Reservas();
                tickets.Personal = personalApi.ObtenerPersonalById(Convert.ToInt32(dr["id_empleado"].ToString()));
                //tickets.fecha_ticket = Convert.ToDateTime(dr["fecha_ticket"]);
                //tickets.DetallesTickets = new List<DetallesTickets>();

                lst.Add(tickets);

            }

            return lst;
        }

        //public List<Reservas> ObtenerReservas()
        //{
        //    List<Reservas> lst = new List<Reservas>();

        //    DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Reservas");
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        Reservas reserva = new Reservas();
        //        reserva.FechaReserva = Convert.ToDateTime(dr["fecha_reserva"]);
        //        reserva.HoraConfirmacion = new DateTime();/*Convert.ToDateTime(dr["hora_confimacion"])*/
        //        reserva.cliente = ObtenerClientes()[(Convert.ToInt32(dr["id_cliente"].ToString())) - 1];
        //       // reserva.Pelicula = ObtenerPeliculas()[(Convert.ToInt32(dr["id_edad_permitida"].ToString())) - 1];

        //        lst.Add(reserva);

        //    }

        //    return lst;
        //}
        //public List<Personal> ObtenerPersonal()
        //{
        //    List<Personal> list = new List<Personal>();

        //    DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM EMPLEADOS");
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        Personal personal = new Personal();
        //        personal.IdEmpleado = int.Parse(dr["id_empleado"].ToString());
        //        personal.Nombre = dr["nombre_empleado"].ToString();
        //        personal.Apellido = dr["apellido_empleado"].ToString();
        //        personal.FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
        //        personal.Telefono = dr["telefono"].ToString();
        //        personal.Cuil = dr["cuil"].ToString();
        //        personal.FechaNac = new DateTime(); /*Convert.ToDateTime(dr["fecha_nacimiento"].ToString());*/
        //        list.Add(personal);
        //    }
        //    return list;
        //}
        //public List<Ciudades> ObtenerCiudades()
        //{
        //    List<Ciudades> list = new List<Ciudades>();

        //    DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Ciudades");
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        Ciudades ciudad = new Ciudades();
        //        ciudad.Nombre = dr["nombre_ciudad"].ToString();
        //        list.Add(ciudad);
        //    }
        //    return list;
        //}
        //public List<TiposCargos> ObtenerTiposCargos()
        //{
        //    List<TiposCargos> list = new List<TiposCargos>();

        //    DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Tipos_cargos");
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        TiposCargos cargo = new TiposCargos();
        //        cargo.Nombre = dr["nombre_cargo"].ToString();
        //        cargo.Descripcion = dr["descripcion_cargo"].ToString();
        //        list.Add(cargo);
        //    }
        //    return list;
        //}

        //public List<Clientes> ObtenerClientes()
        //{
        //    List<Clientes> lst = new List<Clientes>();

        //    DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Clientes");
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        Clientes cliente = new Clientes();
        //        cliente.Nombre = dr["nombre"].ToString();
        //        cliente.Apellido = dr["apellido"].ToString();
        //        cliente.FechaNac = new DateTime(); /* Convert.ToDateTime(dr["fecha_nac"])*/;
        //        cliente.Email = dr["email"].ToString();
        //        cliente.Socio = Convert.ToBoolean(dr["socio"]);
        //        cliente.Ciudad = new Ciudades(); /*Convert.ToInt32(dr["id_ciudad"].ToString());*/


        //        lst.Add(cliente);
        //    }
        //    return lst;
        //}

        //public List<Peliculas> ObtenerPeliculas()
        //{
        //    List<Peliculas> lst = new List<Peliculas>();

        //    DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM PELICULAS");
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        Peliculas pelicula = new Peliculas();
        //        pelicula.Descripcion = dr["descripcion_pelicula"].ToString();
        //        pelicula.Nombre = dr["nombre_pelicula"].ToString();
        //        pelicula.Genero = ObtenerGeneros()[(Convert.ToInt32(dr["id_genero_pelicula"].ToString())) - 1];
        //        pelicula.EdadMinima = ObtenerEdadesPermitidas()[(Convert.ToInt32(dr["id_edad_permitida"].ToString())) - 1];
        //        pelicula.NombrePoster = dr["nombre_imagen"].ToString();

        //        lst.Add(pelicula);

        //    }

        //    return lst;
        //}


        //public List<GeneroPelicula> ObtenerGeneros()
        //{
        //    List<GeneroPelicula> lst = new List<GeneroPelicula>();

        //    DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Generos_peliculas");
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        GeneroPelicula genero = new GeneroPelicula();
        //        genero.Nombre = dr["nombre_genero"].ToString();
        //        genero.Descripcion = dr["descripcion_genero"].ToString();

        //        lst.Add(genero);
        //    }
        //    return lst;
        //}

        //public List<EdadesPermitidas> ObtenerEdadesPermitidas()
        //{
        //    List<EdadesPermitidas> lst = new List<EdadesPermitidas>();

        //    DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Edades_permitidas");
        //    foreach (DataRow dr in table.Rows)
        //    {
        //        EdadesPermitidas edades = new EdadesPermitidas();
        //        edades.Nombre = dr["nombre_edad"].ToString();
        //        edades.Edad = int.Parse(dr["minimo_edad"].ToString());

        //        lst.Add(edades);

        //    }
        //    return lst;
        //}

        public bool Crear(Tickets oTickets)
        {
            bool isCreated = false;

            try
            {
                bool filasAfectadas = Helper.ObtenerInstancia().EjecutarSQLMaestro(oTickets);
                if (filasAfectadas)
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

        public bool Actualizar(Tickets oTickets)
        {
            bool isCreated = false;

            string script = "update Tickets set id_reserva = @id_reserva, id_funcion = @id_funcion,id_empleado = @id_empleado, id_cliente = @id_cliente,fecha_ticket=@fecha_ticket where id_ticket = @id_ticket";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_ticket", oTickets.id_ticket));
            lParametros.Add(new Parametro("@id_reserva", 1));
            lParametros.Add(new Parametro("@id_funcion", oTickets.Funciones.IdFuncion));
            lParametros.Add(new Parametro("@id_empleado", oTickets.Personal.IdEmpleado));
            lParametros.Add(new Parametro("@id_cliente", oTickets.Clientes.Id_Cliente));
            lParametros.Add(new Parametro("@fecha_ticket", oTickets.fecha_ticket));

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

            string script = "delete from tickets where id_ticket = @id_ticket";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_ticket", nro));

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




