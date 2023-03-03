using JobSity.Chatroom.Application.Shared.Notifications;

namespace JobSity.Chatroom.Application.Shared.Validator
{
    public interface IValidatorService<in TInput>
        where TInput : class
    {
        bool ValidateAndNotifyIfError(TInput input);

        bool Validate(TInput input, out NotificationErrors notificationErrors);
    }
}