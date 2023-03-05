using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Shared.Data.Postgres.Mappings
{
    [ExcludeFromCodeCoverage]
    public class ChatMessageMap : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.CreatedUserId)
                .HasColumnName("CreatedUserId");

            builder.Property(c => c.ChatRoomId)
                .HasColumnName("ChatRoomId");

            builder.Property(c => c.CreatedOn)
                .IsRequired();

            builder.Property(c => c.Text)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(p => p.ChatRoom).WithMany(p => p.ChatMessages).HasForeignKey(p => p.ChatRoomId).HasPrincipalKey(p => p.Id);
        }
    }
}
