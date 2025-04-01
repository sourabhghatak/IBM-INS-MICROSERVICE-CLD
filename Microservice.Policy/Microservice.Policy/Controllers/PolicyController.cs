using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Microservice.Policy.Controllers
{
    [Route("api/policy")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private IPolicyService policyService;
        private readonly IMapper mapper;
        public PolicyController(IPolicyService policyService,IMapper mapper)
        {
            this.policyService = policyService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        [Produces("application/json", Type = typeof(Policy))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Policy))]
        public async Task<IActionResult> CreatePolicyAsync([FromBody] PolicyDTO policyRequestDTO)
        {
            //Map Policy DTO to Policy
            Policy? policy = this.mapper.Map<Policy>(policyRequestDTO);

            policy = await this.policyService.CreatePolicyAsync(policy);

            return Ok(policy);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json", Type = typeof(List<Policy>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Policy))]
        public async Task<IActionResult> GetAllPoliciesAsync()
        {
            var policy = await this.policyService.GetPoliciesAsync();

            return Ok(policy);
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json", Type = typeof(Policy))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Policy))]
        public async Task<IActionResult> GetPolicyAsync([FromRoute]string id)
        {
            var policy = await this.policyService.GetPolicyAsync(id);

            return Ok(policy);
        }
    }
}
