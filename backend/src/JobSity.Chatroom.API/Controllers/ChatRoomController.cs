using Hubla.Sales.API.Transport.V1.GetSales;
using JobSity.Chatroom.API.Transport.V1.CreateRoom;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase;
using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase;
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
    }
}
