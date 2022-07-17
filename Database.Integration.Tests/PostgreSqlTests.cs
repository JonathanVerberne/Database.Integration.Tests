using Database.Integration.Layer.DbContext;
using Database.Integration.Layer.DbContext.Entities;
using Database.Integration.Layer.DbContext.Repositories;

namespace Database.Unit.Test.Tests
{
    public class PostgreSqlTests
    {
        private readonly DatabaseContext _databaseContext;

        public PostgreSqlTests()
        {
            //pipeline host
            //_databaseContext = new DatabaseContext(new PostgreSqlDbContext("localhost", "unit-test-db", 5432, "postgres", "root"));
            //local
            _databaseContext = new DatabaseContext(new PostgreSqlDbContext("localhost", "unit-test-db", 5432, "postgres", "postgres"));
            _databaseContext.InitializeDatabase();
        }

        [Fact]
        public void TestConn()
        {
            var result = _databaseContext.TestConnection();
            Assert.True(result);
        }

        [Fact]
        public void Add_NewEmployee_ReturnEmployeeId()
        {
            var empRepo = new EmployeeRepository(_databaseContext.dbContext);

            var employeeId = empRepo.Insert(new Employee
            {
                EmployeeName = "John",
                DateJoined = DateTime.Now
            });

            Assert.True(employeeId > 0);

            var employee = empRepo.GetById(employeeId);

            Assert.Equal("John", employee.EmployeeName);
        }
    }
}