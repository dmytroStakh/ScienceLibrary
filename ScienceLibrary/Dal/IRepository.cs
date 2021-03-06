using System.Collections.Generic;

namespace ScienceLibrary.Dal
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int obj);
    }
}
