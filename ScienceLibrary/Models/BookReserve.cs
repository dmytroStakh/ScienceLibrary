using System;
using System.ComponentModel.DataAnnotations;

namespace ScienceLibrary.Models
{
    public class BookReserve
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }
    }
}
