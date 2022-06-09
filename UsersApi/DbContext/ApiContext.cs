using Microsoft.EntityFrameworkCore;
using UsersApi.BusinessLayer;

namespace UsersApi.DataAccessLayer
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserMap());
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("DemiconUsers");
        }
        
    }
}
