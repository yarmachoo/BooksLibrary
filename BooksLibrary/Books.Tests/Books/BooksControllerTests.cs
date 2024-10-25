using Books.Application.Services.BookService;
using Books.Domain.DTO;
using Books.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Books.Tests.Books
{
    public class BookServiceTests : TestCommandBase
    {
        private readonly ApiBookService _bookService;

        public BookServiceTests()
        {
            // Инициализация BookService с тестовым контекстом
            _bookService = new ApiBookService(BooksContextFactory.Create());
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnAllBooks()
        {
            // Act
            var result = await _bookService.GetAllBooksAsync();

            // Assert
            Assert.True(result.IsSuccessfull);
            Assert.Equal(3, result.Data.Count());
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnCorrectBook()
        {
            // Act
            var result = await _bookService.GetBookByIdAsync(1);

            // Assert
            Assert.True(result.IsSuccessfull);
            Assert.Equal("First book", result.Data.Title);
        }

        [Fact]
        public async Task CreateBookAsync_ShouldAddBook()
        {
            // Arrange
            var newBookDto = new CreateBookDto
            {
                ISBN = "11111111",
                Title = "New Book",
                Genre = "New Genre",
                Description = "New Description",
                AuthorId = 1, // Assuming author with Id = 1 exists
                StartTimeOfBorrowingBook = DateTime.Now,
                EndTimeOfBorrowingBook = DateTime.Now.AddDays(7)
            };

            // Act
            var result = await _bookService.CreateBookAsync(newBookDto);

            // Assert
            Assert.True(result.IsSuccessfull);
            Assert.Equal("New Book", result.Data.Title);
            Assert.Equal(4, (await _bookService.GetAllBooksAsync()).Data.Count());
        }
    }
}
