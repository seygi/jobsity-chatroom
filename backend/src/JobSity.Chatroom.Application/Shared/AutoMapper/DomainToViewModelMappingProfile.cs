using AutoMapper;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.ViewModels;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Chatrooms.ViewModels;

namespace JobSity.Chatroom.Application.Shared.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ChatRoom, ChatRoomViewModel>();
            CreateMap<ChatMessage, ChatMessageViewModel>()
                .ConstructUsing(e => new ChatMessageViewModel { CreatedUserName = e.CreatedUserName, CreatedUserId = e.CreatedUserId, Text = e.Text, CreatedOn = e.CreatedOn });
        }
    }
}
