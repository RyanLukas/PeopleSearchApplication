using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
             :base(options)
        {

        }

        public DbSet<People> Users { get; set; }
    }
}
