using Microsoft.EntityFrameworkCore;

namespace Auth
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<AuthModel>? Auths { get; set; }
    }
}
