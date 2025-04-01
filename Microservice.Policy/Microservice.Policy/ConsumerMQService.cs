namespace Microservice.Policy
{
    public class ConsumerMQService : MQService
    {
        private IConfiguration configuration;
        public ConsumerMQService(IHttpClientFactory httpClientFactory, IConfiguration configuration,IMQSettings mQSettings) : base(httpClientFactory,mQSettings)
        {
            this.configuration = configuration;
            this.MQUrl = this.configuration["consumeMQTokenUrl"] ?? string.Empty;
        }
    }
}
