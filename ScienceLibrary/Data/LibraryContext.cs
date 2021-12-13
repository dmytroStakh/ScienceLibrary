using Microsoft.EntityFrameworkCore;
using ScienceLibrary.Models;

namespace ScienceLibrary.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<BookReserve> BookReserves { get; set; }
    }
}
