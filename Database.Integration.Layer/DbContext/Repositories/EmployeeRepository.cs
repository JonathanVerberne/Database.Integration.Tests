using Dapper;
using Database.Unit.Test.DbContext.Entities;
using Npgsql;
using System.Data;

namespace Database.Unit.Test.DbContext.Repositories
{
    public class EmployeeRepository
    {        
        private readonly DbContext _dbContext;
        private const string _primaryTable = "employee";

        public EmployeeRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Employee GetById(int employeeId)
        {
            try
            {
                var sql = $@"SELECT employee_id as EmployeeId, 
                                    employee_name as EmployeeName,
                                    date_joined as DateJoined,
                                    created as Created
                            FROM 
                                {_primaryTable} 
                            WHERE 
                                employee_id = @employee_id";


                using var connection = _dbContext.Connection;
                connection.Open();
                var employee = connection.QueryFirst<Employee>(sql, new { employee_id = employeeId });

                return employee;
            }
            catch
            {
                throw;
            }
        }

        public int Insert(Employee employee)
        {
            try
            {
                var sql = GetInsertCommand();

                var parameters = new DynamicParameters();
                parameters.Add("@employee_name", employee.EmployeeName, DbType.String, ParameterDirection.Input, 50);
                parameters.Add("@date_joined", employee.DateJoined);
                parameters.Add("@created", DateTime.Now);

                using var connection = _dbContext.Connection;
                connection.Open();
                using var trans = connection.BeginTransaction();
                var employeeId = connection.ExecuteScalar<int>(sql, parameters, trans, 0);
                trans.Commit();

                Console.WriteLine($"({_primaryTable}) New records inserted --> (employee_id: {employeeId}).");

                return employeeId;
            }
            catch
            {
                throw;
            }
        }

        private string GetInsertCommand()
        {
            string identity = string.Empty;

            switch (_dbContext.GetType().Name)
            {
                case "PostgreSqlDbContext":
                    identity = "RETURNING employee_id";
                    break;

                case "SqlDbContext":
                    identity = "SELECT @@IDENTITY";
                    break;

                default:
                    break;
            };

            string sql = $@"INSERT INTO {_primaryTable} 
                            (
                                employee_name,
                                date_joined,
                                created
                            ) 
                            VALUES(@employee_name, @date_joined, @created) {identity}";

            return sql;
        }
    }
}
