using JobSity.Chatroom.API.Transport.V1.CreateRoom;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSity.Chatroom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatRoomController : ControllerBase
    {

        [HttpPost()]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request,
            [FromServices] IUseCase<CreateRoomInput, CreateRoomOutput> useCase,
            CancellationToken cancellationToken)
        {
            await useCase.ExecuteAsync(CreateRoomInput.Create(request.Name), cancellationToken);
            return Ok();
        }
    }
}
