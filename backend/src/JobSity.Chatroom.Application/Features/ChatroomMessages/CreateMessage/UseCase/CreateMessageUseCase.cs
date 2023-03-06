using JobSity.Chatroom.Application.Shared.ChatroomMessages.Services;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.Stocks.Entities;
using JobSity.Chatroom.Application.Shared.Stocks.Services;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Application.Shared.Validator;
using System.Net;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    internal sealed class CreateMessageUseCase : IUseCase<CreateMessageInput, CreateMessageOutput>
    {
        private readonly IValidatorService<CreateMessageInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly IChatMessageService _chatMessageService;
        private readonly IStockService _stockService;

        public CreateMessageUseCase(IValidatorService<CreateMessageInput> validatorService, INotificationContext notificationContext, IChatMessageService chatMessageService, IStockService stockService)
        {
            _validatorService = validatorService;
            _notificationContext = notificationContext;
            _chatMessageService = chatMessageService;
            _stockService = stockService;
        }

        public async Task<CreateMessageOutput> ExecuteAsync(CreateMessageInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return CreateMessageOutput.Empty;


            if (input.Text.ToLower().StartsWith("/stock="))
            {
                var stockTicker = input.Text.ToLower().Split("/stock=").Last();
                await _stockService.EnqueueStockToSearchAsync(Stock.Create(stockTicker, input.ChatRoomId), cancellationToken);
            }
            else
            {
                var rowsAffected = await _chatMessageService.CreateMessageAsync(input, cancellationToken);

                if (rowsAffected <= 0)
                {
                    _notificationContext.Create(HttpStatusCode.InternalServerError, "Error on create message, please try again.");
                    return CreateMessageOutput.Empty;
                }
            }

            return CreateMessageOutput.Create(true);
        }
    }
}
