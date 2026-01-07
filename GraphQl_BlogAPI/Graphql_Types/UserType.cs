using GraphQl_BlogAPI.Graphql_DataLoaders;
using GraphQl_BlogAPI.Models;

namespace GraphQl_BlogAPI.Graphql_Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(u => u.Id);
            descriptor.Field(u => u.Name);
            descriptor.Field(u => u.Email);

            descriptor.Field("Posts").ResolveWith<Resolvers>(r => r.GetPostsAsync(default!, default!, default)).UseSorting().UseFiltering();
            descriptor.Field("Comments").ResolveWith<Resolvers>(r => r.GetCommnetsAsync(default!, default!, default)).UseSorting().UseFiltering();
            descriptor.Field("Reactions").ResolveWith<Resolvers>(r => r.GetReactionsAsync(default!, default!, default)).UseSorting().UseFiltering();
        }

        private class Resolvers
        {
            public async Task<IEnumerable<Post>> GetPostsAsync([Parent] User user, PostsByUserIdDataLoader dataLoader, CancellationToken ct)
                        => await dataLoader.LoadAsync(user.Id, ct);

            public async Task<IEnumerable<Comment>> GetCommnetsAsync([Parent] User user, CommentsByUserIdDataLoader dataLoader, CancellationToken ct)
                        => await dataLoader.LoadAsync(user.Id, ct);

            public async Task<IEnumerable<Reaction>> GetReactionsAsync([Parent] User user, ReactionsByUserIdDataLoader dataLoader, CancellationToken ct)
                        => await dataLoader.
        }
    }
}
