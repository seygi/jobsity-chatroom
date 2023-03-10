using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Data.Postgres;
using Microsoft.EntityFrameworkCore;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        protected readonly DataContext Db;
        protected readonly DbSet<ChatRoom> DbSet;

        public ChatRoomRepository(DataContext context)
        {
            Db = context;
            DbSet = Db.Set<ChatRoom>();
        }

        public void Add(ChatRoom customer)
        {
            DbSet.Add(customer);
        }
        public async Task<IEnumerable<ChatRoom>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return Db.SaveChangesAsync();
        }
    }
}