using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    public sealed class GetMessagesRoomListOutput : IOutput, IEnumerable<GetMessagesRoomOutput>
    {
        private readonly IList<GetMessagesRoomOutput> _messagesOutput;

        private GetMessagesRoomListOutput(IEnumerable<ChatMessage> messages)
        {
            _messagesOutput = new List<GetMessagesRoomOutput>();
            foreach (var message in messages)
                _messagesOutput.Add(GetMessagesRoomOutput.Create(message.CreatedUserId, message.ChatRoomId, message.CreatedOn, message.CreatedUserName, message.Text));
        }

        public static GetMessagesRoomListOutput Create(IEnumerable<ChatMessage> messages) => new(messages ?? Array.Empty<ChatMessage>());

        public IEnumerator<GetMessagesRoomOutput> GetEnumerator()
        {
            foreach (var message in _messagesOutput)
                yield return message;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static GetMessagesRoomListOutput Success(IEnumerable<ChatMessage> messages) => new(messages);

        public static GetMessagesRoomListOutput Empty => new(Array.Empty<ChatMessage>());
    }
}