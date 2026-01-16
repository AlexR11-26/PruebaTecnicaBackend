using Microsoft.AspNetCore.Mvc;
using Api.Handlers;
using Api.Models;

namespace Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaHandler _handler;

        public CategoriaController(CategoriaHandler handler)
        {
            _handler = handler;
        }

        // GET api/categorias/listar
        [HttpGet("listar")]
        public IActionResult ListarCategorias()
        {
            return Ok(_handler.Listar());
        }

        // GET api/categorias/obtener/5
        [HttpGet("obtener/{id}")]
        public IActionResult ObtenerCategoria(int id)
        {
            var categoria = _handler.Obtener(id);
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        // POST api/categorias/insertar
        [HttpPost("insertar")]
        public IActionResult InsertarCategoria([FromBody] Categoria categoria)
        {
            return Ok(new { success = _handler.Insertar(categoria) });
        }

        // PUT api/categorias/actualizar
        [HttpPut("actualizar")]
        public IActionResult ActualizarCategoria([FromBody] Categoria categoria)
        {
            return Ok(new { success = _handler.Actualizar(categoria) });
        }

        // DELETE api/categorias/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarCategoria(int id)
        {
            return Ok(new { success = _handler.Eliminar(id) });
        }
    }
}
