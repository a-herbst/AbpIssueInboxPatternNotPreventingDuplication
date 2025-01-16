using System;
using RabbitMQ.Client;

namespace AbpIssue.RabbitMQ;

public interface IConnectionPool : IDisposable
{
    IConnection Get(string? connectionName = null);
}
