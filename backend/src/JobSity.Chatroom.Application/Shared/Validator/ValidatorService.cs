using FluentValidation;
using JobSity.Chatroom.Application.Shared.Notifications;
using System.Net;

namespace JobSity.Chatroom.Application.Shared.Validator
{
    public class ValidatorService<TInput> : IValidatorService<TInput>
        where TInput : class
    {
        private readonly IValidator<TInput> _validator;
        private readonly INotificationContext _notificationContext;

        public ValidatorService(IValidator<TInput> validator, INotificationContext notificationContext) 
            => (_validator, _notificationContext) = (validator, notificationContext);

        public bool ValidateAndNotifyIfError(TInput input)
        {
            var notificationErrors = NotificationErrors.Empty;
            var result = _validator.Validate(input);

            if (result.IsValid)
                return result.IsValid;

            foreach (var error in result.Errors)
            {
                notificationErrors.Add(error.PropertyName, error.ErrorMessage);
            }

            _notificationContext.Create(HttpStatusCode.BadRequest, notificationErrors);

            return result.IsValid;
        }

        public bool Validate(TInput input, out NotificationErrors notificationErrors)
        {
            notificationErrors = NotificationErrors.Empty;
            var result = _validator.Validate(input);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    notificationErrors.Add(error.PropertyName, error.ErrorMessage);
                }
                _notificationContext.Create(HttpStatusCode.BadRequest, notificationErrors);
            }

            return result.IsValid;
        }
    }
}