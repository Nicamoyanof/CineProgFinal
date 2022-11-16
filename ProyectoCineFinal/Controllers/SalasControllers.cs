using Microsoft.AspNetCore.Mvc;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;
using DllCineApi.Dominios;
using System.Data;
using ApiRestCine;



namespace ApiRestCine.Controllers
{
    [ApiController]
    public class SalasControllers : Controller
    {

        private ISalasApi daoSalas; //punto de acceso a la API

        ConvertirDataJSON convertirDataJSON = new ConvertirDataJSON();


        public SalasControllers()
        {
            daoSalas = new SalasApiImp();
        }

        [HttpGet("/tipos-salas")]
        public IActionResult GetTipoSalas()
        {
            List<TipoSala> lst = null;
            try
            {
                lst = daoSalas.CargarTipoSalas();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/salas")]
        public IActionResult GetSalas()
        {
            List<Salas> lst = null;
            try
            {
                lst = daoSalas.CargarSalas();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/salas-by-id/{id}")]
        public IActionResult GetSalasById(int id)
        {
            Salas sala = null;
            try
            {
                sala = daoSalas.CargarSalaPorId(id);
                return Ok(sala);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/asientos-ocupaoas-by-func/{id}")]
        public IActionResult GetAsientosOcupadosByFunc(int id)
        {
            try
            {
                List<Asientos> lAsientos = daoSalas.CargarAsientosOcupados(id);
                return Ok(lAsientos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }
    }
}
