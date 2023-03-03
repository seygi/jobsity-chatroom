using JobSity.Chatroom.Application.Shared.Chat.Services;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Application.Shared.Validator;

namespace JobSity.Chatroom.Application.Features.Chatroom.ChatroomCreateMessage.UseCase
{
    internal sealed class CreateMessageUseCase : IUseCase<CreateMessageInput, DefaultOutput>
    {
        private readonly IValidatorService<CreateMessageInput> _validatorService;
        private readonly IChatMessageService _chatMessageService;

        public CreateMessageUseCase(IValidatorService<CreateMessageInput> validatorService, IChatMessageService chatMessageService)
        {
            _validatorService = validatorService;
            _chatMessageService = chatMessageService;
        }

        public async Task<DefaultOutput> ExecuteAsync(CreateMessageInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return DefaultOutput.Default;

            await _chatMessageService.CreateMessageAsync(input, cancellationToken);
            return DefaultOutput.Default;
        }
    }
}
