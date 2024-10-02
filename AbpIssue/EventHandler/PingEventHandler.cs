using AbpIssue.Events;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AbpIssue.EventHandler;

public class PingEventHandler
    : AbstractEventHandler<PingEventHandler>,
        IDistributedEventHandler<PingEto>,
        ISingletonDependency
{
    readonly IDistributedEventBus _distributedEventBus;

    public PingEventHandler(
        IDistributedEventBus distributedEventBus,
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<PingEventHandler> logger
    )
        : base(hostApplicationLifetime, logger)
    {
        _distributedEventBus = distributedEventBus;
    }

    public async Task HandleEventAsync(PingEto eventData)
    {
        SequenceNumberDuplicationCheck(eventData.SequenceNumber);

        // at the end of each package of ping events produce a package of pong events.
        if (eventData.PackageIndex == eventData.PackageSize - 1)
            await SendPingEventsPackage(eventData.SequenceNumber + 1, eventData.PackageSize);
    }

    private async Task SendPingEventsPackage(ulong sequenceNumber, uint packageSize)
    {
        _logger.LogDebug($"Sending {packageSize} PongEtos");

        for (uint i = 0; i < packageSize; i++)
        {
            await _distributedEventBus.PublishAsync<PongEto>(
                new()
                {
                    SequenceNumber = sequenceNumber++,
                    PackageIndex = i,
                    PackageSize = packageSize,
                }
            );
        }
    }
}
