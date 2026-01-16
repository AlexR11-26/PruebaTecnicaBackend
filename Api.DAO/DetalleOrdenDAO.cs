using MySqlConnector;
using Api.Models;
using System.Data;

namespace Api.DAO
{
    public class DetalleOrdenDAO
    {
        private readonly string _connectionString;

        public DetalleOrdenDAO(DbConfig config)
        {
            _connectionString = config.ConnectionString;
        }

        public List<DetalleOrden> ListarPorOrden(int ordenId)
        {
            var lista = new List<DetalleOrden>();

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_listar_detalle_orden", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_orden_id", ordenId);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new DetalleOrden
                {
                    IdDetalleOrden = reader.GetInt32("id_detalle_orden"),
                    OrdenId = reader.GetInt32("orden_id"),
                    IdProducto = reader.GetInt32("producto_id"),
                    Cantidad = reader.GetInt32("cantidad"),
                    Precio = reader.GetDecimal("precio")
                });
            }

            return lista;
        }

        public int Insertar(DetalleOrden detalle)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_insertar_detalle_orden", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_orden_id", detalle.OrdenId);
            cmd.Parameters.AddWithValue("p_producto_id", detalle.IdProducto);
            cmd.Parameters.AddWithValue("p_cantidad", detalle.Cantidad);
            cmd.Parameters.AddWithValue("p_precio", detalle.Precio);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int Eliminar(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_eliminar_detalle_orden", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_id", id);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
