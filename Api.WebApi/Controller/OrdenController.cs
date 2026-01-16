using Microsoft.AspNetCore.Mvc;
using Api.Handlers;
using Api.Models;

namespace Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/ordenes")]
    public class OrdenController : ControllerBase
    {
        private readonly OrdenHandler _handler;

        public OrdenController(OrdenHandler handler)
        {
            _handler = handler;
        }

        // GET api/ordenes/listar
        [HttpGet("listar")]
        public IActionResult ListarOrdenes()
        {
            return Ok(_handler.Listar());
        }

        // GET api/ordenes/obtener/5
        [HttpGet("obtener/{id}")]
        public IActionResult ObtenerOrden(int id)
        {
            var orden = _handler.Obtener(id);
            if (orden == null) return NotFound();
            return Ok(orden);
        }

        // POST api/ordenes/insertar
        [HttpPost("insertar")]
        public IActionResult InsertarOrden([FromBody] Orden orden)
        {
            return Ok(new { success = _handler.Insertar(orden) });
        }

        // DELETE api/ordenes/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarOrden(int id)
        {
            return Ok(new { success = _handler.Eliminar(id) });
        }
    }
}
