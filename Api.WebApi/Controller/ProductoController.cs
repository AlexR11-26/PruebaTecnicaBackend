using Microsoft.AspNetCore.Mvc;
using Api.Handlers;
using Api.Models;

namespace Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoHandler _handler;

        public ProductoController(ProductoHandler handler)
        {
            _handler = handler;
        }

        // GET api/productos/listar
        [HttpGet("listar")]
        public IActionResult ListarProductos()
        {
            return Ok(_handler.Listar());
        }

        // GET api/productos/obtener/5
        [HttpGet("obtener/{filtro}")]
        public IActionResult ObtenerProducto(string filtro)
        {
            var producto = _handler.Obtener(filtro);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        // POST api/productos/insertar
        [HttpPost("insertar")]
        public IActionResult InsertarProducto([FromBody] Producto producto)
        {
            return Ok(new { success = _handler.Insertar(producto) });
        }

        // PUT api/productos/actualizar
        [HttpPut("actualizar")]
        public IActionResult ActualizarProducto([FromBody] Producto producto)
        {
            return Ok(new { success = _handler.Actualizar(producto) });
        }

        // DELETE api/productos/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarProducto(int id)
        {
            return Ok(new { success = _handler.Eliminar(id) });
        }
    }
}
