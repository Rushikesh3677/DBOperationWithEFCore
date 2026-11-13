using DBOperationWIthEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBOperationWIthEFCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private AppDbContext appDbContext;
        public CurrencyController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet("")]
        public IActionResult GetAllCurrencies()
        {
            //var result = this.appDbContext.Currencies.ToList();
            var result = (from currencies in appDbContext.Currencies
                          select currencies).ToList();
            return Ok(result);
        }

        [HttpGet("AllLanguages")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var result = await this.appDbContext.Languages.AsNoTracking().ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var result = await appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetCurrencyByName([FromRoute] string name)
        {
            var result = await appDbContext.Currencies.Where(x => x.Title == name).FirstOrDefaultAsync();
            return Ok(result);
        }


        [HttpGet("desc/{id}")]
        public async Task<IActionResult> GetCurrencyBynameAndDescr([FromRoute] int id, [FromQuery] string? desc)
        {
            var result = await appDbContext.Currencies.FirstOrDefaultAsync(x => x.id == id && (string.IsNullOrEmpty(desc) || x.Descrition == desc));
            return Ok(result);
        }

        [HttpGet("GetAll/{name}")]
        public async Task<IActionResult> GetAllFilCurrencies([FromRoute] string name)
        {
            var result = await appDbContext.Currencies.Where(x => x.Title == name).ToListAsync();
            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetCurrenciesbyIdAsync([FromBody] List<int> ids)
        {
            var result = await appDbContext.Currencies.Where(x => ids.Contains(x.id))
                .Select(x => new CurrencyType()
                {
                    id = x.id,
                    Title = x.Title
                }).ToListAsync();
            return Ok(result);
        }
    }

}
