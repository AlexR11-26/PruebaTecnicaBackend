using Api.DAO;
using Api.Models;

namespace Api.Handlers
{
    public class ClienteHandler
    {
        private readonly ClienteDAO _dao;

        public ClienteHandler(ClienteDAO dao)
        {
            _dao = dao;
        }

        public List<Cliente> Listar() => _dao.Listar();
        public Cliente Obtener(int id) => _dao.Obtener(id);
        public bool Insertar(Cliente c) => _dao.Insertar(c) > 0;
        public bool Actualizar(Cliente c) => _dao.Actualizar(c) > 0;
        public bool Eliminar(int id) => _dao.Eliminar(id) > 0;
    }
}
