using Books.Application.Interfaces;
using Books.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Persistence.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;
        public AuthorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task CreateAsync(Author item)
        {
            await _appDbContext.Authors.AddAsync(item);
        }

        public void Delete(Author item)
        {
            _appDbContext.Authors.Remove(item);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _appDbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _appDbContext.Authors.FindAsync(id);
        }

        public async Task<IEnumerable<Author>> GetPagedAsync(int pageNum, int pageSize)
        {
            return await _appDbContext.Authors
                .OrderBy(a=>a.LastName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Update(Author item)
        {
            _appDbContext.Authors.Update(item);
        }
    }
}
