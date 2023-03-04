using JobSity.Chatroom.API.Transport.V1.GetMessagesRoom;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSity.Chatroom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatroomMessagesController : ControllerBase
    {
        [HttpGet("{chatRoomId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] Guid chatRoomId, 
            [FromServices] IUseCase<GetMessagesRoomInput, GetMessagesRoomListOutput> useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(GetMessagesRoomInput.Create(chatRoomId), cancellationToken);

            return Ok(GetMessagesRoomResponse.Create(result));

        }
    }
}
