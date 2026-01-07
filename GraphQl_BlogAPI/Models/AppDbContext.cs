using Microsoft.EntityFrameworkCore;

namespace GraphQl_BlogAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions op) : base(op)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

    }
}
