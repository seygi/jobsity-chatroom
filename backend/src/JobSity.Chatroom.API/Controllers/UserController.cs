using JobSity.Chatroom.API.Transport.V1.CreateUser;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Model;
using NetDevPack.Identity.User;

namespace JobSity.Chatroom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly AppJwtSettings _appJwtSettings;
        //private readonly IAspNetUser _user;

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] RegisterUser newUser,
            [FromServices]IUseCase<CreateUserInput, CreateUserOutput> useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(CreateUserInput.Create(newUser.Email, newUser.Password, newUser.ConfirmPassword), cancellationToken);

            if (result.Success)
                return Ok(CreateUserResponse.Create(result));
            
            return Problem();
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login(LoginUser loginUser)
        //{
        //    if (!ModelState.IsValid) return CustomResponse(ModelState);

        //    var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        //    if (result.Succeeded)
        //    {
        //        var fullJwt = GetFullJwt(loginUser.Email);
        //        return CustomResponse(fullJwt);
        //    }

        //    if (result.IsLockedOut)
        //    {
        //        AddError("This user is temporarily blocked");
        //        return CustomResponse();
        //    }

        //    AddError("Incorrect user or password");
        //    return CustomResponse();
        //}

        //[Authorize]
        //[HttpGet("get-user-id")]
        //public async Task<IActionResult> GetUserId()
        //{
        //    return CustomResponse(_user.GetUserId().ToString());
        //}
    }
}
