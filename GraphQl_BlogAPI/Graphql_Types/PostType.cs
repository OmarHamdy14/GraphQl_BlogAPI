using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_BlogAPI.Graphql_Types
{
    public class PostType : ObjectType<Post>
    {
        protected override void Configure(IObjectTypeDescriptor<Post> descriptor)
        {
            descriptor.Field(p => p.Id);
            descriptor.Field(p => p.Content);
            descriptor.Field(p => p.UserId);

            descriptor.Field(p => p.User).ResolveWith<Resolvers>(r => r.GetUserAsync(default!, default!, default))
                .UseSorting().UseFiltering();

        }
        private class Resolvers
        {
            public async Task<User?> GetUserAsync([Parent] Post post, [Service] IDbContextFactory<AppDbContext> dbFactory,CancellationToken ct)
            {
                await using var db = dbFactory.CreateDbContext();
                return await db.Users.FindAsync(new object[] { post.UserId }, ct);
            }
        }
    }
}
