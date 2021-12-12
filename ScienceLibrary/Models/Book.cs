using System.Collections.Generic;

namespace ScienceLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<BookCopy> BookCopies { get; set; }
    }
}
