using GraphQl_BlogAPI.Eunms;
using GraphQl_BlogAPI.Graphql_Types;
using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl_BlogAPI.Graphql_Commands
{
    public class Mutation
    {
        public async Task<User> CreateUser(string name, string email, [Service] IDbContextFactory<AppDbContext> dbFactory)
        {
            var user = new User { Name = name, Email = email };

            await using var db = dbFactory.CreateDbContext();
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }
        public async Task<Post> CreatePost(Guid userId, string content, [Service] IDbContextFactory<AppDbContext> dbFactory)
        {
            var post = new Post { UserId = userId, Content = content };

            await using var db = dbFactory.CreateDbContext();
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return post;
        }
        public async Task<Comment> CreateComment(Guid userId, string content, int PostId, [Service] IDbContextFactory<AppDbContext> dbFactory)
        {
            var comment = new Comment { UserId = userId, Content = content, PostId=PostId };

            await using var db = dbFactory.CreateDbContext();
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
            return comment;
        }
        public async Task<Reaction> CreateReaction(Guid userId, ReactionKind type, int PostId, [Service] IDbContextFactory<AppDbContext> dbFactory)
        {
            var react = new Reaction { UserId = userId, Type = type, PostId=PostId };

            await using var db = dbFactory.CreateDbContext();
            db.Reactions.Add(react);
            await db.SaveChangesAsync();
            return react;
        }

    }
}
