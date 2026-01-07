using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GraphQl_BlogAPI.Graphql_DataLoaders
{
    public class ReactionsByUserIdDataLoader : BatchDataLoader<Guid, IEnumerable<Reaction>>
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public ReactionsByUserIdDataLoader([Service] IDbContextFactory<AppDbContext> factory, IBatchScheduler scheduler) : base(scheduler) 
        {
            _factory = factory;
        }
        protected override async Task<IReadOnlyDictionary<Guid, IEnumerable<Reaction>>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            await using var db = await _factory.CreateDbContextAsync();

            var reacts = await db.Reactions
                .Where(r => keys.Contains(r.UserId))
                .ToListAsync(cancellationToken);

            return reacts.GroupBy(r => r.UserId).ToDictionary(g => g.Key, g => g.AsEnumerable());
        }
    }
}