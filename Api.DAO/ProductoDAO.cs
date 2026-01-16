using MySqlConnector;
using Api.Models;
using System.Data;

namespace Api.DAO
{
    public class ProductoDAO
    {
        private readonly string _connectionString;

        public ProductoDAO(DbConfig config)
        {
            _connectionString = config.ConnectionString;
        }

        public List<Producto> Listar()
        {
            var lista = new List<Producto>();

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_listar_productos", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Producto
                {
                    IdProducto = reader.GetInt32("id_producto"),
                    CategoriaId = reader.GetInt32("id_categoria"),
                    Categoria = reader.GetString("categoria"),
                    Nombre = reader.GetString("nombre"),
                    Precio = reader.GetDecimal("precio"),
                    Stock = reader.GetInt32("stock")
                }); 
            }
            return lista;
        }

        public Producto Obtener(string filtro)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_buscar_producto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_producto", filtro);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;

            return new Producto
            {
                IdProducto = reader.GetInt32("id_producto"),
                CategoriaId = reader.GetInt32("id_categoria"),
                Nombre = reader.GetString("nombre"),
                Precio = reader.GetDecimal("precio"),
                Stock = reader.GetInt32("stock")
            };
        }

        public int Insertar(Producto p)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_insertar_producto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id_categoria", p.CategoriaId);
            cmd.Parameters.AddWithValue("p_nombre", p.Nombre);
            cmd.Parameters.AddWithValue("p_precio", p.Precio);
            cmd.Parameters.AddWithValue("p_stock", p.Stock);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int Actualizar(Producto p)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_actualizar_producto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id_producto", p.IdProducto);
            cmd.Parameters.AddWithValue("p_id_categoria", p.CategoriaId);
            cmd.Parameters.AddWithValue("p_nombre", p.Nombre);
            cmd.Parameters.AddWithValue("p_precio", p.Precio);
            cmd.Parameters.AddWithValue("p_stock", p.Stock);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int Eliminar(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_eliminar_producto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id_producto", id);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
