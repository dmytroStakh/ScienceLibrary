using ScienceLibrary.Models;
using System.Linq;

namespace ScienceLibrary.Dal.Api
{
    public interface IBookCopyRepository
    {
        IQueryable<BookCopy> GetAllResered();
        IQueryable<BookCopy> GetAllArchived();
        IQueryable<BookCopy> GetAllAvailible();

    }
}
