using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase
{
    public sealed class CreateUserInput : IInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public CreateUserInput(string email, string password, string confirmPassword)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public static CreateUserInput Create(string email, string password, string confirmPassword)
            => new(email, password, confirmPassword);
    }
}