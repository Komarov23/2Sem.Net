using Microsoft.EntityFrameworkCore;
using WebApplication2.Entity;

namespace WebApplication2.DB
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options): base (options) { }

        public DbSet<Book> Books { get; set; }
    }
}
