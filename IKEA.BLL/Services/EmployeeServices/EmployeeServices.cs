using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DTO_s;
using IKEA.BLL.DTO_s.Employee;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistancs.Repository.Departments;
using IKEA.DAL.Persistancs.Repository.Employees;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private IEmployeeRepository EmployeeRepository;

        public EmployeeServices(IEmployeeRepository _employeeRepository)
        {
            EmployeeRepository = _employeeRepository;
        }
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
           
            var Employee = EmployeeRepository.GetAll();
            var FilterEmployee = Employee.Where(D => D.IsDeleted == false);
          
            var AfterFilterEmployee = FilterEmployee.Select(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Address = E.Address,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = E.Gender,
                EmployeeType = E.EmployeeType
            });
            return AfterFilterEmployee.ToList();
        }
         public  EmployeeDetailsDto? GetEmployeeById(int id)
        {
           var employee = EmployeeRepository.GetById(id);
            if(employee is not null)
            {
                return new EmployeeDetailsDto()
                {
                    Id=employee.Id,
                    Name = employee.Name,
                    Address = employee.Address,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    CreateBy = employee.CreateBy,
                    CreatedOn = employee.CreatedOn,

                    

                };

               
            }
            return null;
        }

         public  int CreateEmployee(CreatedEmployeeDto employee)
        {
            var Employee = new Employee()
            {

                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                CreateBy = 1,
                CreatedOn = DateTime.Now,
            };
            return EmployeeRepository.Add(Employee);
        }

        public  int UpdateEmployee(UpdatedEmployeeDto employee)
        {
            var Employee = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            return EmployeeRepository.Update(Employee);
        }
        public  bool DeleteEmployee(int id)
        {
            var employee = EmployeeRepository.GetById(id);
            if (employee != null)
                return EmployeeRepository.Delete(employee) > 0;

            else return false;
        }

     

    }
}
