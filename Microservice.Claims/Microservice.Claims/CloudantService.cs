
using System.Net.Http.Headers;

namespace Microservice.Claims
{
    public class CloudantService<T> : ICloudantService<T> where T : class
    {
        private IHttpClientFactory httpClientFactory;
        private readonly ICloudantSettings cloudantSettings;
        public string DatabaseName { get; set; }
        public CloudantService(IHttpClientFactory httpClientFactory, ICloudantSettings cloudantSettings)
        {
            this.httpClientFactory = httpClientFactory;
            this.cloudantSettings = cloudantSettings;
        }
        public async Task<T?> CreateAsync(T item)
        {
            var httpClient = await this.CreateHttpClient();
            var httpResponse = await httpClient.PostAsJsonAsync<T>(this.DatabaseName, item);

            if (httpResponse != null && httpResponse.IsSuccessStatusCode)
            {
                var responseJson = await httpResponse.Content.ReadAsStringAsync();
            }

            return item;
        }

        public Task DeleteAsync(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<CloudantResponse?> GetAllAsync()
        {
            CloudantResponse? cloudantResponse = null;
            using (var httpClient = await this.CreateHttpClient())
            {
                var httpResponseMessage = await httpClient.GetAsync($"{this.DatabaseName}/_all_docs");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    cloudantResponse = await httpResponseMessage.Content.ReadFromJsonAsync<CloudantResponse>();
                }
            }
            return cloudantResponse;
        }

        public Task<T?> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }

        private async Task<HttpClient> CreateHttpClient()
        {
            var token = await this.cloudantSettings.GenerateBearerToken();
            //var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(this.cloudantSettings.UserName + ":" + this.cloudantSettings.Password));

            HttpClient client = this.httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://" + this.cloudantSettings.Host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public async Task<T?> GetAsync(string id)
        {
            T? response = null;
            using (var httpClient = await this.CreateHttpClient())
            {
                var httpResponseMessage = await httpClient.GetAsync($"{this.DatabaseName}/{id}");
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    response = await httpResponseMessage.Content.ReadFromJsonAsync<T>();
                }
            }
            return response;
        }
    }
}
