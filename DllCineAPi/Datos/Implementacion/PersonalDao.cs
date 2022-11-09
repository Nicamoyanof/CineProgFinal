using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineApi.Datos;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Dominios;
using System.Data;

namespace DllCineApi.Datos.Implementacion
{
    internal class PersonalDao : IDaoPersonal
    {
        private Helper oDatos;
        public List<Personal> ObtenerPersonal()

        {
            List<Personal> list = new List<Personal>();

            DataTable table = Helper.ObtenerInstancia().ConsultaSQL("pa_consultar_empleados", null);

            foreach (DataRow dr in table.Rows)
            {
                Personal personal = new Personal();
                //personal.IdEmpleado = int.Parse(dr["id_empleado"].ToString());
                personal.Nombre = dr["nombre_empleado"].ToString();
                personal.Apellido = dr["apellido_empleado"].ToString();
                personal.FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"].ToString());
                personal.Telefono = dr["telefono"].ToString();
                personal.Cuil = dr["cuil"].ToString();
                personal.FechaNac = Convert.ToDateTime(dr["fecha_nac"].ToString());
                personal.Ciudad.Nombre = dr["nombre_ciudad"].ToString();
                personal.Cargo.Nombre = dr["nombre_cargo"].ToString();
                list.Add(personal);
            }
            return list;
        }
        public List<Ciudades> ObtenerCiudades()
        {
            List<Ciudades> list = new List<Ciudades>();

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Ciudades");
            foreach (DataRow dr in table.Rows)
            {
                Ciudades ciudad = new Ciudades();
                ciudad.Nombre = dr["nombre_ciudad"].ToString();
                list.Add(ciudad);
            }
            return list;
        }
        public List<TiposCargos> ObtenerCargos()
        {
            List<TiposCargos> list = new List<TiposCargos>();

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Tipos_cargos");
            foreach (DataRow dr in table.Rows)
            {
                TiposCargos cargo = new TiposCargos();
                cargo.Nombre = dr["nombre_cargo"].ToString();
                cargo.Descripcion = dr["descripcion_cargo"].ToString();
                list.Add(cargo);
            }
            return list;
        }
        public DataTable CargarVacacionesPorEmpleado()
        {
            return Helper.ObtenerInstancia().ConsultaSQL("pa_vacaciones_empleados", null);
        }
        public bool CrearPersonal(Personal personal)
        {
            bool isCreated = false;
            string script = "INSERT INTO Empleados VALUES (@nombre, @id_ciudad, @id_tipo_cargo, @apellido_empleado, @fecha_ingreso, @telefono, @cuil, @fecha_nac)";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@nombre", personal.Nombre));
            lParametros.Add(new Parametro("@id_ciudad", personal.Ciudad.Id_Ciudad));
            lParametros.Add(new Parametro("@id_tipo_cargo", personal.Cargo.Id_Tipo_Cargo));
            lParametros.Add(new Parametro("@apellido_empleado", personal.Apellido));
            lParametros.Add(new Parametro("@fecha_ingreso", personal.FechaIngreso));
            lParametros.Add(new Parametro("@telefono", personal.Telefono));
            lParametros.Add(new Parametro("@cuil", personal.Cuil));
            lParametros.Add(new Parametro("@fecha_nac", personal.FechaNac));

            try
            {
                int filasAfectadas = Helper.ObtenerInstancia().EjecutarSQL(script, lParametros);
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

        public bool Actualizar(Personal personal, int id)
        {

            bool isCreated = false;

            string script =
                "update Empleados set nombre = @nombre, id_ciudad = @id_ciudad," +
                " id_tipo_cargo = @id_tipo_cargo, apellido_empleado = @apellido_empleado," +
                " fecha_ingreso = @fecha_ingreso, telefono = @telefono, " +
                "cuil = @cuil, fecha _nac = @fecha_nac where id_empleado = @id_empleado";
            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_empleado", id));
            lParametros.Add(new Parametro("@nombre", personal.Nombre));
            lParametros.Add(new Parametro("@id_ciudad", personal.Ciudad.Id_Ciudad));
            lParametros.Add(new Parametro("@id_tipo_cargo", personal.Cargo.Id_Tipo_Cargo));
            lParametros.Add(new Parametro("@apellido", personal.Apellido));
            lParametros.Add(new Parametro("@fecha_ingreso", personal.FechaIngreso));
            lParametros.Add(new Parametro("@telefono", personal.Telefono));
            lParametros.Add(new Parametro("@cuil", personal.Cuil));
            lParametros.Add(new Parametro("@fecha_nac", personal.FechaNac));

            try
            {
                int filasAfectadas = Helper.ObtenerInstancia().EjecutarSQL(script, lParametros);
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

            string script = "delete from empleados where id_empleado = @id_empleado";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@id_empleado", nro));

            try
            {
                int filasAfectadas = Helper.ObtenerInstancia().EjecutarSQL(script, lParametros);
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


        public bool Crear(Personal personal)
        {
            throw new NotImplementedException();
        }
    }
}
