using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DllCineApi.Datos;
using DllCineApi.Dominios;
using DllCineApi.Fachada.Interfaz;
using DllCineAPi.Fachada.Implementacion;


namespace ApiRestCine.Controllers
{
    public class TicketController : Controller
    {
        private ITickets daoTickets; //punto de acceso a la API
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




    }
}
