using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Respositories
{
    public interface IRepository<T>
    {
        T Find(int id);
        IEnumerable<T> FindAll(Predicate<T> predicate);
        int Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
