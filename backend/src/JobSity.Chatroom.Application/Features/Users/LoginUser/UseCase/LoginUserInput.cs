using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase
{
    public sealed class LoginUserInput : IInput
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginUserInput(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public static LoginUserInput Create(string email, string password)
            => new(email, password);
    }
}