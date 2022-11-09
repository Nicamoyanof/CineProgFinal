using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineAPi.Datos.Implementacion;
using DllCineAPi.Datos.Interfaz;
using DllCineAPi.Dominios;
using DllCineAPi.Fachada.Interfaz;

namespace DllCineAPi.Fachada.Implementacion
{
    public class UsuariosApiImp : IusuariosApi
    {

        private IDaoUsuario daoUsuario;

        public UsuariosApiImp()
        {
            daoUsuario = new UsuariosDao();
        }

        public bool AgregarUsuario(Usuarios user)
        {
            return daoUsuario.AgregarUsuario(user);
        }

        public bool Login(string user, string pass)
        {
            return daoUsuario.Login(user, pass);
        }

        public List<Usuarios> ObtenerUsuarios()
        {
            return daoUsuario.ObtenerUsuarios();
        }
    }
}
