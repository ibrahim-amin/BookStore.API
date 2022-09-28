using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBookAsync();
            return Ok(books);
        }


        [HttpGet("GetBookById/{Id}")]
        public async Task<IActionResult> GetBookById(int Id)
        {
            var book = await _bookRepository.GetBookById(Id);
            if (book is null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] BookModel bookmodel)
        {

            var id=  await _bookRepository.AddBook(bookmodel);
           return CreatedAtAction(nameof(GetBookById),new { id = id, controller = "books" }, id);

        }

        [HttpPut("UpdateBook/{Id}")]
        public async Task<IActionResult> UpdateBook(int Id, BookModel bookmodel)
        {
            await _bookRepository.UpdateBook(Id, bookmodel);
            return Ok();
        } 
        
        
        [HttpPatch("UpdatePatchBook/{Id}")]
        public async Task<IActionResult> PatchBook(int Id, JsonPatchDocument bookmodel)
        {
            await _bookRepository.UpdatePatchBookAsync(Id, bookmodel);
            return Ok();
        }

        [HttpDelete("DeleteBook/{Id}")]
        public async Task<IActionResult> DeleteBookAsync(int Id)
        {
            await _bookRepository.DeleteBookAsync(Id);
            return Ok();
        }
    }
}
