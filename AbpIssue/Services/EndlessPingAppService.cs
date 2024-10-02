using AbpIssue.Events;
using Volo.Abp.EventBus.Distributed;

namespace AbpIssue.Services
{
    public class EndlessPingAppService : AbpIssueAppService
    {
        readonly IDistributedEventBus _distributedEventBus;

        public EndlessPingAppService(IDistributedEventBus distributedEventBus) : base()
        {
            _distributedEventBus = distributedEventBus;
        }

        public virtual async Task StartPing()
        {
            await _distributedEventBus.PublishAsync<PingEto>(new() { SequenceNumber = 0u, PackageIndex = 0u, PackageSize = 1u });
        }
    }
}
