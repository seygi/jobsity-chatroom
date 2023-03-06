using JobSity.Chatroom.Application;
using JobSity.Chatroom.Bot.API.Configurations;
using JobSity.Chatroom.Bot.API.Filters;
using NetDevPack.Identity.User;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Bot.API
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<NotificationFilter>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication(builder.Configuration);

            builder.Services.AddAspNetUserConfiguration();

            builder.Services.AddSwaggerConfiguration();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
                //c.AllowCredentials();
            });

            app.UseAuthConfiguration();

            //app.UseAuthorization();

            app.MapControllers();

            app.UseSwaggerSetup();

            app.Run();
        }
    }
}