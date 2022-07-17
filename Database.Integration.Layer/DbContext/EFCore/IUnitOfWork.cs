using Database.Integration.Layer.DbContext.Repositories;

namespace Database.Integration.Layer.DbContext.EFCore
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeEFCoreRepository Employees { get; }
        
        int Complete();
    }
}