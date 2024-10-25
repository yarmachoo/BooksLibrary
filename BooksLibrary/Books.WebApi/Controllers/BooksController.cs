using AutoMapper;
using Books.Application.Services.BookService;
using Books.Domain;
using Books.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper; // добавляем IMapper

        public BooksController(IBookService bookService, IMapper mapper) // инжектируем IMapper
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        // GET: api/books
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAllBooksAsync();
            if (result.IsSuccessfull)
            {
                var booksDto = _mapper.Map<IEnumerable<BookDto>>(result.Data); // маппинг списка книг в список DTO
                return Ok(booksDto);
            }
            return BadRequest(result.ErrorMessage);
        }

        // GET: api/books/paged?pageNum=1&pageSize=5&authorNormalizedName=optional
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedBooks([FromQuery] string? authorNormalizedName, int pageNum = 1, int pageSize = 5)
        {
            var result = await _bookService.GetBooksPagedAsync(authorNormalizedName, pageNum, pageSize);
            if (result.IsSuccessfull)
            {
                var pagedBooksDto = _mapper.Map<IEnumerable<BookDto>>(result.Data.Items);
                return Ok(pagedBooksDto);
            }
            return BadRequest(result.ErrorMessage);
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            if (result.IsSuccessfull)
            {
                var bookDto = _mapper.Map<BookDto>(result.Data); // маппинг Book -> BookDto
                return Ok(bookDto);
            }
            return NotFound(result.ErrorMessage);
        }

        // POST: api/books
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = _mapper.Map<Book>(createBookDto); // маппинг CreateBookDto -> Book

            var result = await _bookService.CreateBookAsync(book);
            if (result.IsSuccessfull)
            {
                var createdBookDto = _mapper.Map<BookDto>(result.Data); // маппинг Book -> BookDto для ответа
                return CreatedAtAction(nameof(GetBookById), new { id = result.Data.Id }, createdBookDto);
            }
            return BadRequest(result.ErrorMessage);
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updateBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = _mapper.Map<Book>(updateBookDto); // маппинг UpdateBookDto -> Book
            book.Id = id; // присваиваем id книги для обновления

            var result = await _bookService.UpdateBookAsync(id, book);
            if (result.IsSuccessfull)
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return Ok();
        }
    }
}
