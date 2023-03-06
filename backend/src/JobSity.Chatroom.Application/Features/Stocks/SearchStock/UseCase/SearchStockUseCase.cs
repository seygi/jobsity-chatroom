using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Services;
using JobSity.Chatroom.Application.Shared.Stocks.Services;
using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.Stocks.SearchStock.UseCase
{
    internal sealed class SearchStockUseCase : IUseCase<SearchStockInput, SearchStockOutput>
    {
        private readonly IStockService _stockService;
        private readonly IChatMessageService _chatMessageService;

        public SearchStockUseCase(IStockService stockService, IChatMessageService chatMessageService)
        {
            _stockService = stockService;
            _chatMessageService = chatMessageService;
        }

        public async Task<SearchStockOutput> ExecuteAsync(SearchStockInput input, CancellationToken cancellationToken)
        {
            var stockValue = await _stockService.GetStockAsync(input.Ticker, cancellationToken);

            var message = new ChatMessage(Guid.Empty,
                input.ChatRoomId,
                "chatbot@jobsity.com",
                $"{input.Ticker.ToUpper()} quote is ${stockValue.Close} per share");

            await _chatMessageService.EnqueueMessageToInsertAsync(message, cancellationToken);

            return SearchStockOutput.Empty;
        }
    }
}
