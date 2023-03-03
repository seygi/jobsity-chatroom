namespace JobSity.Chatroom.Application.Shared.UseCase
{
    public interface IUseCase<TInput, TOutput>
        where TInput : IInput
        where TOutput : IOutput
    {
        Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationToken);
    }
}