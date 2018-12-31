using Microsoft.EntityFrameworkCore;
using PeopleApp.Data;

namespace Test.Data
{
    public class PeopleDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=People.sqlite");
        }

        public DbSet<User> Users { get; set; }
        
    }
}