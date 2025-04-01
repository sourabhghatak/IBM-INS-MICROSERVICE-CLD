namespace Microservice.Claims
{
    public interface IClaimsService
    {
        Task<Claims?> ClaimPolicy(ClaimPolicyDTO? claimPolicyDTO);
    }
}
