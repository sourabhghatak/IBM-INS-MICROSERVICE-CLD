
namespace Microservice.Policy
{
    public class PolicyBackgroundService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public PolicyBackgroundService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this.ProcessPolicyEvent(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }

        private async Task ProcessPolicyEvent(CancellationToken cancellationToken)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                try
                {
                    var mqService = scope.ServiceProvider.GetRequiredService<Func<string, IMQService>>();
                    var policyService = scope.ServiceProvider.GetRequiredService<IPolicyService>();

                    //Get the message
                    var claimsResponse = await mqService("consume").PullMessageFromQueueAsync<Claims>();

                    if (claimsResponse == null)
                    {
                        return;
                    }

                    //Get poicy details
                    Policy? policy = await policyService.GetPolicyAsync(claimsResponse.PolicyNumber);

                    if (policy == null)
                    {
                        return;
                    }

                    policy.ClaimDetails = claimsResponse.ClaimDetails;

                    policy = await policyService.CreatePolicyAsync(policy);
                }
                catch (Exception)
                {

                }
                
            }
        }
    }
}
