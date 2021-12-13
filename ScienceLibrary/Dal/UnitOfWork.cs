using ScienceLibrary.Dal.Impl;
using ScienceLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceLibrary.Dal
{
    public class UnitOfWork : IDisposable
    {
        private LibraryContext _context = new LibraryContext();

        public UnitOfWork(LibraryContext context) 
        {
            _context = context;
        }
        private BookRepository _bookRepository;

        public BookRepository BookRepository
        {
            get
            {

                if (_bookRepository == null)
                {
                    _bookRepository = new BookRepository(_context);
                }
                return _bookRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed == false)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
