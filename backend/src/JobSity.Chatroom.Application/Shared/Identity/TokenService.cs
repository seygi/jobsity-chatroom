using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobSity.Chatroom.Application.Shared.Identity
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<AppJwtSettings> _appJwtSettings;

        public TokenService(IOptions<AppJwtSettings> appJwtSettings)
        {
            _appJwtSettings = appJwtSettings;
        }
        public string GenerateBotToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appJwtSettings.Value.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub", Guid.NewGuid().ToString()),
                    new Claim("email", "bot@jobsity.com"),
                    new Claim("jti", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _appJwtSettings.Value.Issuer,
                Audience = _appJwtSettings.Value.Audience,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
