using AbpIssue.Events;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AbpIssue.EventHandler;

public class PongEventHandler
    : AbstractEventHandler<PongEventHandler>,
        IDistributedEventHandler<PongEto>,
        ISingletonDependency
{
    public const uint MaxPackageSize = 2048u;

    readonly IDistributedEventBus _distributedEventBus;

    public PongEventHandler(
        IDistributedEventBus distributedEventBus,
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<PongEventHandler> logger
    )
        : base(hostApplicationLifetime, logger)
    {
        _distributedEventBus = distributedEventBus;
    }

    public async Task HandleEventAsync(PongEto eventData)
    {
        SequenceNumberDuplicationCheck(eventData.SequenceNumber);

        // at the end of each package of pong events produce a package of ping events with doubled size.
        if (eventData.PackageIndex == eventData.PackageSize - 1)
        {
            var nextPackageSize = eventData.PackageSize * 2;

            // ... stop producing new events when reaching MaxPackageSize
            if (nextPackageSize > MaxPackageSize)
            {
                // no need to await this here
                _logger.LogInformation("Reached MaxPackageSize in PongEto. Stopping application.");
                Task.Run(_hostApplicationLifetime.StopApplication);
            }
            else
            {
                await SendPingEventsPackage(eventData.SequenceNumber + 1, nextPackageSize);
            }
        }
    }

    private async Task SendPingEventsPackage(ulong sequenceNumber, uint packageSize)
    {
        _logger.LogDebug($"Sending {packageSize} PingEtos");

        for (uint i = 0; i < packageSize; i++)
        {
            await _distributedEventBus.PublishAsync<PingEto>(
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
