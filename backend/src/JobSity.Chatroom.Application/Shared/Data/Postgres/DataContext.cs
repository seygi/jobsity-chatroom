using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Data.Postgres.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NetDevPack.Messaging;

namespace JobSity.Chatroom.Application.Shared.Data.Postgres
{
    public sealed class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            Database.EnsureCreated();
        }

        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: ver os modelos que aparecerao aqui
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfiguration(new ChatMessageMap());
            modelBuilder.ApplyConfiguration(new ChatRoomMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}