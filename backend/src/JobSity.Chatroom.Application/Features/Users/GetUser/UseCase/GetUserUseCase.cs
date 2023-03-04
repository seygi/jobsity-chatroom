using JobSity.Chatroom.Application.Shared.UseCase;
using NetDevPack.Identity.User;

namespace JobSity.Chatroom.Application.Features.Users.GetUser.UseCase
{
    internal sealed class GetUserUseCase : IUseCase<DefaultInput, GetUserOutput>
    {
        private readonly IAspNetUser _aspNetUser;

        public GetUserUseCase(IAspNetUser aspNetUser)
        {
            _aspNetUser = aspNetUser;
        }

        public async Task<GetUserOutput> ExecuteAsync(DefaultInput input, CancellationToken cancellationToken)
        {
            return GetUserOutput.Create(_aspNetUser.GetUserId());
        }
    }
}
