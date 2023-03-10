using JobSity.Chatroom.API.Transport.V1.CreateRoom;
using JobSity.Chatroom.API.Transport.V1.GetAllRooms;
using JobSity.Chatroom.API.Transport.V1.GetMessagesRoom;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase;
using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.API.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatRoomController : ControllerBase
    {
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IUseCase<DefaultInput, GetAllRoomsListOutput> useCase,
            CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(DefaultInput.Default, cancellationToken);

            return Ok(GetAllRoomsResponse.Create(result));

        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request,
            [FromServices] IUseCase<CreateRoomInput, CreateRoomOutput> useCase,
            CancellationToken cancellationToken)
        {
            await useCase.ExecuteAsync(CreateRoomInput.Create(request.Name), cancellationToken);
            return Ok();
        }

        [HttpGet("{chatRoomId}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMessages([FromRoute] Guid chatRoomId, [FromQuery] string? lastMessageTime,
            [FromServices] IUseCase<GetMessagesRoomInput, GetMessagesRoomListOutput> useCase,
            CancellationToken cancellationToken)
        {
            DateTime? date = null;
            if (lastMessageTime != null)
                date = DateTime.ParseExact(lastMessageTime, "yyyy-MM-dd'T'HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);

            var result = await useCase.ExecuteAsync(GetMessagesRoomInput.Create(chatRoomId, date), cancellationToken);

            return Ok(GetMessagesRoomResponse.Create(result));

        }
    }
}
