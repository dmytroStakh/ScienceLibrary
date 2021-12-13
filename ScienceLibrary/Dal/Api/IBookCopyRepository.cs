using ScienceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceLibrary.Dal.Api
{
    public interface IBookCopyRepository
    {
        IQueryable<BookCopy> GetAllResered();
        IQueryable<BookCopy> GetAllArchived();
        IQueryable<BookCopy> GetAllAvailible();

    }
}
