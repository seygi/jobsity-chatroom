using JobSity.Chatroom.Application.Shared.Data.Postgre;
using Microsoft.EntityFrameworkCore;

namespace JobSity.Chatroom.Application.Shared.Chat.Repositories
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

        public async Task<ChatMessage> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<ChatMessage>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(ChatMessage customer)
        {
            DbSet.Add(customer);
        }

        public async Task<IEnumerable<ChatMessage>> GetAllByChatRoomId(Guid chatRoomId)
        {
            return await DbSet.Where(p => p.ChatRoomId == chatRoomId).ToListAsync();
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