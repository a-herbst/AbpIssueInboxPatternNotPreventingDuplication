using AbpIssue.Data;
using Microsoft.Extensions.Options;
using System.Threading;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Timing;

namespace AbpIssue.DebugLog;

[ExposeServices(typeof(IDbContextEventInbox<AbpIssueDbContext>))]
[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
public class DbContextEventInboxLogDecorator : DbContextEventInbox<AbpIssueDbContext>
{
    readonly ILogger<DbContextEventInboxLogDecorator> _logger;

    public DbContextEventInboxLogDecorator(ILogger<DbContextEventInboxLogDecorator> logger, IDbContextProvider<AbpIssueDbContext> dbContextProvider, IClock clock, IOptions<AbpEventBusBoxesOptions> eventBusBoxesOptions) : base(dbContextProvider, clock, eventBusBoxesOptions)
    {
        _logger = logger;
    }

    public override Task EnqueueAsync(IncomingEventInfo incomingEvent)
    {
        var threadId = Thread.CurrentThread.ManagedThreadId;
        _logger.LogDebug($"DbContextEventInbox.EnqueueAsync(incomingEvent.MessageId: {incomingEvent.MessageId}) (threadId: {threadId})");
        return base.EnqueueAsync(incomingEvent);
    }

    public override async Task<bool> ExistsByMessageIdAsync(string messageId)
    {
        var threadId = Thread.CurrentThread.ManagedThreadId;
        var result = await base.ExistsByMessageIdAsync(messageId);
        _logger.LogDebug($"DbContextEventInbox.ExistsByMessageIdAsync(messageId: {messageId}) -> {result} (threadId: {threadId})");

        return result;
    }
}
