using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_BlogAPI.Graphql_DataLoaders
{
    public class CommentsByPostIdDataLoader : BatchDataLoader<int, IEnumerable<Comment>>
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public CommentsByPostIdDataLoader([Service] IDbContextFactory<AppDbContext> factory, IBatchScheduler scheduler) : base(scheduler)
        {
            _factory = factory;
        }
        protected override async Task<IReadOnlyDictionary<int, IEnumerable<Comment>>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using var db = await _factory.CreateDbContextAsync();

            var comments = await db.Comments
                .Where(c => keys.Contains(c.PostId))
                .ToListAsync(cancellationToken);

            return comments.GroupBy(c => c.PostId).ToDictionary(g => g.Key, g => g.AsEnumerable());
        }
    }
}
