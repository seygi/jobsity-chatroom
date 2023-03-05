using JobSity.Chatroom.Application.Shared.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NetDevPack.Identity.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Identity
{
    public class TokenServiceTest
    {
        private readonly AppJwtSettings _appJwtSettings;
        private readonly ITokenService _tokenService;

        public TokenServiceTest()
        {
            _appJwtSettings = new AppJwtSettings
            {
                SecretKey = "super_secret_key",
                Issuer = "jobSity",
                Audience = "jobSity",
                Expiration = 2
            };

            var appJwtSettingsMock = new Mock<IOptions<AppJwtSettings>>();
            appJwtSettingsMock.Setup(x => x.Value).Returns(_appJwtSettings);

            _tokenService = new TokenService(appJwtSettingsMock.Object);
        }

        [Fact]
        public void GenerateToken_ValidUser_ReturnsTokenString()
        {
            // Act
            var tokenString = _tokenService.GenerateBotToken();

            // Assert
            Assert.NotNull(tokenString);
            Assert.NotEmpty(tokenString);
        }

        [Fact]
        public void GenerateToken_ShouldReturn_ValidToken()
        {
            // Act
            var token = _tokenService.GenerateBotToken();

            // Assert
            Assert.NotNull(token);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appJwtSettings.SecretKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _appJwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _appJwtSettings.Audience,
                ValidateLifetime = true
            };
            var claimsPrincipal = jwtTokenHandler.ValidateToken(token, validationParameters, out var securityToken);
            Assert.NotNull(claimsPrincipal);
            Assert.IsType<JwtSecurityToken>(securityToken);
        }
    }
}
