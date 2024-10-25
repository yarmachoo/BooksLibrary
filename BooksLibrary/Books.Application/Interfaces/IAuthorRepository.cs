using Books.Application.Interfaces;
using Books.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces
{
    public interface IAuthorRepository: IRepository<Author>
    {
        Task<IEnumerable<Author>> GetPagedAsync(int pageNum, int pageSize);
    }
}
