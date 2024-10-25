using Books.Domain;
using Books.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services.BookService
{
    public interface IBookService
    {
        Task<ResponseData<Book>> GetBookByIdAsync(int id);
        Task<ResponseData<IEnumerable<Book>>> GetAllBooksAsync();
        Task<ResponseData<BooksListModel<Book>>> GetBooksPagedAsync(string? authorNormalizedName, int pageNum, int pageSize);
        Task<ResponseData<Book>> CreateBookAsync(Book book);
        Task<ResponseData<Book>> UpdateBookAsync(int id, Book book);
        Task DeleteBookAsync(int id);

    }
}
