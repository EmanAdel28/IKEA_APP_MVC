using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Common;

namespace IKEA.BLL.DTO_s.Employee
{
    public class CreatedEmployeeDto
    {
        [Required]
        public string Name { get; set; }
        [Range(20,50)]
        public int? Age { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int Salary { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
