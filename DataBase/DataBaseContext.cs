using Dating_App_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dating_App_API
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }



    }
}
