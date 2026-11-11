using DBOperationWIthEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBOperationWIthEFCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            appDbContext.Books.Add(book);
            await appDbContext.SaveChangesAsync();
            return Ok(book); 
        }

        [HttpPost("BulkAdd")]
        public async Task<IActionResult> AddBulkBooks([FromBody] List<Book> books)
        {
            appDbContext.Books.AddRangeAsync(books);
            await appDbContext.SaveChangesAsync();
            return Ok(books);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] Book book)
        {
            var book1 = appDbContext.Books.FirstOrDefault(x => x.Id == id);
            if (book1 == null)
            {
                return NotFound();
            }
            if (book.Author == null)
            {
                return BadRequest("Author is required");
            }
            if (book1.Author == null)
            {
                book1.Author = new Author();
            }
            book1.Author.name = book.Author.name;
            book1.Author.Email = book.Author.Email;
            book1.Title = book.Title;

            await appDbContext.SaveChangesAsync();

            return Ok(book1);
               
        }
    }
}
