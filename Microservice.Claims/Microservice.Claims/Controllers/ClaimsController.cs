using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Claims.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimsService claimsService;
        private readonly IMapper mapper;
        public ClaimsController(IClaimsService claimsService, IMapper mapper)
        {
            this.claimsService = claimsService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("claimpolicy")]
        public async Task<IActionResult> ClaimPolicy([FromBody] ClaimPolicyDTO claimPolicyDTO)
        {
            var claims = await this.claimsService.ClaimPolicy(claimPolicyDTO);
            
            var claimPolicyResponseDTO = this.mapper.Map<ClaimsPolicyResponseDTO>(claims);

            return Ok(claimPolicyResponseDTO);
        }
    }
}
