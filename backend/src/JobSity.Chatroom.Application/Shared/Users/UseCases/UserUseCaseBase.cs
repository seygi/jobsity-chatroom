using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Shared.Users.UseCases
{
    [ExcludeFromCodeCoverage]
    public abstract class UserUseCaseBase
    {
        private readonly IOptions<AppJwtSettings> _appJwtSettings;
        public readonly UserManager<IdentityUser> _userManager;

        protected UserUseCaseBase(IOptions<AppJwtSettings> appJwtSettings, UserManager<IdentityUser> userManager)
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
