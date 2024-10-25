using Books.Application.Interfaces;
using Books.Domain;
using Books.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services.AuthorService
{
    public class ApiAuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApiAuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseData<Author>> CreateAuthorAsync(Author author)
        {
            await _unitOfWork.Authors.CreateAsync(author); 
            await _unitOfWork.CompleteAsync();
            return ResponseData<Author>.Success(author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if(author != null)
            {
                _unitOfWork.Authors.Delete(author);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<ResponseData<IEnumerable<Author>>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return ResponseData<IEnumerable<Author>>.Success(authors); 
        }

        public async Task<ResponseData<Author>> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if(author is null)
            {
                return ResponseData<Author>.Error($"Author with id {id} is not found");
            }

            return ResponseData<Author>.Success(author);     
        }

        public async Task<ResponseData<BooksListModel<Author>>> GetAuthorPagedAsync(int pageNum, int pageSize)
        {
            var allAuthors = await _unitOfWork.Authors.GetAllAsync();
            var totalCount = allAuthors.Count();

            var authors = await _unitOfWork.Authors.GetPagedAsync(pageNum, pageSize);

            var result = new BooksListModel<Author>(totalCount, pageNum, pageSize)
            {
                Items = authors.ToList()
            };
            return ResponseData<BooksListModel<Author>>.Success(result);
        }

        public async Task<ResponseData<Author>> UpdateAuthorAsync(int id, Author author)
        {
            var currentAuthor = await _unitOfWork.Authors.GetByIdAsync(id);
            if(currentAuthor is null)
            {
                return ResponseData<Author>.Error($"Author with id {id} is not found");
            }

            currentAuthor.FirstName = author.FirstName;
            currentAuthor.LastName = author.LastName;
            currentAuthor.Country = author.Country;
            currentAuthor.Birthday = author.Birthday;

            _unitOfWork.Authors.Update(currentAuthor);
            await _unitOfWork.CompleteAsync();

            return ResponseData<Author>.Success(currentAuthor);
        }
    }
}
