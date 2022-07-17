using Database.Integration.Layer.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Integration.Layer.DbContext
{
    public class EFCoreContext : Microsoft.EntityFrameworkCore.DbContext
    {        
        public DbSet<Employee> Employees { get; set; }
        

        public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
        {
        }

        public EFCoreContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
                {
                    entity.ToTable("employee");
                    entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
                    entity.Property(e => e.EmployeeName).HasColumnName("employee_name").HasMaxLength(50);
                    entity.Property(e => e.DateJoined).HasColumnName("date_joined").HasColumnType("timestamp");
                    entity.Property(e => e.Created).HasColumnName("created_date").HasColumnType("timestamp");
                    entity.HasKey(e => e.EmployeeId);
                }
            );
        }
    }
}
