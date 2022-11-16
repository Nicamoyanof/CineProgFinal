using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DllCineApi.Dominios;

namespace DllCineApi.Datos
{
    internal class Helper
    {
        private static Helper instancia;
        private SqlConnection cnn;

        private Helper()
        {
            cnn = new SqlConnection(@"Data Source=DESKTOP-E2CTOJK;Initial Catalog=CINE;Integrated Security=True");
        }

        public static Helper ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new Helper();
            return instancia;
        }
        public DataTable ConsultaSQL(string spNombre, List<Parametro> values)
        {
            DataTable tabla = new DataTable();

            cnn.Open();
            SqlCommand cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (values != null)
            {
                foreach (Parametro oParametro in values)
                {
                    cmd.Parameters.AddWithValue(oParametro.Clave, oParametro.Valor);
                }
            }
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
        }

        public DataTable ConsultarSQLScript(string script)
        {

            DataTable tabla = new DataTable();

            cnn.Open();
            SqlCommand cmd = new SqlCommand(script, cnn);

            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;

        }

        public int ConsultaEscalarSQL(string spNombre, string pOutNombre)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = pOutNombre;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pOut);
            cmd.ExecuteNonQuery();
            cnn.Close();

            return (int)pOut.Value;
        }

        public bool EjecutarSQLMaestro(Tickets ticket)
        {
            bool ok = false;
            SqlTransaction t = null;
            SqlCommand cmd = new SqlCommand();
            try
            {

                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandText = "SP_INSERTAR_MAESTRO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@reserva", 1 );
                cmd.Parameters.AddWithValue("@funcion",ticket.Funciones.IdFuncion );
                cmd.Parameters.AddWithValue("@personal", ticket.Personal.IdEmpleado);
                cmd.Parameters.AddWithValue("@cliente", ticket.Clientes.Id_Cliente );
                cmd.Parameters.AddWithValue("@fecha_ticket", ticket.fecha_ticket );

                //parámetro de salida:
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@nro_ticket";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pOut);
                cmd.ExecuteNonQuery();

                int presupuestoNro = (int)pOut.Value;

                SqlCommand cmdDetalle;
                foreach (DetallesTickets item in ticket.DetallesTickets)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@ticket_nro", presupuestoNro);
                    cmdDetalle.Parameters.AddWithValue("@tipo_pago", item.id_tipo_pago.id_tipo_pago);
                    cmdDetalle.Parameters.AddWithValue("@precio_venta", item.precio_venta);
                    cmdDetalle.Parameters.AddWithValue("@descuento", item.Descuentos);
                    cmdDetalle.Parameters.AddWithValue("@id_asiento_sala", item.id_asiento_sala.NumeroAsiento);
                    cmdDetalle.ExecuteNonQuery();
                }
                t.Commit();
                ok = true;
            }

            catch (Exception)
            {
                if (t != null)
                    t.Rollback();
                ok = false;
            }

            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return ok;
        }
        public int EjecutarSQL(string strSql, List<Parametro> values)
        {
            int afectadas = 0;
            SqlTransaction t = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strSql;
                cmd.Transaction = t;

                if (values != null)
                {
                    foreach (Parametro param in values)
                    {
                        cmd.Parameters.AddWithValue(param.Clave, param.Valor);
                    }
                }

                afectadas = cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null) { t.Rollback(); }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();

            }

            return afectadas;
        }

        public int EjecutarSQLScript(string strSql, List<Parametro> values)
        {
            int afectadas = 0;
            SqlTransaction t = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = strSql;

                if (values != null)
                {
                    foreach (Parametro param in values)
                    {
                        cmd.Parameters.AddWithValue(param.Clave, param.Valor);
                    }
                }

                afectadas = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
               
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();

            }

            return afectadas;
        }

        public SqlConnection ObtenerConexion()
        {
            return this.cnn;
        }

    }

}
