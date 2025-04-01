using System.Text.Json.Serialization;

namespace Microservice.Claims
{
    public class ClaimsPolicyResponseDTO
    {
        public ClaimsPolicyResponseDTO()
        {
            ClaimDetails = new List<ClaimDetails>();
        }
        public required string PolicyNumber { get; set; }

        [JsonPropertyName("claims")]
        public List<ClaimDetails> ClaimDetails { get; set; }
    }
}
