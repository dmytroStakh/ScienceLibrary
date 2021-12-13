using Microsoft.EntityFrameworkCore;
using ScienceLibrary.Dal.Api;
using ScienceLibrary.Data;
using ScienceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceLibrary.Dal.Impl
{
    public class BookRepository : IBookRepository
    {
        private LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }
        public void AddNewBook(Book book)
        {
            _context.Books.Add(book);
        }

        public void DeleteBook(int bookID)
        {
            Book book = _context.Books.Find(bookID);
            _context.Books.Remove(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public IOrderedQueryable<Book> GetBooksInAlfabetOrder()
        {
            return _context.Books.OrderBy(books => books.Title);
        }

        public Book GetSBookByID(int? bookId)
        {
            return _context.Books.Find(bookId);
        }

        public void importBooksToFile()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }
    }
}

