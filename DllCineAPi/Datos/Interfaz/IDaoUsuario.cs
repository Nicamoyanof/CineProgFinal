using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllCineAPi.Dominios;
namespace DllCineAPi.Datos.Interfaz
{
    public interface IDaoUsuario
    {
        List<Usuarios> ObtenerUsuarios();
        bool Login(string user, string pass);

        bool AgregarUsuario(Usuarios user);

    }
}
