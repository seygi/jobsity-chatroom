using XpInc.Security.FacialBiometrics.Application.Shared.Users.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Shared.Chat.Services
{
    public interface IChatMessageService
    {
        Task CreateMessageAsync(CreateMessageInputBase input, CancellationToken cancellationToken);
    }
}