using ScienceLibrary.Dal.Api;
using ScienceLibrary.Data;
using ScienceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceLibrary.Dal.Impl
{
    public class BookCopyRepository : GenericRepository<BookCopy>, IBookCopyRepository
    {
        private LibraryContext _context;

        public BookCopyRepository(LibraryContext context):base(context)
        {
            _context = context;
        }

        public void Test()
        {
            throw new NotImplementedException();
        }
    }
}
