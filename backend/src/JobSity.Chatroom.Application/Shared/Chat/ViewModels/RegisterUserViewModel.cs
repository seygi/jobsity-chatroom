using System.ComponentModel.DataAnnotations;

namespace JobSity.Chatroom.Application.Shared.Chat.ViewModels
{
    public class RegisterUserViewModel
    {
        public string Email { get; set; }
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
