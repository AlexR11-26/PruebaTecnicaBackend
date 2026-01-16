using Microsoft.AspNetCore.Mvc;
using Api.Handlers;
using Api.Models;

namespace Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/detalle-orden")]
    public class DetalleOrdenController : ControllerBase
    {
        private readonly DetalleOrdenHandler _handler;

        public DetalleOrdenController(DetalleOrdenHandler handler)
        {
            _handler = handler;
        }

        // GET api/detalle-orden/listar/5
        [HttpGet("listar/{ordenId}")]
        public IActionResult ListarDetalle(int ordenId)
        {
            return Ok(_handler.ListarPorOrden(ordenId));
        }

        // POST api/detalle-orden/insertar
        [HttpPost("insertar")]
        public IActionResult InsertarDetalle([FromBody] DetalleOrden detalle)
        {
            return Ok(new { success = _handler.Insertar(detalle) });
        }

        // DELETE api/detalle-orden/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarDetalle(int id)
        {
            return Ok(new { success = _handler.Eliminar(id) });
        }
    }
}
