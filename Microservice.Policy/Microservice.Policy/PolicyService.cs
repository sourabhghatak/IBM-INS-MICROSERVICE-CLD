
using AutoMapper;

namespace Microservice.Policy
{
    public class PolicyService : IPolicyService
    {
        private readonly IMapper mapper;
        private readonly ICloudantService<Policy> cloudantService;
        private readonly Func<string, IMQService> mQService;

        public PolicyService(IMapper mapper, ICloudantService<Policy> cloudantService, Func<string, IMQService> mQService)
        {
            this.mapper = mapper;
            this.cloudantService = cloudantService;
            this.mQService = mQService;
        }

        public async Task<Policy?> CreatePolicyAsync(Policy? policy)
        {
            //Generate Policy Id
            if (string.IsNullOrEmpty(policy.Id))
            {
                policy.Id = $"P-{DateTime.Now.ToString("yyyyMMddmmssff")}";
            }            

            //Save policy to Cloudant
            policy = await this.cloudantService.CreateAsync(policy);

            if (string.IsNullOrEmpty(policy?.Revision))
            {
                //Publish to IBm MQ
                await this.mQService("publish").PublishMessageToQueueAsync<Policy>(policy);
            }

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
