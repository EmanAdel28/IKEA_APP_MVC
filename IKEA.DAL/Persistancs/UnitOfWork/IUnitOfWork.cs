using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Persistancs.Repository.Departments;
using IKEA.DAL.Persistancs.Repository.Employees;

namespace IKEA.DAL.Persistancs.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository departmentRepository { get; set; }
        public IEmployeeRepository employeeRepository { get; set; }
        public int Complete();
    }
}
