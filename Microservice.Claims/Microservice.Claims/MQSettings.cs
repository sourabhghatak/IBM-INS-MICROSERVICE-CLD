namespace Microservice.Claims
{
    public class MQSettings : IMQSettings
    {
        private readonly IConfiguration configuration;
        public MQSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string UserName => this.configuration["mqconfig:username"] ?? string.Empty;
        public string ApiKey =>this.configuration["mqconfig:apiKey"] ?? string.Empty;
        public string MQUrl => this.configuration["mqconfig:tokenUrl"] ?? string.Empty;
    }
}
