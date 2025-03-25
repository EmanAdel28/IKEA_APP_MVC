using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistancs.Data;
using IKEA.DAL.Persistancs.Repository.GenericRepository;

namespace IKEA.DAL.Persistancs.Repository.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

            private readonly ApplicationDbContext dbContext;

            public DepartmentRepository(ApplicationDbContext Context):base(Context) 
            {
                dbContext = Context;
            }

         
        }
}
