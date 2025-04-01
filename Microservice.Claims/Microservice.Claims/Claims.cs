using System.Text.Json.Serialization;

namespace Microservice.Claims
{
    public class Claims
    {
        public Claims()
        {
            ClaimDetails = new List<ClaimDetails>();
        }
        [JsonPropertyName("_id")]
        public required string PolicyNumber { get; set; }
        public required string CustomerId { get; set; }
        [JsonPropertyName("claims")]
        public List<ClaimDetails> ClaimDetails { get; set; }

        [JsonPropertyName("_rev")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Revision { get; set; }
    }

    public class ClaimDetails
    {
        public decimal ClaimAmount { get; set; }
        public DateTime ClaimedOn { get; set; }
    }
}
