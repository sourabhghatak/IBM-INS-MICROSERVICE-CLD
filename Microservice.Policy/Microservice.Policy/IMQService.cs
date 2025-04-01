namespace Microservice.Policy
{
    public interface IMQService
    {
        public Task PublishMessageToQueueAsync<T>(T value);
        public Task<T> PullMessageFromQueueAsync<T>();
    }
}
