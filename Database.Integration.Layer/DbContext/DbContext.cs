using System.Data.Common;

namespace Database.Integration.Layer.DbContext
{
    public abstract class DbContext
    {        
        public virtual DbConnection Connection { get; set; }

        public abstract bool InitializeDatabase();

        public abstract bool TestConnection();
    }

    public class DatabaseContext
    {
        public readonly DbContext dbContext;
        // Constructor
        public DatabaseContext(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool InitializeDatabase()
        {
            return dbContext.InitializeDatabase();
        }

        public bool TestConnection()
        {
            return dbContext.TestConnection();
        }
    }
}
