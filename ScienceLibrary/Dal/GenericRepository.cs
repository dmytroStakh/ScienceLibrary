using Microsoft.EntityFrameworkCore;
using ScienceLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceLibrary.Dal
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public LibraryContext _context = null;
        public DbSet<T> table = null;

        public GenericRepository(LibraryContext context)
        {
            _context = context;
            table = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {

            return table.ToList();
        }

        public T GetById(int? id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var data = table.Find(id);
            table.Remove(data);
        }
    }

}
