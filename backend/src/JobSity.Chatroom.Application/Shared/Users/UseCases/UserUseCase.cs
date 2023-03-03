using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;

namespace JobSity.Chatroom.Application.Shared.Users.UseCases
{
    public abstract class UserUseCase
    {
        private readonly IOptions<AppJwtSettings> _appJwtSettings;
        public readonly UserManager<IdentityUser> _userManager;

        protected UserUseCase(IOptions<AppJwtSettings> appJwtSettings, UserManager<IdentityUser> userManager)
        {
            _appJwtSettings = appJwtSettings;
            _userManager = userManager;
        }

        protected string GetFullJwt(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings.Value)
                .WithEmail(email)
                .WithJwtClaims()
                .WithUserClaims()
                .WithUserRoles()
                .BuildToken();
        }
    }
}
