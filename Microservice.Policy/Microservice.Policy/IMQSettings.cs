namespace Microservice.Policy
{
    public interface IMQSettings
    {
        public string UserName { get; }
        public string ApiKey { get; }
        public string MQUrl { get; }
    }
}
