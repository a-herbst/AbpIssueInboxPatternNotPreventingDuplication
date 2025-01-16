using System;

namespace AbpIssue.RabbitMQ;

public interface IChannelPool : IDisposable
{
    IChannelAccessor Acquire(string? channelName = null, string? connectionName = null);
}
