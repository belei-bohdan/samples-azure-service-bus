namespace QueueSenderAPI.Services
{
    public interface ISenderService
    {
        Task SendMessageAsync<T>(T message);
    }
}
