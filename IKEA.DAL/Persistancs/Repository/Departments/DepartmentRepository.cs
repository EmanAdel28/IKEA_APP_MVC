using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistancs.Data;

namespace IKEA.DAL.Persistancs.Repository.Departments
{
    public class DepartmentRepository:IDepartmentRepository
    {

            private readonly ApplicationDbContext dbContext;

            public DepartmentRepository(ApplicationDbContext Context)
            {
                dbContext = Context;
            }

            public int Add(Department department)
            {
                dbContext.Departments.Add(department);
                return dbContext.SaveChanges();
            }

            public int Delete(Department department)
            {
                dbContext.Departments.Remove(department);
                return dbContext.SaveChanges();
            }

            public Department? GetById(int id)
            {
                var department = dbContext.Departments.Find(id);
                return department;
            }

            public IEnumerable<Department> GetGetAll()
            {
                return dbContext.Departments.Where(D=>D.IsDeleted==false).ToList();
            }

            public int Update(Department department)
            {
                dbContext.Departments.Update(department);
                return dbContext.SaveChanges();
            }
        }
}
