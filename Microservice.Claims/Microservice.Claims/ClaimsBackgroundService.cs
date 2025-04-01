
namespace Microservice.Claims
{
    public class ClaimsBackgroundService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public ClaimsBackgroundService(IServiceProvider serviceProvider)
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
                var mqService = scope.ServiceProvider.GetRequiredService<Func<string, IMQService>>();

                //Get the message
                var policyResponse = await mqService("consume").PullMessageFromQueueAsync<Policy>();

                if (policyResponse == null)
                {
                    return;
                }

                //Save the policy in the database
                Policy policy = new() { CustomerId = policyResponse.CustomerId, Id = policyResponse.Id };

                var policyService = scope.ServiceProvider.GetRequiredService<IPolicyService>();

                await policyService.CreateOrUpdatePolicyAsync(policy);
            }
        }
    }
}
