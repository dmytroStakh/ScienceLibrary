using ScienceLibrary.Dal.Api;
using ScienceLibrary.Data;
using ScienceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
