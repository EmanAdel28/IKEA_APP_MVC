using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistancs.Repository.GenericRepository;

namespace IKEA.DAL.Persistancs.Repository.Departments
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
      
    }
}
