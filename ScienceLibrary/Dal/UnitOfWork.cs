using ScienceLibrary.Dal.Impl;
using ScienceLibrary.Data;
using System;

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
        private BookCopyRepository _bookCopyRepository;
        private BookReserveRepository bookReserveRepository;

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

        public BookCopyRepository BookCopyRepository
        {
            get
            {

                if (_bookCopyRepository == null)
                {
                    _bookCopyRepository = new BookCopyRepository(_context);
                }
                return _bookCopyRepository;
            }
        }

        public BookReserveRepository BookReserveRepository
        {
            get
            {

                if (bookReserveRepository == null)
                {
                    bookReserveRepository = new BookReserveRepository(_context);
                }
                return bookReserveRepository;
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
