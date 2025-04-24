using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Persistancs.Data;
using IKEA.DAL.Persistancs.Repository.Departments;
using IKEA.DAL.Persistancs.Repository.Employees;

namespace IKEA.DAL.Persistancs.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbcontext;
        public IDepartmentRepository departmentRepository { get ; set; }
        public IEmployeeRepository employeeRepository { get ; set ; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbcontext = dbContext;
            departmentRepository = new DepartmentRepository(this.dbcontext);
            employeeRepository = new EmployeeRepository(this.dbcontext);


        }
        public int Complete()
        {
            return dbcontext.SaveChanges();
        }
    }
}
