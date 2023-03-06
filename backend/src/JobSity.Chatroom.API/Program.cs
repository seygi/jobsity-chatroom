using JobSity.Chatroom.API.Configurations;
using JobSity.Chatroom.API.Filters;
using JobSity.Chatroom.API.Hubs;
using JobSity.Chatroom.Application;
using JobSity.Chatroom.Application.Shared.Data.Postgres;
using NetDevPack.Identity.User;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.API
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
           
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<NotificationFilter>();
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new Configurations.DateTimeConverter());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication(builder.Configuration);

            builder.Services.AddSignalR();

            builder.Services.AddAspNetUserConfiguration();

            builder.Services.AddSwaggerConfiguration();
            
            var app = builder.Build();

            //for ensure create database
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<DataContext>();
            }

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
                c.AllowCredentials();
                c.WithOrigins("http://localhost:4200", "http://localhost:8080");
            });

            app.UseAuthConfiguration();
            
            //app.UseAuthorization();

            app.MapControllers();

            app.MapHub<ChatRoomHub>("/chat-room-hub");

            app.UseSwaggerSetup();

            app.Run();
        }
    }
}