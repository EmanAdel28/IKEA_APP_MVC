using IKEA.DAL.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }
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
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
    }
}
