using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_BlogAPI.Graphql_Types
{
    public class ReactionType : ObjectType<Reaction>
    {
        protected override void Configure(IObjectTypeDescriptor<Reaction> descriptor)
        {
            descriptor.Field(r => r.Id);
            descriptor.Field(r => r.Type);
            descriptor.Field(r => r.UserId);
            descriptor.Field(r => r.PostId);

            descriptor.Field(r => r.User).ResolveWith<Resolvers>(r => r.GetUserAsync(default!,default!,default!));
            descriptor.Field(r => r.Post).ResolveWith<Resolvers>(r => r.GetPostAsync(default!,default!,default!));
        }
        private class Resolvers
        {
            public async Task<User?> GetUserAsync([Parent] Reaction rect, [Service] IDbContextFactory<AppDbContext> factory, CancellationToken ct)
            {
                await using var db = factory.CreateDbContext();
                return await db.Users.FindAsync(new object[] { rect.UserId }, ct);
            }
            public async Task<Post?> GetPostAsync([Parent] Reaction rect, [Service] IDbContextFactory<AppDbContext> factory, CancellationToken ct)
            {
                await using var db = factory.CreateDbContext();
                return await db.Posts.FindAsync(new object[] { rect.PostId }, ct);
            }
        }
    }
}
