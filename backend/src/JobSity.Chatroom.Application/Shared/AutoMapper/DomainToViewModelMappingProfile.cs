using AutoMapper;
using JobSity.Chatroom.Application.Shared.Chat;
using JobSity.Chatroom.Application.Shared.Chat.ViewModels;
using JobSityNETChallenge.Application.ViewModels;

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
