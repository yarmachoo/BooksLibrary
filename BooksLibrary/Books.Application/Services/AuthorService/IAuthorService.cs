using Books.Domain;
using Books.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<ResponseData<Author>> GetAuthorByIdAsync(int id);
        Task<ResponseData<IEnumerable<Author>>> GetAllAuthorsAsync();
        Task<ResponseData<BooksListModel<Author>>> GetAuthorPagedAsync(int pageNum, int pageSize);
        Task<ResponseData<Author>> CreateAuthorAsync(Author author);
        Task<ResponseData<Author>> UpdateAuthorAsync(int id, Author author);
        Task DeleteAuthorAsync(int id);
    }
}
