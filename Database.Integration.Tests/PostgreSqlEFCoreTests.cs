using Database.Integration.Layer.DbContext;
using Database.Integration.Layer.DbContext.EFCore;
using Database.Integration.Layer.DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database.Unit.Test.Tests
{
    public class PostgreSqlEFCoreTests
    {
        private readonly EFCoreContext _databaseContext;

        public PostgreSqlEFCoreTests()
        {
            try
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();

                var connectionString = config.GetSection("ConnectionStrings:DemoDbContext").Value;

                var optionsBuilder = new DbContextOptionsBuilder<EFCoreContext>();
                optionsBuilder.UseNpgsql(connectionString);

                _databaseContext = new EFCoreContext(optionsBuilder.Options);
                DbInitializer.Initialize(_databaseContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [Fact]
        public void Add_NewEmployee_ReturnEmployeeId()
        {
            var employee = new Employee
            {
                EmployeeName = "John",
                DateJoined = DateTime.Now
            };
                        
            using (var unitOfWork = new UnitOfWork(_databaseContext))
            {
                unitOfWork.Employees.Add(employee);
                unitOfWork.Complete();
                var emp = unitOfWork.Employees.GetByName(employee.EmployeeName);
                Assert.Equal("John", emp.EmployeeName);
            }
        }
    }
}