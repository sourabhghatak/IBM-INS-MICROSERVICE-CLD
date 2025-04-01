namespace Microservice.Claims
{
    public class ClaimPolicyDTO
    {
        public required string PolicyNumber { get; set; }

        public decimal ClaimAmount { get; set; }

        public DateTime ClaimedOn { get; set; }
    }
}
