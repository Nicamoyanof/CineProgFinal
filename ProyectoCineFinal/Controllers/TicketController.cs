using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DllCineApi.Datos;
using DllCineApi.Dominios;
using DllCineApi.Fachada.Interfaz;
using DllCineAPi.Fachada.Implementacion;


namespace ApiRestCine.Controllers
{
    [ApiController]
    public class TicketController : Controller
    {
        private ITickets daoTickets; //punto de acceso a la API
        ConvertirDataJSON convertirDataJSON = new ConvertirDataJSON();
        public TicketController()
        {
            daoTickets = new TicketsApilmp();
        }

        [HttpGet("/tickets")]
        public IActionResult GetProductos()
        {
            List<Tickets> lst = null;
            try
            {
                lst = daoTickets.ObtenerTickets();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpPost("/ticket")]
        public IActionResult PostProductos(Tickets ticket)
        {
            try
            {
                if (ticket == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }

                return Ok(daoTickets.Crear(ticket));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }




    }
}
