using Books.Application.Interfaces;
using Books.Domain;
using Books.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task CreateAsync(Book item)
        {
            await _appDbContext.Books.AddAsync(item);
        }

        public void Delete(Book item)
        {

            _appDbContext.Books.Remove(item);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var allBooks = _appDbContext.Books.Include(b => b.Author).ToList();

            return allBooks;
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int authorId)
        {
            return await _appDbContext.Books.
                        Where(b=>b.AuthorId==authorId).
                        ToListAsync();
        }

        public async Task<ResponseData<BooksListModel<Book>>> GetBooksByAuthorNamePagedAsync(string? authorNormalizedName, int pageNum, int pageSize)
        {
            try
            {
                var allBooks = _appDbContext.Books.Include(b=>b.Author).ToList();

                //Если передано имя, то фильтруем по автору
                if (!string.IsNullOrWhiteSpace(authorNormalizedName))
                {
                    authorNormalizedName = authorNormalizedName.Trim().ToLower();
                    allBooks = allBooks.Where(b => b.Author.FirstName.ToLower().Contains(authorNormalizedName)
                                                || b.Author.LastName.ToLower().Contains(authorNormalizedName)).ToList();
    
                }

                var totalItems = allBooks.Count;

                var books = allBooks
                    .OrderBy(b => b.Id)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var result = new BooksListModel<Book>(
                    totalItems, pageNum, pageSize)
                {
                    Items = books
                };

                return ResponseData<BooksListModel<Book>>.Success(result);
            }
            catch(Exception ex)
            {
                return ResponseData<BooksListModel<Book>>.Error(ex.Message);
            }
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var allBooks = _appDbContext.Books.Include(b => b.Author).ToList();
            return allBooks.First(b=>b.Id==id);
        }

        public void Update(Book item)
        {
            _appDbContext.Books.Update(item);
        }
    }
}
