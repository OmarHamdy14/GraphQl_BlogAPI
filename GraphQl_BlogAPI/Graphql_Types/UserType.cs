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

            descriptor.Field("Posts").ResolveWith<Resolvers>(r => r.GetPostsAsync(default!, default!, default)).UseSorting().UseFiltering(); ;
        }

        private class Resolvers
        {
            public Task<IEnumerable<Post>> GetPostsAsync([Parent] User user, PostsByUserIdDataLoader dataLoader,CancellationToken ct)
                        => dataLoader.LoadAsync(user.Id, ct);
        }
    }
}
