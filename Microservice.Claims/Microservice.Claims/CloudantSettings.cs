
using System.Net.Http.Headers;

namespace Microservice.Claims
{
    public class CloudantSettings : ICloudantSettings
    {
        private readonly IConfiguration configuration;
        private string bearerToken;
        private DateTime expireAt;
        private readonly IHttpClientFactory httpClientFactory;
        public CloudantSettings(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            if (this.configuration == null)
            {
                throw new ArgumentNullException();
            }
        }
        public string Host => this.configuration["cloudant:host"] ?? string.Empty;
        public string Database => this.configuration["cloudant:database"] ?? string.Empty;

        public async Task<string> GenerateBearerToken()
        {
            if (string.IsNullOrEmpty(this.bearerToken) || this.expireAt.CompareTo(DateTime.UtcNow) < 0)
            {
                using (var httpClient = this.httpClientFactory.CreateClient())
                {
                    var apiKey = this.configuration["token:apiKey"] ?? string.Empty;
                    var uri = this.configuration["token:apiTokenUrl"] ?? string.Empty;
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), uri))
                    {
                        request.Content = new StringContent($"grant_type=urn:ibm:params:oauth:grant-type:apikey&apikey={apiKey}");
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
                        var httpResponseMessage = await httpClient.SendAsync(request);
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            var response = await httpResponseMessage.Content.ReadAsStringAsync();
                            ApiToken? apiToken = await httpResponseMessage.Content.ReadFromJsonAsync<ApiToken>();
                            if (apiToken != null)
                            {
                                this.expireAt = DateTimeOffset.FromUnixTimeSeconds(apiToken.Expiration).DateTime;
                                this.bearerToken = apiToken.AccessToken ?? string.Empty;
                            }
                        }
                    }
                }
                return this.bearerToken ?? string.Empty;
            }
            else
            {
                return this.bearerToken;
            }
        }
    }
}
