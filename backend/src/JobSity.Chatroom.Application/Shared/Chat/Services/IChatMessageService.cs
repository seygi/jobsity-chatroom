using XpInc.Security.FacialBiometrics.Application.Shared.Users.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Shared.Chat.Services
{
    public interface IChatMessageService
    {
        Task<int> CreateMessageAsync(CreateMessageInputBase input, CancellationToken cancellationToken);
    }
}