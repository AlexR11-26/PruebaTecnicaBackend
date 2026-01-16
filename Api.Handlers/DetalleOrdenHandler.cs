using Api.DAO;
using Api.Models;

namespace Api.Handlers
{
    public class DetalleOrdenHandler
    {
        private readonly DetalleOrdenDAO _dao;

        public DetalleOrdenHandler(DetalleOrdenDAO dao)
        {
            _dao = dao;
        }

        public List<DetalleOrden> ListarPorOrden(int ordenId)
            => _dao.ListarPorOrden(ordenId);

        public bool Insertar(DetalleOrden detalle)
            => _dao.Insertar(detalle) > 0;

        public bool Eliminar(int id)
            => _dao.Eliminar(id) > 0;
    }
}
