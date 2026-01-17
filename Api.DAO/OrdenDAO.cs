using MySqlConnector;
using Api.Models;
using System.Data;
using System.Text.Json;

namespace Api.DAO
{
    public class OrdenDAO
    {
        private readonly string _connectionString;

        public OrdenDAO(DbConfig config)
        {
            _connectionString = config.ConnectionString;
        }

        // =========================
        // LISTAR ORDENES + DETALLE
        // =========================
        public List<Orden> Listar()
        {
            var ordenes = new List<Orden>();

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_listar_ordenes_con_detalle", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int idOrden = reader.GetInt32("id_orden");

                var orden = ordenes.FirstOrDefault(o => o.IdOrden == idOrden);

                if (orden == null)
                {
                    orden = new Orden
                    {
                        IdOrden = idOrden,
                        IdCliente = reader.GetInt32("id_cliente"),
                        Cliente = reader.GetString("cliente"),
                        TotalOrden = reader.GetDecimal("total_orden"),
                        Fecha = reader.GetDateTime("fecha"),
                        Detalles = new List<DetalleOrden>()
                    };

                    ordenes.Add(orden);
                }

                if (!reader.IsDBNull(reader.GetOrdinal("id_detalle_orden")))
                {
                    orden.Detalles.Add(new DetalleOrden
                    {
                        IdDetalleOrden = reader.GetInt32("id_detalle_orden"),
                        IdProducto = reader.GetInt32("id_producto"),
                        Producto = reader.GetString("producto"),
                        Cantidad = reader.GetInt32("cantidad"),
                        Precio = reader.GetDecimal("precio"),
                        Subtotal = reader.GetDecimal("subtotal")
                    });
                }
            }

            return ordenes;
        }

        // =========================
        // INSERTAR ORDEN + DETALLE
        // =========================
        public (int idOrden, decimal totalOrden) Insertar(OrdenInsertar o)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_insertar_orden", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Parámetro del cliente
            cmd.Parameters.AddWithValue("p_id_cliente", o.IdCliente);

            // Convertir lista de detalles a JSON
            var detalleJson = JsonSerializer.Serialize(
                o.Detalles.Select(d => new {
                    id_producto = d.IdProducto,
                    cantidad = d.Cantidad
                })
            );

            var pDetalle = cmd.Parameters.Add("p_detalle", MySqlDbType.JSON);
            pDetalle.Value = detalleJson;

            conn.Open();

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int idOrden = reader.GetInt32("id_orden");
                decimal totalOrden = reader.GetDecimal("total_orden");
                return (idOrden, totalOrden);
            }

            throw new Exception("No se pudo insertar la orden.");
        }

        public int Eliminar(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_eliminar_orden", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id_orden", id);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public Orden Obtener(int id)
        {
            Orden orden = null;

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_buscar_orden", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("p_id_orden", id);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                orden = new Orden
                {
                    IdOrden = reader.GetInt32("id_orden"),
                    IdCliente = reader.GetInt32("id_cliente"),
                    TotalOrden = reader.GetDecimal("total_orden"),
                    Fecha = reader.GetDateTime("fecha")
                };
            }

            return orden;
        }


    }
}
