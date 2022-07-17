using Dapper;
using Npgsql;
using System.Data.Common;

namespace Database.Integration.Layer.DbContext
{
    public class PostgreSqlDbContext : DbContext
    {
        private DbConnection _connection;
        private readonly string _connectionString;
        private readonly string _masterDbConnectionString;
        private readonly string _dbName;

        public PostgreSqlDbContext(string host, string dbName, int port, string userName, string password)
        {
            _connectionString = $@"Host={host};
                                   Port={port.ToString()};
                                   Database={dbName};
                                   User ID={userName};
                                   Password={password};";

            _masterDbConnectionString = $@"Host={host};
                                   Port={port.ToString()};
                                   Database=postgres;
                                   User ID=postgres;
                                   Password={password};";
            _dbName = dbName;
        }

        public override DbConnection Connection
        {
            get
            {
                if (_connection == null || string.IsNullOrEmpty(_connection?.DataSource))
                {
                    _connection = new NpgsqlConnection(_connectionString);
                }
                
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }
        
        public override bool TestConnection()
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                Console.WriteLine("Opened connection to postgresql database.");
                connection.Close();
                Console.WriteLine("Closed connection to postgresql database.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open connection to postgresql database. Error: {ex.Message}");
                throw;
            }
        }

        public override bool InitializeDatabase()
        {
            try
            {
                CreateDatabase();
                CreateEmployeeTable();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to InitializeDatabase postgresql database. Error: {ex.Message}");
                throw;
            }
        }

        private void CreateDatabase()
        {
            try
            {
                var sql = $@"SELECT 1 FROM pg_database WHERE datname = '{_dbName}'";

                using var connection = new NpgsqlConnection(_masterDbConnectionString);
                connection.Open();
                var dbExists = connection.QueryFirstOrDefault<int>(sql);

                if (dbExists == 0)
                {
                    sql = $@"CREATE DATABASE ""{_dbName}""";
                    connection.Execute(sql);
                }
            }
            catch
            {
                throw;
            }
        }

        private void CreateEmployeeTable()
        {
            try
            {
                var sql = $@"CREATE TABLE IF NOT EXISTS public.employee 
                            (
	                            employee_id int PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                                employee_name varchar(50),
	                            date_joined timestamp NOT NULL,
	                            created timestamp NOT NULL
                            );";

                using var connection = new NpgsqlConnection(_connectionString);
                connection.Execute(sql);
            }
            catch
            {
                throw;
            }
        }
    }
}
