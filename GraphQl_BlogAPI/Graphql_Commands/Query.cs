using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_BlogAPI.Graphql_Commands
{
    public class Query
    {
        public async Task<IEnumerable<User>> GetAllUsers([Service] IDbContextFactory<AppDbContext> dbFactory)
        {
            await using var db = dbFactory.CreateDbContext();
            return db.Users;
        }
        public async Task<IEnumerable<Post>> GetPosts([Service] IDbContextFactory<AppDbContext> dbFactory)
        {
            await using var db = dbFactory.CreateDbContext();
            return db.Posts;
        }
        public async Task<IEnumerable<Comment>> GetComments([Service] IDbContextFactory<AppDbContext> dbFactory)
        {
            await using var db = dbFactory.CreateDbContext();
            return db.Comments;
        }
    }
}
