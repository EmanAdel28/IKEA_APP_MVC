using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;

namespace IKEA.DAL.Persistancs.Repository.GenericRepository
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IQueryable<T> GetAll();

        T? GetById(int id);

        void Add(T department);

        void Update(T department);

        void Delete(T department);
    }
}
