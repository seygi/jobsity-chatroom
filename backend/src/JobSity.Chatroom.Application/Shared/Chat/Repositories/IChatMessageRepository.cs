﻿namespace JobSity.Chatroom.Application.Shared.Chat.Repositories
{
    public interface IChatMessageRepository
    {
        Task<ChatMessage> GetById(Guid id);
        Task<IEnumerable<ChatMessage>> GetAll();
        Task<IEnumerable<ChatMessage>> GetAllByChatRoomId(Guid chatRoomId);
        void Add(ChatMessage message);
        Task<int> SaveChangesAsync();
    }
}