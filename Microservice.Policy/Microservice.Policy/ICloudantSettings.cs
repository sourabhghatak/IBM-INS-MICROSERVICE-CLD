namespace Microservice.Policy
{
    public interface ICloudantSettings
    {
        public string Host { get; }
        public string Database { get; }
        public Task<string> GenerateBearerToken();
    }
}
