
using System.Runtime.CompilerServices;

namespace Microservice.Claims
{
    public class PolicyCloudantService : CloudantService<Policy> , ICloudantService<Policy>
    {        
        
        public PolicyCloudantService(IHttpClientFactory httpClientFactory, ICloudantSettings cloudantSettings) : base(httpClientFactory, cloudantSettings)
        {
            this.DatabaseName = "policy-dev";
        }
    }
}
