
namespace Microservice.Policy
{
    public class PublishMQService : MQService , IMQService
    {
        private IConfiguration configuration;
        public PublishMQService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMQSettings mQSettings) : base(httpClientFactory, mQSettings)
        {
            this.configuration = configuration;
            this.MQUrl = this.configuration["publishMQTokenUrl"] ?? string.Empty;
        }
    }
}
