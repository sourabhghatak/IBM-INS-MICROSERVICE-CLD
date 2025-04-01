
using AutoMapper;

namespace Microservice.Claims
{
    public class ClaimsService : IClaimsService
    {
        private readonly IMapper mapper;
        private readonly ICloudantService<Claims> cloudantService;
        private readonly Func<string,IMQService> mQService;
        private readonly IPolicyService policyService;

        public ClaimsService(IMapper mapper, ICloudantService<Claims> cloudantService, Func<string, IMQService> mQService, IPolicyService policyService)
        {
            this.mapper = mapper;
            this.cloudantService = cloudantService;
            this.mQService = mQService;
            this.policyService = policyService;
        }

        public async Task<Claims?> ClaimPolicy(ClaimPolicyDTO? claimPolicyDTO)
        {
            Claims? claims = null;
            if (claimPolicyDTO == null || string.IsNullOrEmpty(claimPolicyDTO?.PolicyNumber))
            {
                return claims;
            }

            //Get details from policy
            Policy? policy = await this.policyService.GetPolicyAsync(claimPolicyDTO.PolicyNumber);

            if (policy == null || string.IsNullOrEmpty(policy.Id))
            {
                return claims;
            }

            //Check whether this policy has been claimed or not
            claims = await this.cloudantService.GetAsync(policy.Id);

            if (claims == null)
            {
                claims = new Claims() { CustomerId = policy.CustomerId, PolicyNumber = policy.Id };
            }

            claims.ClaimDetails ??= new List<ClaimDetails>();

            //Check if same date exist or not for claims
            var existingClaims = claims.ClaimDetails.Any(x => x.ClaimedOn.Equals(claimPolicyDTO.ClaimedOn));

            if (existingClaims)
            {
                return claims;
            }

            claims.ClaimDetails.Add(new ClaimDetails()
            {
                ClaimAmount = claimPolicyDTO.ClaimAmount,
                ClaimedOn = claimPolicyDTO.ClaimedOn
            });

            claims = await this.cloudantService.CreateAsync(claims);

            if (claims == null)
            {
                return claims;
            }

            //publish to IBM MQ
            await this.mQService("publish").PublishMessageToQueueAsync<Claims>(claims);

            return claims;
        }
    }
}
