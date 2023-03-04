using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.Users.GetUser.UseCase
{
    public sealed class GetUserOutput : IOutput
    {
        public Guid UserId { get; set; }
        private GetUserOutput(Guid userId)
        {
            UserId = userId;
        }

        public static GetUserOutput Create(Guid userId) => new(userId);

        public static GetUserOutput Empty => new(Guid.Empty);
    }
}
