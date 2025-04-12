using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Common;

namespace IKEA.BLL.DTO_s.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; } 
        public string? Department { get; set; }


    }
}
