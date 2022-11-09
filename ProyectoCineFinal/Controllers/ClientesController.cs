using Microsoft.AspNetCore.Mvc;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;
using DllCineApi.Datos;
using DllCineApi.Dominios;
using DllCineApi.Fachada;
using DllCineApi.Fachada;
using System.Data;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;
using DllCineApi.Datos.Interfaz;

namespace ApiRestCine.Controllers
{
    public class ClientesController : Controller
    {
        private IClientesApi daoClientes; //punto de acceso a la API

        ConvertirDataJSON convertirDataJSON = new ConvertirDataJSON();


        public ClientesController()
        {
            daoClientes = new ClientesApiImp();
        }

        [HttpGet("/clientes")]
        public IActionResult GetClientes()
        {
            List<Clientes> lst = null;
            try
            {
                lst = daoClientes.ObtenerClientes();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/ciudades/clientes")]
        public IActionResult GetCiudades()
        {
            List<Ciudades> lst = null;
            try
            {
                lst = daoClientes.CargarCiudades();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }
        //[HttpGet("/socios-provincia")]
        //public IActionResult GetSociosProvincia()
        //{
        //    List<Clientes> lst = null;
        //    try
        //    {
        //        lst = daoClientes.CargarSociosPorProvincia();
        //        return Ok(lst);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Error interno! Intente luego");
        //    }
        //}

        [HttpPost("/clientes")]
        public IActionResult PostCliente(Clientes cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return BadRequest("Datos de cliente incorrectos!");
                }

                return Ok(daoClientes.Crear(cliente));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpPut("/cliente/{id}")]
        public IActionResult PutCliente(Clientes cliente, int id)
        {

            try
            {
                if (cliente == null)
                {
                    return BadRequest("Datos de cliente incorrectos!");
                }

                return Ok(daoClientes.Actualizar(cliente, id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpDelete("/cliente/{id}")]
        public IActionResult DeleteCliente(int id)
        {

            try
            {
                if (id == null)
                {
                    return BadRequest("Datos de cliente incorrectos!");
                }

                return Ok(daoClientes.Borrar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

    }

}




