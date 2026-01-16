using Microsoft.AspNetCore.Mvc;
using Api.Handlers;
using Api.Models;

namespace Api.WebApi.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteHandler _handler;

        public ClienteController(ClienteHandler handler)
        {
            _handler = handler;
        }

        // GET api/clientes/listar
        [HttpGet("listar")]
        public IActionResult ListarClientes()
        {
            return Ok(_handler.Listar());
        }

        // GET api/clientes/obtener/5
        [HttpGet("obtener/{id}")]
        public IActionResult ObtenerCliente(int id)
        {
            var cliente = _handler.Obtener(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        // POST api/clientes/insertar
        [HttpPost("insertar")]
        public IActionResult InsertarCliente([FromBody] Cliente cliente)
        {
            return Ok(new { accion = "InsertarCliente", success = _handler.Insertar(cliente) });
        }

        // PUT api/clientes/actualizar
        [HttpPut("actualizar")]
        public IActionResult ActualizarCliente([FromBody] Cliente cliente)
        {
            return Ok(new { accion = "ActualizarCliente", success = _handler.Actualizar(cliente) });
        }

        // DELETE api/clientes/eliminar/5
        [HttpDelete("eliminar/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            return Ok(new { accion = "EliminarCliente", success = _handler.Eliminar(id) });
        }
    }
}
