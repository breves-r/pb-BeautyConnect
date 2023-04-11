using RedeSocial.Domain.Interfaces;
using RedeSocial.Domain.Services;
using RedeSocial.Infra.Context;
using RedeSocial.Infra.Repositories;
using System.Text.Json.Serialization;

namespace RedeSocial.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(c =>
            {
                c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ProfileService, ProfileService>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<PostService, PostService>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<FriendshipService, FriendshipService>();
            builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();
            builder.Services.AddScoped<ProfileDetailsService, ProfileDetailsService>();
            builder.Services.AddScoped<IProfileDetailsRepository, ProfileDetailsRepository>();
            builder.Services.AddScoped<CommentService,  CommentService>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();

            builder.Services.AddDbContext<RedeSocialDbContext>();

            var app = builder.Build();

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