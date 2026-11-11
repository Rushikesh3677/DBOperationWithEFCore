using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DBOperationWIthEFCoreApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyType>().HasData(
                new CurrencyType() { id = 1, Title = "INR", Descrition = "Indian" },
                new CurrencyType() { id = 2, Title = "USD", Descrition = "America" },
                new CurrencyType() { id = 3, Title = "Euro", Descrition = "Europe" },
                new CurrencyType() { id = 4, Title = "Dinar", Descrition = "Dubai" }
                );
            modelBuilder.Entity<Language>().HasData(
               new Language() { id = 1, Title = "Marathi", Description = "Marathi" },
               new Language() { id = 2, Title = "English", Description = "English" },
               new Language() { id = 3, Title = "Tamil", Description = "Tamil" },
               new Language() { id = 4, Title = "Sanskrit", Description = "Sanskrit" }
               );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<CurrencyType> Currencies { get; set; }
        public DbSet <BookPrice> Prices { get; set; }
    }
}
