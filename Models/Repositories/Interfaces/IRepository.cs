using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskPizza.Models
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetList(int? Group);
        IEnumerable<T> GetList();
        T Get(int id);
        void Create(T item);
        void Delete(int id);
        void Update(T item);
        int Count();
    }
}
