using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;

namespace IKEA.DAL.Persistancs.Repository.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetGetAll();

        Department? GetById(int id);

        int Add(Department department);

        int Update(Department department);

        int Delete(Department department);
    }
}
