using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Integration.Layer.DbContext.Entities
{
    public class Employee
    {
        public int EmployeeId { get; private set; }
                
        public string EmployeeName { get; set; }
                
        public DateTime DateJoined { get; set; }
        
        public DateTime Created { get; private set; } = DateTime.Now;
    }
}
