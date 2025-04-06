using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistancs.Data;

namespace IKEA.DAL.Persistancs.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext Context)
        {
            dbContext = Context;
        }

        public int Add(T item)
        {
            dbContext.Set<T>().Add(item);
            return dbContext.SaveChanges();
        }

        public int Delete(T item)
        { 
           item.IsDeleted = true;
            dbContext.Set<T>().Update(item);
           
            return dbContext.SaveChanges();
        }

        public T? GetById(int id)
        {
            var item = dbContext.Set<T>().Find(id);
            return item;
        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

        public int Update(T item)
        {
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();
        }
    }
}
