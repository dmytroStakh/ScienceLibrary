using ScienceLibrary.Models;
using System.Linq;

namespace ScienceLibrary.Dal.Api
{
    public interface IBookRepository
    {
        IOrderedQueryable<Book> GetBooksInAlfabetOrder();
        void importBooksToFile();
    }
}
