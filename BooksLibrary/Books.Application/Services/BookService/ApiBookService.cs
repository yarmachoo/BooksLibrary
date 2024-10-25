using Books.Application.Interfaces;
using Books.Domain;
using Books.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services.BookService
{
    public class ApiBookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApiBookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseData<Book>> CreateBookAsync(Book book)
        {
            await _unitOfWork.Books.CreateAsync(book);
            await _unitOfWork.CompleteAsync();
            var returnedBook = await _unitOfWork.Books.GetByIdAsync(book.Id);
            return ResponseData<Book>.Success(returnedBook);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if(book is null)
            {
                throw new KeyNotFoundException("Book is not found.");
            }

            _unitOfWork.Books.Delete(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ResponseData<IEnumerable<Book>>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            return ResponseData<IEnumerable<Book>>.Success(books);
        }

        public async Task<ResponseData<Book>> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if(book is null)
            {
                return ResponseData<Book>.Error("Book is not found.");
            }

            return ResponseData<Book>.Success(book);
        }

        public async Task<ResponseData<BooksListModel<Book>>> GetBooksPagedAsync(string? authorNormalizedName, int pageNum, int pageSize)
        {
            if(!string.IsNullOrEmpty(authorNormalizedName))
            {
                var booksByAuthor = await _unitOfWork.Books.GetBooksByAuthorNamePagedAsync(authorNormalizedName, pageNum, pageSize);
                if(booksByAuthor.Data.Items.Count == 0)
                {
                    return ResponseData<BooksListModel<Book>>.Error("No books found the specified author.");
                }

                return ResponseData<BooksListModel<Book>>.Success(booksByAuthor.Data);
            }

            var books = await _unitOfWork.Books.GetBooksByAuthorNamePagedAsync(null, pageNum, pageSize);
            return ResponseData<BooksListModel<Book>>.Success(books.Data);
        }

        public async Task<ResponseData<Book>> UpdateBookAsync(int id, Book book)
        {
            var currentBook = await _unitOfWork.Books.GetByIdAsync(id);
            if(currentBook is null)
            {
                return ResponseData<Book>.Error($"Book with id {id} is not found");
            }

            currentBook.ISBN = book.ISBN;
            currentBook.Title = book.Title;
            currentBook.Author = book.Author;
            currentBook.Description = book.Description;
            currentBook.Genre = book.Genre;
            currentBook.StartTimeOfBorrowingBook = book.StartTimeOfBorrowingBook;
            currentBook.EndTimeOfBorrowingBook = book.EndTimeOfBorrowingBook;

            _unitOfWork.Books.Update(currentBook);
            await _unitOfWork.CompleteAsync();

            return ResponseData<Book>.Success(currentBook);
        }
    }
}
