using Api.DAO;
using Api.Models;

namespace Api.Handlers
{
    public class OrdenHandler
    {
        private readonly OrdenDAO _dao;

        public OrdenHandler(OrdenDAO dao)
        {
            _dao = dao;
        }

        public List<Orden> Listar() => _dao.Listar();
        public Orden Obtener(int id) => _dao.Obtener(id);
        public (int idOrden, decimal totalOrden) Insertar(OrdenInsertar o)
        {
            return _dao.Insertar(o);
        }


        public bool Eliminar(int id) => _dao.Eliminar(id) > 0;
    }
}
