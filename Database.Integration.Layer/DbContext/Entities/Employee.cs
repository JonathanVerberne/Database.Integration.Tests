using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Unit.Test.DbContext.Entities
{
    public class Employee
    {
        public int EmployeeId { get; private set; }
        public string EmployeeName { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime Created { get; private set; }
    }
}
