using Database.Integration.Layer.DbContext.Repositories;

namespace Database.Integration.Layer.DbContext.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFCoreContext _context;

        public UnitOfWork(EFCoreContext context)
        {
            _context = context;

            Employees = new EmployeeEFCoreRepository(_context);
        }

        public IEmployeeEFCoreRepository Employees { get; private set; }
        

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}