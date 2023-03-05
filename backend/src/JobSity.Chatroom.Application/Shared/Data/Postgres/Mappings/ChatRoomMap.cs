using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Shared.Data.Postgres.Mappings
{
    [ExcludeFromCodeCoverage]
    public class ChatRoomMap : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.CreatedOn)
                .IsRequired();

            builder.Property(c => c.CreatedUserId)
                .HasColumnName("CreatedUserId");
        }
    }
}
