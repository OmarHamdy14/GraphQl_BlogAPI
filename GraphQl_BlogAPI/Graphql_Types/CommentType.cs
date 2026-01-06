using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GraphQl_BlogAPI.Graphql_Types
{
    public class CommentType : ObjectType<Comment>
    {
        protected override void Configure(IObjectTypeDescriptor<Comment> descriptor)
        {
            descriptor.Field(c => c.Id);
            descriptor.Field(c => c.Content);
            descriptor.Field(c => c.UserId);
            descriptor.Field(c => c.PostId);

            descriptor.Field(c => c.User).ResolveWith<Resolvers>(r => r.GetUserAsync(default!, default!, default!));
            descriptor.Field(c => c.Post).ResolveWith<Resolvers>(r => r.GetPostAsync(default!, default!, default!));

            base.Configure(descriptor);
        }
        private class Resolvers
        {
            public async Task<User?> GetUserAsync([Parent] Comment cmnt, [Service] IDbContextFactory<AppDbContext> _factory, CancellationToken ct)
            {
                await using var dbContext = _factory.CreateDbContext();
                return await dbContext.Users.FindAsync(new object[] { cmnt.UserId }, ct);
            }
            public async Task<Post?> GetPostAsync([Parent] Comment cmnt, [Service] IDbContextFactory<AppDbContext> _factory, CancellationToken ct)
            {
                await using var db = _factory.CreateDbContext();
                return await db.Posts.FindAsync(new object[] { cmnt.PostId }, ct);
            }
        }
    }

}
