using Microsoft.AspNetCore.Mvc;
using DllCineAPi.Fachada.Implementacion;
using DllCineAPi.Fachada.Interfaz;
using DllCineAPi.Dominios;

namespace ApiRestCine.Controllers
{
    [ApiController]
    public class UsuarioController : Controller
    {
        private IusuariosApi daoUsuario; //punto de acceso a la API

        ConvertirDataJSON convertirDataJSON = new ConvertirDataJSON();


        public UsuarioController()
        {
            daoUsuario = new UsuariosApiImp();
        }

        [HttpGet("/usuarios")]
        public IActionResult GetUsuarios()
        {
            List<Usuarios> lst = null;
            try
            {
                lst = daoUsuario.ObtenerUsuarios();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }
        [HttpPost("/usuario")]
        public IActionResult PostUser(Usuarios user)
        {
            bool isCreated = false;
            user.Password = convertirDataJSON.Encriptar(user.Password);
            try
            {
                isCreated = daoUsuario.AgregarUsuario(user);
                return Ok(isCreated);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/login/{user}/{pass}")]
        public IActionResult Login(string user, string pass)
        {
            bool isCreated = false;

            try
            {
                isCreated = daoUsuario.Login(user, pass);
                return Ok(isCreated);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        

        

    }
}
