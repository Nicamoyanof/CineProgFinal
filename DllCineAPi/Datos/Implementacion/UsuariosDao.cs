using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DllCineApi.Datos;
using DllCineAPi.Datos.Interfaz;
using DllCineAPi.Dominios;

namespace DllCineAPi.Datos.Implementacion
{
    public class UsuariosDao : IDaoUsuario
    {

        public bool Login(string user, string pass)
        {

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript($"SELECT * FROM usuarios where nom_usuario = '{user}'");
            Usuarios usuario = new Usuarios();
            foreach (DataRow dr in table.Rows)
            {

                usuario.IdUsuario = int.Parse(dr["id_usuario"].ToString());
                usuario.Username = dr["nom_usuario"].ToString();
                usuario.Password = dr["contraseña"].ToString();


            }

            string contraseña = DesEncriptar(usuario.Password);

            if (contraseña.Equals(pass))
            {
                return true;
            }

            return false;
        }


        public List<Usuarios> ObtenerUsuarios()
        {
            List<Usuarios> lst = new List<Usuarios>(); 

            DataTable table = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM usuarios");
            foreach (DataRow dr in table.Rows)
            {
                Usuarios user = new Usuarios();
                user.IdUsuario = int.Parse(dr["id_usuario"].ToString());
                user.Username = dr["nom_usuario"].ToString();
                user.Password = dr["contraseña"].ToString();

                lst.Add(user);

            }

            return lst;
        }

        public bool AgregarUsuario(Usuarios user)
        {
            bool isCreated = false;
            string script = "INSERT INTO usuarios  VALUES (@nom_usuario, @contraseña)";

            List<Parametro> lParametros = new List<Parametro>();
            lParametros.Add(new Parametro("@nom_usuario", user.Username));
            lParametros.Add(new Parametro("@contraseña", user.Password));

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

        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted =
            Convert.FromBase64String(_cadenaAdesencriptar);
            //result = 
            System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

    }
}
