using ScienceLibrary.Dal.Api;
using ScienceLibrary.Data;
using ScienceLibrary.Models;
using System.Linq;

namespace ScienceLibrary.Dal.Impl
{
    public class BookCopyRepository : GenericRepository<BookCopy>, IBookCopyRepository
    {
        public BookCopyRepository(LibraryContext context):base(context)
        {
            _context = context;
        }

        public IQueryable<BookCopy> GetAllAvailible()
        {
            return _context.BookCopies.Where(bookCopy => bookCopy.IsReserved == false && bookCopy.IsArchived == false);
        }

        public IQueryable<BookCopy> GetAllResered()
        {
            return _context.BookCopies.Where(bookCopy => bookCopy.IsReserved == true);
        }

        public IQueryable<BookCopy> GetAllArchived()
        {
            return _context.BookCopies.Where(bookCopy => bookCopy.IsArchived == true);
        }
    }
}
