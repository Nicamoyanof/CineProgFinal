using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineAPi.Dominios;

namespace DllCineAPi.Fachada.Interfaz
{
    public interface IusuariosApi
    {

        public List<Usuarios> ObtenerUsuarios();
        public  bool Login(string user, string pass);

        public bool AgregarUsuario(Usuarios user);

    }
}
