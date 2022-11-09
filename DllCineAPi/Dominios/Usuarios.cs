using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllCineAPi.Dominios
{
    public class Usuarios
    {

        public Usuarios(int idUsuario, string username, string password)
        {
            IdUsuario = idUsuario;
            Username = username;
            Password = password;
        }

        public Usuarios()
        {
            IdUsuario = 0;
            Username = "user";
            Password = "pass";
        }

        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
