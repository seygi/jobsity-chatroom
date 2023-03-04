using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;
using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.GetMessagesRoom
{
    public class GetMessagesRoomResponse
    {

        [JsonPropertyName("createdUserId")]
        public Guid CreatedUserId { get; set; }
        [JsonPropertyName("chatRoomId")]
        public Guid ChatRoomId { get; set; }
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("createdUserName")]
        public string CreatedUserName { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }

        public GetMessagesRoomResponse(Guid createdUserId, Guid chatRoomId, DateTime createdOn, string createdUserName, string text)
        {
            CreatedUserId = createdUserId;
            ChatRoomId = chatRoomId;
            CreatedOn = createdOn;
            CreatedUserName = createdUserName;
            Text = text;
        }

        public static IList<GetMessagesRoomResponse> Create(GetMessagesRoomListOutput outputUseCase) =>
            outputUseCase.Select(lnq => new GetMessagesRoomResponse(lnq.CreatedUserId, lnq.ChatRoomId, lnq.CreatedOn, lnq.CreatedUserName, lnq.Text))
               .ToList();
    }
}
