using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Common;
using IKEA.DAL.Models.Departments;

namespace IKEA.DAL.Models.Employees
{
    public class Employee:ModelBase
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

    }
}
