using Database.Integration.Layer.DbContext.EFCore;
using Database.Integration.Layer.DbContext.Entities;

namespace Database.Integration.Layer.DbContext.Repositories
{
    public class EmployeeEFCoreRepository : Repository<Employee>, IEmployeeEFCoreRepository
    {
        public EmployeeEFCoreRepository(EFCoreContext context) : base(context)
        {
        }

        public EFCoreContext DbContext
        {
            get { return _context as EFCoreContext; }
        }

        public Employee GetByName(string employeeName)
        {
            var employee = DbContext.Employees.Where(e => e.EmployeeName == employeeName).FirstOrDefault();
            return employee;
        }
    }
}