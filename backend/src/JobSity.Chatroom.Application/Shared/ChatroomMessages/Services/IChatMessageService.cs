﻿using JobSity.Chatroom.Application.Shared.ChatroomMessages.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.Services
{
    public interface IChatMessageService
    {
        Task<int> CreateMessageAsync(CreateMessageInputBase input, CancellationToken cancellationToken);
    }
}