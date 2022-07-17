using Dapper;
using System.Data.Common;
using System.Data.SqlClient;


namespace Database.Integration.Layer.DbContext
{
    public class SqlDbContext: DbContext
    {
        private DbConnection _connection;
        private readonly string _connectionString;
        private readonly string _masterDbConnectionString;
        private readonly string _dbName;

        public SqlDbContext(string server, string dbName, string userName, string password, bool integratedSecurity)
        {
            var credentials = integratedSecurity ? $"Integrated Security={integratedSecurity.ToString()};" : $"user id={userName};Password={password};";

            _connectionString = $@"Data Source={server};Initial Catalog={dbName};{credentials}";
            _masterDbConnectionString = $@"Data Source={server};Initial Catalog=master;{credentials}";

            _dbName = dbName;
        }

        public override DbConnection Connection
        {
            get
            {
                if (_connection == null || string.IsNullOrEmpty(_connection?.DataSource))
                {
                    _connection = new SqlConnection(_connectionString);
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
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                Console.WriteLine("Opened connection to sql database.");
                connection.Close();
                Console.WriteLine("Closed connection to sql database.");
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open connection to sql database. Error: {ex.Message}");
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
                Console.WriteLine($"Failed to InitializeDatabase sql database. Error: {ex.Message}");
                throw;
            }
        }

        private void CreateDatabase()
        {
            try
            {
                var sql = $@"IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'{_dbName}')
                            BEGIN
	                           CREATE DATABASE [{_dbName}]
                            END
                           ";

                using var connection = new SqlConnection(_masterDbConnectionString);
                connection.Execute(sql);
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
                var sql = $@"IF OBJECT_ID(N'employee', N'U') IS NULL
                            CREATE TABLE employee 
                            (
	                            employee_id [int] IDENTITY(1,1) PRIMARY KEY,
                                employee_name [varchar](50),
	                            date_joined [datetime] NOT NULL,
	                            created [datetime] NOT NULL
                            );";

                using var connection = new SqlConnection(_connectionString);
                connection.Execute(sql);
            }
            catch
            {
                throw;
            }
        }
    }
}
