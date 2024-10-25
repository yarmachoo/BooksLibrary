using Books.Application.Interfaces;
using Books.Domain;
using Books.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appContext;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public UnitOfWork(AppDbContext appDbContext,
            IBookRepository bookRepository,
            IAuthorRepository authorRepository)
        {
            _appContext = appDbContext;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        public IBookRepository Books =>
            _bookRepository;
        public IAuthorRepository Authors =>
            _authorRepository;
        public async Task<int> CompleteAsync()
        {
            return await _appContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appContext.Dispose();
        }
    }
}
