using JobSity.Chatroom.Application.Shared.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobSity.Chatroom.Application.Shared.Data.Postgre.Mappings
{
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
            
            //CreatedUserName

            builder.Property(c => c.Text)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(p => p.ChatRoom).WithMany(p => p.ChatMessages).HasForeignKey(p => p.ChatRoomId).HasPrincipalKey(p => p.Id);
        }
    }
}
