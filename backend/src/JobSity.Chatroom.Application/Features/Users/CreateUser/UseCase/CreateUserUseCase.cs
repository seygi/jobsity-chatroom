using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Shared.Chat.Services;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Application.Shared.Validator;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    internal sealed class CreateUserUseCase : IUseCase<CreateUserInput, DefaultOutput>
    {
        private readonly IValidatorService<CreateUserInput> _validatorService;
        private readonly IChatMessageService _chatMessageService;

        public CreateUserUseCase(IValidatorService<CreateUserInput> validatorService, IChatMessageService chatMessageService)
        {
            _validatorService = validatorService;
            _chatMessageService = chatMessageService;
        }

        public async Task<DefaultOutput> ExecuteAsync(CreateUserInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return DefaultOutput.Default;

            //await _chatMessageService.CreateMessageAsync(input, cancellationToken);

            return DefaultOutput.Default;
        }
    }
}
