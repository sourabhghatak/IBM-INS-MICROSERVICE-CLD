using System.Text.Json.Serialization;

namespace Microservice.Policy
{
    public class Policy
    {
        public Policy()
        {
            ClaimDetails = new List<ClaimDetails>();
        }
        [JsonPropertyName("_id")]
        public string? Id { get; set; }

        public required string PolicyName { get; set; }

        public required string PolicyType { get; set; }

        public DateTime PolicyStartDate { get; set; }

        public DateTime PolicyEndDate { get; set; }

        public required string CustomerId { get; set; }

        [JsonPropertyName("_rev")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Revision { get; set; }

        public List<ClaimDetails> ClaimDetails { get; set; }
    }
}
