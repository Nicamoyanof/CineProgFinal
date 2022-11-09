using Microsoft.AspNetCore.Mvc;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;
using DllCineApi.Dominios;
using System.Data;
using ApiRestCine;


namespace ApiRestCine.Controllers
{
    public class PeliculasControllers : Controller
    {
        private IPeliculasApi daoPeliculas; //punto de acceso a la API

        ConvertirDataJSON convertirDataJSON = new ConvertirDataJSON();


        public PeliculasControllers()
        {
            daoPeliculas = new PeliculasApiImp();
        }

        [HttpGet("/peliculas")]
        public IActionResult GetProductos()
        {
            List<Peliculas> lst = null;
            try
            {
                lst = daoPeliculas.ObtenerPeliculas();
                return Ok(lst);

            }
            catch (Exception ex) {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/generos")]
        public IActionResult GetGeneros()
        {
            List<GeneroPelicula> lst = null;
            try
            {
                lst = daoPeliculas.ObtenerGeneros();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }


        [HttpGet("/edades-permitidas")]
        public IActionResult GetEdadesPermitidas()
        {
            List<EdadesPermitidas> lst = null;
            try
            {
                lst = daoPeliculas.ObtenerEdadesPermitidas();
                return Ok(lst);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/peliculasById/{id}")]
        public IActionResult GetProductos(int id)
        {
            Peliculas pel = null;
            try
            {
                pel = daoPeliculas.CargarPeliculaPorId(id);
                return Ok(pel);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpGet("/pelicula-recaudacion")]
        public IActionResult GetPeliculaRecuadacion()
        {
            DataTable lst = null;
            try
            {
                lst = daoPeliculas.CargarPeliculasRecaudacion();
                return Ok(convertirDataJSON.DataTableToJSONWithStringBuilder(lst));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpPost("/pelicula")]
        public IActionResult PostPelicula(Peliculas peliculas)
        {
            try
            {
                if (peliculas == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }

                return Ok(daoPeliculas.Crear(peliculas));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpPut("/pelicula/{id}")]
        public IActionResult PutPelicula(Peliculas peliculas, int id)
        {

            try
            {
                if (peliculas == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }

                return Ok(daoPeliculas.Actualizar(peliculas, id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        [HttpDelete("/pelicula/{id}")]
        public IActionResult DeletePelicula(int id)
        {

            try
            {
                if (id == null)
                {
                    return BadRequest("Datos de presupuesto incorrectos!");
                }

                return Ok(daoPeliculas.Borrar(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

    }
}
