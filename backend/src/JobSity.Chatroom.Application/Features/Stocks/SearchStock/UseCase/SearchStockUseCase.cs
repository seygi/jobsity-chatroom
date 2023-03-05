using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Services;
using JobSity.Chatroom.Application.Shared.Stocks.Services;
using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.Stocks.SearchStock.UseCase
{
    internal sealed class SearchStockUseCase : IUseCase<DefaultInput, SearchStockOutput>
    {
        private readonly IStockService _stockService;
        private readonly IChatMessageService _chatMessageService;

        public SearchStockUseCase(IStockService stockService, IChatMessageService chatMessageService)
        {
            _stockService = stockService;
            _chatMessageService = chatMessageService;
        }

        public async Task<SearchStockOutput> ExecuteAsync(DefaultInput input, CancellationToken cancellationToken)
        {
            await _stockService.SubscribeAsync(async (stk) =>
            {
                var stockValue = await _stockService.GetStockAsync(stk.Ticker, cancellationToken);

                var message = new ChatMessage(Guid.Empty, 
                    stk.ChatRoomId, 
                    "chatbot@jobsity.com", 
                    $"{stk.Ticker.ToUpper()} quote is ${stockValue.Close} per share");

                await _chatMessageService.EnqueueMessageToInsertAsync(message, cancellationToken);
            });

            return SearchStockOutput.Empty;
        }
    }
}
