using Books.Application.Services.AuthorService;
using Books.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        //GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await _authorService.GetAllAuthorsAsync();
            if(result.IsSuccessfull)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        //GET: api/authors/paged?pageNum=1&pageSize=5
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAuthors(int pageNum = 1, int pageSize = 1)
        {
            var result = await _authorService.GetAuthorPagedAsync(pageNum, pageSize);
            if(result.IsSuccessfull)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        //GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var result = await _authorService.GetAuthorByIdAsync(id);
            if(result.IsSuccessfull)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        //POST: api/authors
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorService.CreateAuthorAsync(author);
            if(result.IsSuccessfull)
            {
                return CreatedAtAction(
                    nameof(GetAuthorById), 
                    new { id = result.Data.Id }, 
                    result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }

        //PUT: api/authors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if(id!=author.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorService.UpdateAuthorAsync(id, author);
            if(result.IsSuccessfull)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        //DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            //TODO: Add realisation to errors
            await _authorService.DeleteAuthorAsync(id);
            return Ok();
        }
    }
}
