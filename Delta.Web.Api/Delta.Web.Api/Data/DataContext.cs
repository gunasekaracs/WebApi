using Microsoft.EntityFrameworkCore;

namespace Delta.Web.Api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser>? Users { get; set; } 
    }
}
