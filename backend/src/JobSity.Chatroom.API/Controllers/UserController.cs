using JobSity.Chatroom.API.Transport.V1.CreateUser;
using JobSity.Chatroom.API.Transport.V1.GetUser;
using JobSity.Chatroom.API.Transport.V1.LoginUser;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Features.Users.GetUser.UseCase;
using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Model;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.API.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUser newUser,
            [FromServices] IUseCase<CreateUserInput, CreateUserOutput> useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(CreateUserInput.Create(newUser.Email, newUser.Password, newUser.ConfirmPassword), cancellationToken);

            if (result.Success)
                return Ok(CreateUserResponse.Create(result));

            return Problem();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser,
            [FromServices] IUseCase<LoginUserInput, LoginUserOutput> useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(LoginUserInput.Create(loginUser.Email, loginUser.Password), cancellationToken);

            if (result.Success)
                return Ok(LoginUserResponse.Create(result));

            return Problem();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserId([FromServices] IUseCase<DefaultInput, GetUserOutput> useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(DefaultInput.Default, cancellationToken);

            return Ok(GetUserResponse.Create(result));
        }
    }
}
