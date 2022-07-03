using Database.Unit.Test.DbContext;
using Database.Unit.Test.DbContext.Entities;
using Database.Unit.Test.DbContext.Repositories;

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
        }

        [Fact]
        public void Get_EmployeeById_ReturnSingleEmployee()
        {
            var empRepo = new EmployeeRepository(_databaseContext.dbContext);
            var employee = empRepo.GetById(1);

            Assert.Equal("John", employee.EmployeeName);
        }
    }
}