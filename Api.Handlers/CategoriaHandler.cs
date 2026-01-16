using Api.DAO;
using Api.Models;

namespace Api.Handlers
{
    public class CategoriaHandler
    {
        private readonly CategoriaDAO _dao;

        public CategoriaHandler(CategoriaDAO dao)
        {
            _dao = dao;
        }

        public List<Categoria> Listar() => _dao.Listar();
        public Categoria Obtener(int id) => _dao.Obtener(id);
        public bool Insertar(Categoria c) => _dao.Insertar(c) > 0;
        public bool Actualizar(Categoria c) => _dao.Actualizar(c) > 0;
        public bool Eliminar(int id) => _dao.Eliminar(id) > 0;
    }
}
