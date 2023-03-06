using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    public sealed class SearchStockInput : IInput
    {
        public Guid ChatRoomId { get; set; }
        public string Ticker { get; set; }

        public SearchStockInput(Guid chatRoomId, string ticker)
        {
            ChatRoomId = chatRoomId;
            Ticker = ticker;
        }

        public static SearchStockInput Create(Guid chatRoomId, string ticker)
            => new(chatRoomId, ticker);
    }
}