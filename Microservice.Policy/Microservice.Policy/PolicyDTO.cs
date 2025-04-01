namespace Microservice.Policy
{
    public class PolicyDTO
    {        
        public required string PolicyName { get; set; }

        public required string PolicyType { get; set; }

        public DateTime PolicyStartDate { get; set; }

        public DateTime PolicyEndDate { get; set; }

        public required string CustomerId { get; set; }
    }
}
