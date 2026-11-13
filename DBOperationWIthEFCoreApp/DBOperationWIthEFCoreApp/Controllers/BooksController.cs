using DBOperationWIthEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DBOperationWIthEFCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await appDbContext.Books.Select(x => new
            {
                x.Title,
                Author = x.Author.name != null ? x.Author.name : "NA",
                //Landguage = x.LanguageName.Title != null ? x.LanguageName.Title : "NA"
            }
            ).AsNoTracking().ToListAsync();
            return Ok(books);
        }

        /*[HttpGet("EagerLoadingGetBooks")]
        public async Task<IActionResult> EagerLoadingGetBooks()
        {
            var books = await appDbContext.Books.Include(x => x.Author).Include(x=>x.LanguageName).ToListAsync();
            return Ok(books);
        }*/

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

        [HttpPut("UpdateBookWithSingleQuery")]
        public async Task<IActionResult> UpdateBookWithSingleQuery([FromBody] Book model)
        {
            appDbContext.Books.Update(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("UpdateBooks")]
        public async Task<IActionResult> UpdateBooks()
        {
            await appDbContext.Books.ExecuteUpdateAsync(x => x.SetProperty(p => p.Description, p => p.Title + " Updated"));
            return Ok();
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            Book book = new Book {  Id = id };
            appDbContext.Entry(book).State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();
            /*Book book = await appDbContext.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            appDbContext.Books.Remove(book);
            await appDbContext.SaveChangesAsync();*/
            return Ok();
        }

        [HttpDelete("DeleteBooksGreaterThanId/{Id}")]
        public async Task<IActionResult> DeleteBooksGreaterThanId([FromRoute] int Id)
        {
            var output = await appDbContext.Books.Where(x => x.Id > Id).ExecuteDeleteAsync();
            return Ok(output);
            /*List<Book> books = await appDbContext.Books.Where(x => x.Id > Id).ToListAsync();
            appDbContext.Books.RemoveRange(books);
            var output = await appDbContext.SaveChangesAsync();
            return Ok(output);*/
        }

        [HttpGet("ExplicitLoadingOneToOneRelationship")]
        public async Task<IActionResult> ExplicitLoadingOneToOneRelationship()
        {
            var book = appDbContext.Books.FirstOrDefault();
            await appDbContext.Entry(book).Reference(x => x.Author).LoadAsync();
            //await appDbContext.Entry(book).Reference(x => x.LanguageName).LoadAsync();
            return Ok(book);
        }

        [HttpGet("ExplicitLoadingOneToManyRelationship")]
        public async Task<IActionResult> ExplicitLoadingOneToManyRelationship()
        {
            var languages = await appDbContext.Languages.ToListAsync();
            foreach (var language in languages)
            {
                await appDbContext.Entry(language).Collection(x => x.Books).LoadAsync();
            }
            
            return Ok(languages);
        }

        [HttpGet("LazyLoading")]
        public async Task<IActionResult> LazyLoading()
        {
            var book = await appDbContext.Books.FirstOrDefaultAsync();
            var author = book.Author;
            return Ok();
        }


        [HttpGet("UseSqlQuery")]
        public async Task<IActionResult> UseSqlQuery()
        {
            int bookId = 18;
            var book = await  appDbContext.Books.FromSql($"select * from Books where Id = {bookId}").ToListAsync();
            return Ok(book);
        }

        [HttpGet("StoreProcedure")]
        public async Task<IActionResult> GetBooksUsingStoreProcedure()
        {
            var books = await appDbContext.Books.FromSql($"EXEC SP_GetBooks").ToListAsync();
            return Ok(books);
        }

        [HttpGet("SP_GETBookById")]
        public async Task<IActionResult> SP_GETBookById()
        {
            var parameter = new SqlParameter("@BookId", 5);
            var book = await appDbContext.Books.FromSql($"EXEC SP_GETBookById {parameter}").ToListAsync();
            return Ok(book);
        }

        [HttpGet("SQLOnDBGetBooks")]
        public async Task<IActionResult> SQLOnDBGet()
        {
            var books = await appDbContext.Database.SqlQuery<Book>($"select * from Books").ToListAsync();
            return Ok(books);
        }
        [HttpGet("SQLOnDBGetId")]
        public async Task<IActionResult> SQLOnDBGetId()
        {
            var books = await appDbContext.Database.SqlQuery<int>($"select Id from Books").ToListAsync();
            return Ok(books);
        }

        [HttpGet("UpdateSQLOnDBBooks")]
        public async Task<IActionResult> UpdateSQLOnDBBooks()
        {
            await appDbContext.Database.ExecuteSqlAsync($"Update Books set Description = {"updated using EF"} where Title = {"Rushi's book"}");
                return Ok();
        }
    }
}
