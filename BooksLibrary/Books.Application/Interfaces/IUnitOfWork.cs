using Books.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IBookRepository Books {  get; }
        IAuthorRepository Authors { get; }
        Task<int> CompleteAsync();
    }
}
