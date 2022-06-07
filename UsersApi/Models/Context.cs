using Microsoft.EntityFrameworkCore;

namespace UsersApi.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You don't actually ever need to call this
            optionsBuilder.UseInMemoryDatabase("Countries");
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
