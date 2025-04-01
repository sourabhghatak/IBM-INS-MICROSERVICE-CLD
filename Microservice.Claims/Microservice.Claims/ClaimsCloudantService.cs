
using System.Runtime.CompilerServices;

namespace Microservice.Claims
{
    public class ClaimsCloudantService : CloudantService<Claims> , ICloudantService<Claims>
    {        
        
        public ClaimsCloudantService(IHttpClientFactory httpClientFactory, ICloudantSettings cloudantSettings) : base(httpClientFactory, cloudantSettings)
        {
            this.DatabaseName = "claims-dev";
        }
    }
}
