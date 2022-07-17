using Database.Integration.Layer.DbContext.EFCore;
using Database.Integration.Layer.DbContext.Entities;

namespace Database.Integration.Layer.DbContext.Repositories
{
    public interface IEmployeeEFCoreRepository : IRepository<Employee>
    {
        EFCoreContext DbContext { get; }

        Employee GetByName(string employeeName);
    }
}