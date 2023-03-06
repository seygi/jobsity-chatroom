using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Bot.API.Transport.V1.SearchStock;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Bot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ExcludeFromCodeCoverage]
    public class BotStockController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostStockToSearchValue([FromBody] SearchStockRequest request,
            [FromServices] IUseCase<SearchStockInput, SearchStockOutput> useCase,
            CancellationToken cancellationToken)
        {
            useCase.ExecuteAsync(SearchStockInput.Create(request.ChatRoomId, request.Ticker), cancellationToken);
            return CreatedAtAction(default, default);
        }
    }
}
