using Volo.Abp.EventBus.Distributed;

namespace AbpIssue.EventBus.RabbitMq;

public interface IRabbitMqDistributedEventBus : IDistributedEventBus
{
    void Initialize();
}
