using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    public sealed class SearchStockOutput : IOutput
    {
        public bool Success { get; }

        private SearchStockOutput(bool success)
        {
            Success = success;
        }

        public static SearchStockOutput Create(bool success) => new(success);

        public static SearchStockOutput Empty => new(false);
    }
}
