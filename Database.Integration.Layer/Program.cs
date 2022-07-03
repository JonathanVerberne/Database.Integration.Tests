// See https://aka.ms/new-console-template for more information
using Database.Unit.Test.DbContext;

Console.WriteLine("Hello, World!");

ConnectToLocalPostgreSQLDb();

void ConnectToLocalPostgreSQLDb()
{
    try
    {
        PostgreSqlDbContext connection = new PostgreSqlDbContext("localhost", "unit-test-db", 5432, "postgres", "postgres");
        var testConn = connection.TestConnection();
        var dbCreated = connection.InitializeDatabase();
    }
    catch (Exception)
    {
        throw;
    }
}
