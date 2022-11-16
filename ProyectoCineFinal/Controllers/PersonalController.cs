using Microsoft.AspNetCore.Mvc;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;
using DllCineApi.Fachada;
using System.Data;
using ApiRestCine;
using DllCineApi.Dominios;
using DllCineApi.Datos;

namespace ApiRestCine.Controllers
{
    [ApiController]
    public class PersonalControllers : Controller
    {
        private IPersonalApi daoPersonal; //punto de acceso a la API

        ConvertirDataJSON convertirDataJSON = new ConvertirDataJSON();


        public PersonalControllers()
        {
            daoPersonal = new PersonalApiImp();
        }

        [HttpGet("/personal")]
        public IActionResult GetPersonal()
        {
            List<Personal> lst = null;
            try
            {
                lst = daoPersonal.CargarPersonal();
                return Ok(lst);

            }
            catch (Exception ex) {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/ciudades")]
        public IActionResult GetCiudades()
        {
            List<Ciudades> lst = null;
            try
            {
                lst = daoPersonal.CargarCiudades();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/tipos-cargos")]
        public IActionResult GetTiposCargos()
        {
            List<TiposCargos> lst = null;
            try
            {
                lst = daoPersonal.ObtenerCargos();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }
        [HttpPost("/personal")]
        public IActionResult PostPersonal(Personal personal)
        {
            try
            {
                if (personal == null)
                {
                    return BadRequest("Datos de personal incorrectos!");
                }

                return Ok(daoPersonal.Crear(personal));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpPut("/personal/{id}")]
        public IActionResult PutPersonal(Personal personal, int id)
        {

            try
            {
                if (personal == null)
                {
                    return BadRequest("Datos de personal incorrectos!");
                }

                return Ok(daoPersonal.Actualizar(personal, id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpDelete("/personal/{id}")]
        public IActionResult DeletePersonal(int id)
        {

            try
            {
                if (id == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }

                return Ok(daoPersonal.Borrar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

    }
}
