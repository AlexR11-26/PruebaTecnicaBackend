using MySqlConnector;
using Api.Models;
using System.Data;

namespace Api.DAO
{
    public class CategoriaDAO
    {
        private readonly string _connectionString;

        public CategoriaDAO(DbConfig config)
        {
            _connectionString = config.ConnectionString;
        }

        public List<Categoria> Listar()
        {
            var lista = new List<Categoria>();

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_listar_categorias", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Categoria
                {
                    IdCategoria = reader.GetInt32("id_categoria"),
                    Nombre = reader.GetString("nombre"),
                });
            }
            return lista;
        }

        public Categoria Obtener(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_buscar_categoria", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id_categoria", id);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;

            return new Categoria
            {
                IdCategoria = reader.GetInt32("id_categoria"),
                Nombre = reader.GetString("nombre"),
            };
        }

        public int Insertar(Categoria c)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_insertar_categoria", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_nombre", c.Nombre);
            cmd.Parameters.AddWithValue("p_descripcion", c.Descripcion);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int Actualizar(Categoria c)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_actualizar_categoria", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id_categoria", c.IdCategoria);
            cmd.Parameters.AddWithValue("p_nombre", c.Nombre);
           

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int Eliminar(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_eliminar_categoria", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id_categoria", id);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
