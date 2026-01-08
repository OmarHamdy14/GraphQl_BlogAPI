using GraphQl_BlogAPI.Graphql_Commands;
using GraphQl_BlogAPI.Graphql_DataLoaders;
using GraphQl_BlogAPI.Graphql_Types;
using GraphQl_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GraphQl_BlogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddGraphQLServer()
                            .AddType<UserType>()
                            .AddType<PostType>()
                            .AddType<CommentType>()
                            .AddQueryType<Graphql_Commands.Query>()
                            .AddMutationType<Mutation>()
                            .AddFiltering()
                            .AddSorting()
                            .AddDataLoader<PostsByUserIdDataLoader>()
                            .AddDataLoader<CommentsByUserIdDataLoader>()
                            .AddDataLoader<CommentsByPostIdDataLoader>()
                            .AddDataLoader<ReactionsByUserIdDataLoader>()
                            .AddDataLoader<ReactionsByPostIdDataLoader>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.MapGraphQL("/graphql");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}