using Microsoft.AspNetCore.Mvc;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;
using DllCineApi.Dominios;
using System.Data;
using ApiRestCine;


namespace ApiRestCine.Controllers
{
    public class FuncionesControllers : Controller
    {

        private IFuncionesApi daoFuncion; //punto de acceso a la API

        ConvertirDataJSON convertirDataJSON = new ConvertirDataJSON();

        public FuncionesControllers()
        {
            daoFuncion = new FuncionesApiImp();
        }

        [HttpGet("/funciones")]
        public IActionResult GetFunciones()
        {
            List<Funciones> lst = null;
            try
            {
                lst = daoFuncion.CargarFunciones();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }


        [HttpGet("/funcion-cliente")]
        public IActionResult GetFuncionCliente()
        {
            DataTable lst = null;
            try
            {
                lst = daoFuncion.ClienteFuncion();
                return Ok(convertirDataJSON.DataTableToJSONWithStringBuilder(lst));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpPost("/funcion")]
        public IActionResult PostProductos(Funciones funciones)
        {
            try
            {
                if (funciones == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }

                return Ok(daoFuncion.Crear(funciones));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpPut("/funcion/{id}")]
        public IActionResult PutProductos(Funciones funciones, int id)
        {
            try
            {
                if (funciones == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }

                return Ok(daoFuncion.Actualizar(funciones, id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpDelete("/funcion/{id}")]
        public IActionResult DeleteProductos( int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }

                return Ok(daoFuncion.Borrar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

    }
}
