using Api.DAO;
using Api.Models;

namespace Api.Handlers
{
    public class ProductoHandler
    {
        private readonly ProductoDAO _dao;

        public ProductoHandler(ProductoDAO dao)
        {
            _dao = dao;
        }

        public List<Producto> Listar() => _dao.Listar();
        public Producto Obtener(string filtro) => _dao.Obtener(filtro);
        public bool Insertar(Producto p) => _dao.Insertar(p) > 0;
        public bool Actualizar(Producto p) => _dao.Actualizar(p) > 0;
        public bool Eliminar(int id) => _dao.Eliminar(id) > 0;
    }
}
