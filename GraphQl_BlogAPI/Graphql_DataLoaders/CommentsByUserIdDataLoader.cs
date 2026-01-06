using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_BlogAPI.Graphql_DataLoaders
{
    public class CommentsByUserIdDataLoader : BatchDataLoader<Guid, IEnumerable<Comment>>
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        public CommentsByUserIdDataLoader(IBatchScheduler scheduler, IDbContextFactory<AppDbContext> dbFactory) : base(scheduler)
        {
            _dbFactory = dbFactory;
        }
        protected override async Task<IReadOnlyDictionary<Guid, IEnumerable<Comment>>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();

            var comments = await db.Comments
                .Where(c => keys.Contains(c.UserId))
                .ToListAsync(cancellationToken);

            return comments
                .GroupBy(c => c.UserId)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
        }
    }
}
