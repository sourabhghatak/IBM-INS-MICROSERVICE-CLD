namespace Microservice.Policy
{
    public interface IPolicyService
    {
        Task<Policy?> CreatePolicyAsync(Policy policyDTO);
        Task<List<Policy>?> GetPoliciesAsync();

        Task<Policy?> GetPolicyAsync(string id);
    }
}
