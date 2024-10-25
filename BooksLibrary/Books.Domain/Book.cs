using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Books.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre {  get; set; } 
        public string? Description { get; set; }
        //FK
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime? StartTimeOfBorrowingBook { get; set; }
        public DateTime? EndTimeOfBorrowingBook { get; set; }
    }
}
