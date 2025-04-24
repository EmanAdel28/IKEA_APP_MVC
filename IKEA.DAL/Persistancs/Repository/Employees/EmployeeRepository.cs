using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistancs.Data;
using IKEA.DAL.Persistancs.Repository.Departments;
using IKEA.DAL.Persistancs.Repository.GenericRepository;

namespace IKEA.DAL.Persistancs.Repository.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext Context) : base(Context)
        {
            dbContext = Context;
        }
    }
}
