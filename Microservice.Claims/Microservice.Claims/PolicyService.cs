
using AutoMapper;

namespace Microservice.Claims
{
    public class PolicyService : IPolicyService
    {
        private readonly ICloudantService<Policy> cloudantService;

        public PolicyService(IMapper mapper, ICloudantService<Policy> cloudantService)
        {
            this.cloudantService = cloudantService;
        }

        public async Task<Policy?> CreateOrUpdatePolicyAsync(Policy policy)
        {
            //Save policy to Cloudant
            policy = await this.cloudantService.CreateAsync(policy);

            //return
            return policy;
        }

        public async Task<List<Policy>?> GetPoliciesAsync()
        {
            List<Policy> policies = new List<Policy>();
            CloudantResponse? cloudantResponse = await this.cloudantService.GetAllAsync();
            if (cloudantResponse == null || (cloudantResponse.rows != null && cloudantResponse.rows.Count == 0))
            {
                return null;
            }
            foreach (var row in cloudantResponse?.rows)
            {
                //Get each Policy Details
                Policy? policy = await this.cloudantService.GetAsync(row?.id ?? string.Empty);
                if (policy != null)
                {
                    policies.Add(policy);
                }
            }
            return policies;
        }

        public async Task<Policy?> GetPolicyAsync(string id)
        {
            Policy? policy = await this.cloudantService.GetAsync(id);

            return policy;
        }
    }
}
