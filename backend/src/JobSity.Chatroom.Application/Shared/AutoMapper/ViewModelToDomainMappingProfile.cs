using AutoMapper;

namespace JobSity.Chatroom.Application.Shared.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //CreateMap<ChatRoomViewModel, CreateChatRoomCommand>()
            //    .ConstructUsing(c => new CreateChatRoomCommand(c.Name, c.CreatedByUserId));

            //CreateMap<ChatRoomViewModel, UpdateChatRoomCommand>()
            //    .ConstructUsing(c => new UpdateChatRoomCommand(c.Id, c.Name));

            //CreateMap<ChatMessageViewModel, CreateChatMessageCommand>()
            //    .ConstructUsing(c => new CreateChatMessageCommand(c.CreatedByUserId, c.ChatRoomId, c.CreatedByUserName, c.Text));
        }
    }
}
