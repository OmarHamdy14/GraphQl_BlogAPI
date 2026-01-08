using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace GraphQl_BlogAPI.Graphql_DataLoaders
{
    public class ReactionsByPostIdDataLoader : BatchDataLoader<int, IEnumerable<Reaction>>
    {
        private readonly IDbContextFactory<AppDbContext> _factory;
        public ReactionsByPostIdDataLoader([Service] IDbContextFactory<AppDbContext> factory, IBatchScheduler scheduler) : base(scheduler) 
        {
            _factory = factory;
        }
        protected override async Task<IReadOnlyDictionary<int, IEnumerable<Reaction>>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using var db = _factory.CreateDbContext();

            var reacts = await db.Reactions.Where(p => keys.Contains(p.Id)).ToListAsync(cancellationToken);

            return reacts.GroupBy(r => r.PostId).ToDictionary(g => g.Key, g => g.AsEnumerable());
        }
    }
}
