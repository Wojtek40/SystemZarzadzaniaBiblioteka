using Microsoft.EntityFrameworkCore;

namespace WpfApp1
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=books.db");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
