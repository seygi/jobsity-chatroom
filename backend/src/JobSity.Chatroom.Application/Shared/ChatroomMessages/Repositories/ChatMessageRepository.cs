using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.Data.Postgre;
using Microsoft.EntityFrameworkCore;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        protected readonly DataContext Db;
        protected readonly DbSet<ChatMessage> DbSet;

        public ChatMessageRepository(DataContext context)
        {
            Db = context;
            DbSet = Db.Set<ChatMessage>();
        }
        public void Add(ChatMessage customer)
        {
            DbSet.Add(customer);
        }

        public async Task<IEnumerable<ChatMessage>> GetTop50ByChatRoomId(Guid chatRoomId)
        {
            return await DbSet
                    .Where(p => p.ChatRoomId == chatRoomId)
                    .OrderByDescending(x=>x.CreatedOn)
                    .Take(50)
                    .ToListAsync();
        }
        public Task<int> SaveChangesAsync()
        {
            return Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }

}