using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DTO_s;
using IKEA.BLL.DTO_s.Employee;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistancs.Repository.Departments;
using IKEA.DAL.Persistancs.UnitOfWork;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepaertmentServices:IDepartmentServices
    {
        private readonly IUnitOfWork unitOfWork;

        public DepaertmentServices(IUnitOfWork unitOfWork)
        {
           
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Department = unitOfWork.departmentRepository.GetAll();
            var FilterDepartment = Department.Where(D => D.IsDeleted == false);
           
           
                var AfterFilterDepartmebt = FilterDepartment.Select(dept => new DepartmentDto()
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Code = dept.Code,
                    CreationDate = dept.CreationDate,

                }
                );

            
            return AfterFilterDepartmebt.ToList();

        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var Department = unitOfWork.departmentRepository.GetById(id);
            if (Department != null)
            {
                return new DepartmentDetailsDto()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code = Department.Code,
                    CreationDate = Department.CreationDate,
                    Description = Department.Description,
                    IsDeleted = Department.IsDeleted,
                    CreateBy = Department.CreateBy,
                    CreatedOn = Department.CreatedOn,
                    LastModifiedBy = Department.LastModifiedBy,
                    LastModifiedOn = Department.LastModifiedOn,
                };

            }
            return null;
        }

        public int CreateDepartment(CreatedDepartmentDto department)
        {
            var createdDepartment = new Department()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreateBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            unitOfWork.departmentRepository.Add(createdDepartment);
            return unitOfWork.Complete();
        }

        public int UpdateDepartment(UpdatedDepartmentDto department)
        {
            var updateDepartment = new Department()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
           unitOfWork.departmentRepository.Update(updateDepartment);
            return unitOfWork.Complete();
        }
        public bool DeleteDepartment(int id)
        {
            var department = unitOfWork.departmentRepository.GetById(id);
           
            if (department != null)
              unitOfWork.departmentRepository.Delete(department) ;

            var result = unitOfWork.Complete();
            if(result>0)
                return true;
            else return false;

        }
    }
}
