using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_BlogAPI.Graphql_DataLoaders
{
    public class PostsByUserIdDataLoader : BatchDataLoader<Guid, IEnumerable<Post>>
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        public PostsByUserIdDataLoader(IBatchScheduler scheduler, IDbContextFactory<AppDbContext> dbFactory) : base(scheduler)
        {
            _dbFactory = dbFactory;
        }
        protected override async Task<IReadOnlyDictionary<Guid, IEnumerable<Post>>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            var posts = await db.Posts
                .Where(p => keys.Contains(p.UserId))
                .ToListAsync(cancellationToken);

            return posts
                .GroupBy(p => p.UserId)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
        }
    }
}
