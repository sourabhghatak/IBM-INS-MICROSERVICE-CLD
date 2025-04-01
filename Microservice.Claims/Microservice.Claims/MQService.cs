
using System.Buffers.Text;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Microservice.Claims
{
    public class MQService : IMQService
    {
        private readonly IMQSettings mQSettings;
        public string MQUrl { get; set; }

        private readonly IHttpClientFactory httpClientFactory;
        public MQService(IHttpClientFactory httpClientFactory,IMQSettings mQSettings)
        {
            this.httpClientFactory = httpClientFactory;
            this.mQSettings = mQSettings;
        }
        public async Task PublishMessageToQueueAsync<T>(T value)
        {
            using (var client = this.CreateHttpClient())
            {
                var httpContent = new StringContent(JsonSerializer.Serialize(value));
                var httpResponseMessage = await client.PostAsync(this.MQUrl, httpContent);
            }
        }

        public async Task<T> PullMessageFromQueueAsync<T>()
        {
            T? messageReponse = default;
            using (var client = this.CreateHttpClient())
            {
                var response = await client.DeleteAsync(this.MQUrl);
                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(message))
                    {
                        messageReponse = JsonSerializer.Deserialize<T>(message);
                    }                    
                }
            }
            return messageReponse;
        }

        private HttpClient CreateHttpClient()
        {
            var client = this.httpClientFactory.CreateClient();
            var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(this.mQSettings.UserName + ":" + this.mQSettings.ApiKey));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            client.DefaultRequestHeaders.Add("ibm-mq-rest-csrf-token", "token");

            return client;
        }
    }
}
