using FluentValidation;

namespace JobSity.Chatroom.Application.Shared.Validator.Validators
{
    internal static class GuidNotEmptyValidator
    {
        private const string EmptyPropertyErrorMessage = "The property {PropertyName} cannot be null or empty.";

        public static IRuleBuilderOptions<T, Guid> MustBeValidGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .Must(guid => IsValidGuid(guid))
                .WithMessage("The property {PropertyName} must be valid GUID.");
        }

        public static bool IsValidGuid(Guid guid)
        {
            return guid != Guid.Empty;
        }
    }

}
