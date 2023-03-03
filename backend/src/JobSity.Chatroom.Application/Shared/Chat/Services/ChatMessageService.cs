using JobSity.Chatroom.Application.Shared.Chat.Repositories;
using NetDevPack.Messaging;
using XpInc.Security.FacialBiometrics.Application.Shared.Users.UseCases.Inputs;

namespace JobSity.Chatroom.Application.Shared.Chat.Services
{
    public class ChatMessageService : CommandHandler, IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        public ChatMessageService(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public Task<int> CreateMessageAsync(CreateMessageInputBase input, CancellationToken cancellationToken)
        {
            var chatMessage = new ChatMessage(input.CreatedUserId, input.ChatRoomId, input.CreatedUserName, input.Text);

            //chatMessage.AddDomainEvent(new ChatMessageCreatedEvent(chatMessage.Id, chatMessage.CreatedUserId, chatMessage.Text, chatMessage.CreatedOn));

            _chatMessageRepository.Add(chatMessage);

            return _chatMessageRepository.SaveChangesAsync();
        }

        //private RegisterModel CreateUnsuccessfulEnroll(CreateMessageInputBase input, EnrollResponse enrollResponse, string imagePath)
        //{
        //    _notificationContext.Create(HttpStatusCode.OK, RegisterResult.Registered(false, enrollResponse.ScanResultBlob, enrollResponse.WasProcessed));

        //    return RegisterModel.CreateUnsuccessfully(input.Document, _productContext.Id, input.Metadata, input.AntiFraudId, input.Brand, imagePath);
        //}

        //private async Task<(OperationResult, User)> AddOrUpdateUserAsync(User user, RegisterModel model)
        //{
        //    var date = DateTime.Now;

        //    if (user.Exist())
        //    {
        //        user.Update(model.ProductId, model.UserStatus, model.StatusReason, string.Empty, model.Metadata, model.ImagePath, model.Brand);

        //        return (await _userRepository.UpdateAsync(user), user);
        //    }

        //    user = User.Create(model.Document, model.ProductId, model.UserStatus, model.StatusReason, string.Empty, date, date, model.Metadata, model.ImagePath, model.Brand);

        //    return (await _userRepository.SaveAsync(user), user);
        //}
    }
}