using Database.Integration.Layer.DbContext;
using Database.Integration.Layer.DbContext.Entities;
using Database.Integration.Layer.DbContext.Repositories;

namespace Database.Unit.Test.Tests
{
    public class SqlTests
    {
        private readonly DatabaseContext _databaseContext;

        public SqlTests()
        {
            _databaseContext = new DatabaseContext(new SqlDbContext(@"(localdb)\MSSQLLocalDB", "unit-test-db", "", "", true));
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