using JobSity.Chatroom.API.Configurations;
using JobSity.Chatroom.API.Filters;
using JobSity.Chatroom.API.Hubs;
using JobSity.Chatroom.Application;
using MediatR;
using NetDevPack.Identity.User;

namespace JobSity.Chatroom.API
{
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
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication(builder.Configuration);

            builder.Services.AddSignalR();

            builder.Services.AddAspNetUserConfiguration();

            builder.Services.AddSwaggerConfiguration();

            // Adding MediatR for Domain Events and Notifications
            builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

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
                c.AllowCredentials();
                c.WithOrigins("http://localhost:4200");
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