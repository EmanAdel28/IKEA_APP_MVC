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
using IKEA.DAL.Persistancs.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork unitOfWork;

        

        public EmployeeServices(IUnitOfWork UnitOfWork)
        {
          
            unitOfWork = UnitOfWork;
        }
        //public IEnumerable<EmployeeDto> GetAllEmployees(string search)
        //{

        //    var Employee = EmployeeRepository.GetAll();
        //    var FilterEmployee = Employee.Include(E => E.Department).Where(D => !D.IsDeleted && (string.IsNullOrEmpty(search) || D.Name.ToLower().Contains(search.ToLower())));

        //    var AfterFilterEmployee = FilterEmployee.Select(E => new EmployeeDto()
        //    {
        //        Id = E.Id,
        //        Name = E.Name,
        //        Address = E.Address,
        //        Age = E.Age,
        //        Salary = E.Salary,
        //        IsActive = E.IsActive,
        //        Email = E.Email,
        //        Gender = E.Gender,
        //        EmployeeType = E.EmployeeType,
        //        Department = E.Department.Name ?? "N/A"
        //    });
        //    return AfterFilterEmployee.ToList();
        //}
        public IEnumerable<EmployeeDto> GetAllEmployees(string search)
        {
            var employees = unitOfWork.employeeRepository.GetAll().Include(e => e.Department);

            var filteredEmployees = employees
                .Where(e => !e.IsDeleted &&
                           (string.IsNullOrEmpty(search)|| e.Name.ToLower().Contains(search.ToLower())));

            var employeeDtos = filteredEmployees.Select(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Address = E.Address,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = E.Gender,
                EmployeeType = E.EmployeeType,
                Department = E.Department.Name ?? "N/A"
            });

            return employeeDtos.ToList();
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
           var employee = unitOfWork.employeeRepository.GetById(id);
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
                    Departments = employee.Department?.Name



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
                DepartmentId = employee.DepartmentId,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                CreateBy = 1,
                CreatedOn = DateTime.Now,
            };
             unitOfWork.employeeRepository.Add(Employee);
            return unitOfWork.Complete();
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
                DepartmentId = employee.DepartmentId,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            unitOfWork.employeeRepository.Update(Employee);
            return unitOfWork.Complete();
        }
        public  bool DeleteEmployee(int id)
        {
            var employee = unitOfWork.employeeRepository.GetById(id);
            if (employee != null)
                unitOfWork.employeeRepository.Delete(employee) ;

            var result = unitOfWork.Complete();
            if(result>0)
                return true;

            else return false;
        }

     

    }
}
