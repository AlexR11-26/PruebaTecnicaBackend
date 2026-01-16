namespace Api.Models
{
    public class DbConfig
    {
        public string ConnectionString { get; set; }

        public DbConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}