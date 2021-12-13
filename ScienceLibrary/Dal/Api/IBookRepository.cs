using ScienceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceLibrary.Dal.Api
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        IOrderedQueryable<Book> GetBooksInAlfabetOrder();
        void importBooksToFile();
        Book GetSBookByID(int? bookId);
        void AddNewBook(Book book);
        void DeleteBook(int bookID);
        void UpdateBook(Book book);
        void Save();
    }
}
