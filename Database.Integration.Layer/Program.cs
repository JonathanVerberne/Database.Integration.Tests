// See https://aka.ms/new-console-template for more information
using Database.Integration.Layer.DbContext;
using Database.Integration.Layer.DbContext.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

Console.WriteLine("Hello, World!");

RunEFCoreDbStartUp();
//ConnectToLocalPostgreSQLDb();

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

void RunEFCoreDbStartUp()
{
    try
    {
        var optionsBuilder = new DbContextOptionsBuilder<EFCoreContext>();
        optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["DemoDbContext"].ConnectionString);

        var context = new EFCoreContext(optionsBuilder.Options);
        DbInitializer.Initialize(context);

        using (var unitOfWork = new UnitOfWork(context))
        {
            var employees = context.Employees.ToList();
        }

    }
    catch (Exception ex)
    {
        throw ex;
    }
}
