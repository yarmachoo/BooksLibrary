using Books.Application.Interfaces;
using Books.Domain;
using Books.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId);
        Task<ResponseData<BooksListModel<Book>>> GetBooksByAuthorNamePagedAsync(
            string?  authorNormalizedName, 
            int pageNum,
            int pageSize);
    }
}
