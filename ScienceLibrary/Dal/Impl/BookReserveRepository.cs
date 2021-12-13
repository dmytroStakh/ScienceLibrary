using ScienceLibrary.Dal.Api;
using ScienceLibrary.Data;
using ScienceLibrary.Models;

namespace ScienceLibrary.Dal.Impl
{
    public class BookReserveRepository : GenericRepository<BookReserve>, IBookReserveRepository
    {
        public BookReserveRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }
    }
}
