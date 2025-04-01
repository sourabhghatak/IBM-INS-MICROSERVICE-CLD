using System.Text.Json.Serialization;

namespace Microservice.Claims
{
    public class Policy
    {
        [JsonPropertyName("_id")]
        public string? Id { get; set; }

        public required string CustomerId { get; set; }
    }
}
