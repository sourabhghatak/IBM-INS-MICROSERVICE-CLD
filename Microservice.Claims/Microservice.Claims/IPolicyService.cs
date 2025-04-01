namespace Microservice.Claims
{
    public interface IPolicyService
    {
        Task<Policy?> CreateOrUpdatePolicyAsync(Policy policy);

        Task<List<Policy>?> GetPoliciesAsync();

        Task<Policy?> GetPolicyAsync(string id);
    }
}
