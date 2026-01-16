using MySqlConnector;
using Api.Models;
using System.Data;

namespace Api.DAO
{
    public class ClienteDAO
    {
        private readonly string _connectionString;

        public ClienteDAO(DbConfig config)
        {
            _connectionString = config.ConnectionString;
        }

        // ▶ INSERT
        public int Insertar(Cliente cliente)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_insertar_cliente", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_nombres", cliente.Nombres);
            cmd.Parameters.AddWithValue("p_apellidos", cliente.Apellidos);
            cmd.Parameters.AddWithValue("p_email", cliente.Email);
            cmd.Parameters.AddWithValue("p_telefono", cliente.Telefono);
            cmd.Parameters.AddWithValue("p_numero_documento", cliente.Documento);


            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        // ▶ LISTAR
        public List<Cliente> Listar()
        {
            var lista = new List<Cliente>();

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_listar_clientes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Cliente
                {
                    IdCliente = reader.GetInt32("id_cliente"),
                    Nombres = reader.GetString("nombres"),
                    Apellidos = reader.GetString("apellidos"),
                    Email = reader.GetString("email"),
                    Telefono = reader.GetString("telefono")
                });
            }

            return lista;
        }

        // ▶ OBTENER POR ID
        public Cliente Obtener(int id)
        {
            Cliente cliente = null;

            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_buscar_cliente_por_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_id_cliente", id);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                cliente = new Cliente
                {
                    IdCliente = reader.GetInt32("id_cliente"),
                    Nombres = reader.GetString("nombres"),
                    Apellidos = reader.GetString("apellidos"),
                    Email = reader.GetString("email"),
                    Telefono = reader.GetString("telefono")
                };
            }

            return cliente;
        }

        // ▶ UPDATE
        public int Actualizar(Cliente cliente)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_actualizar_cliente", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_id_cliente", cliente.IdCliente);
            cmd.Parameters.AddWithValue("p_nombres", cliente.Nombres);
            cmd.Parameters.AddWithValue("p_apellidos", cliente.Apellidos);
            cmd.Parameters.AddWithValue("p_email", cliente.Email);
            cmd.Parameters.AddWithValue("p_telefono", cliente.Telefono);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        // ▶ DELETE
        public int Eliminar(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand("sp_eliminar_cliente", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_id_cliente", id);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
