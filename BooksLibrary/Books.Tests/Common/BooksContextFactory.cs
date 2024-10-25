using Books.Domain;
using Books.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Tests.Common
{
    public class BooksContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static int NoteIdForDelete;
        public static int NoteIdForUpdate;

        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();

            ////    public string ISBN { get; set; }
            ////public string Title { get; set; }
            ////public string Genre { get; set; }
            ////public string? Description { get; set; }
            //////FK
            ////public int AuthorId { get; set; }
            ////public Author Author { get; set; }
            ////public DateTime? StartTimeOfBorrowingBook { get; set; }
            ////public DateTime? EndTimeOfBorrowingBook { get; set; }
            context.Books.AddRange(
                new Book
                {
                    Id = 1,
                    ISBN = "12345678",
                    Title = "First book",
                    Genre = "Genre",
                    Description = "Description",
                    AuthorId = 1,
                    StartTimeOfBorrowingBook = DateTime.Now,
                    EndTimeOfBorrowingBook = DateTime.Now
                },
                new Book
                {
                    Id = 2,
                    ISBN = "87654321",
                    Title = "Second book",
                    Genre = "Genre",
                    Description = "Description",
                    AuthorId = 1,
                    StartTimeOfBorrowingBook = DateTime.Now,
                    EndTimeOfBorrowingBook = DateTime.Now
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
