namespace ScienceLibrary.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        public bool IsReserved { get; set; }
        public bool IsArchived { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        public BookReserve BookReserve { get; set; }
    }
}
