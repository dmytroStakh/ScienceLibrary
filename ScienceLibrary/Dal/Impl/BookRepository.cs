using ScienceLibrary.Dal.Api;
using ScienceLibrary.Data;
using ScienceLibrary.Models;
using System;
using System.Linq;

namespace ScienceLibrary.Dal.Impl
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public IOrderedQueryable<Book> GetBooksInAlfabetOrder()
        {
            return _context.Books.OrderBy(books => books.Title);
        }

        public void importBooksToFile()
        {
            throw new NotImplementedException();
        }
    }
}

