using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.DTO
{
    public class CreateBookDto
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string? Description { get; set; }
        //FK
        public int AuthorId { get; set; }
        public DateTime? StartTimeOfBorrowingBook { get; set; }
        public DateTime? EndTimeOfBorrowingBook { get; set; }

    }
}
