using Database.Integration.Layer.DbContext.Entities;

namespace Database.Integration.Layer.DbContext.EFCore
{
    public static class DbInitializer
    {
        public static void Initialize(EFCoreContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var employees = new Employee[]
            {
                // DateTime.Parse("2005-09-01 00:00:00")
                //DateTime.Parse("2002-09-01 00:00:00")
                new Employee{EmployeeName="Alexander", DateJoined = new DateTime(2005, 9, 1)},
                new Employee{EmployeeName="Alonso", DateJoined =  new DateTime(2002, 9, 1)}
            };

            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }

            context.SaveChanges();
        }
    }
}
