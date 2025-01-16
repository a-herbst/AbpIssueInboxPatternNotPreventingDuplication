using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace AbpIssue.EventBus.RabbitMq;

[DependsOn(
    typeof(AbpEventBusModule),
    typeof(AbpIssue.RabbitMQ.AbpRabbitMqModule))]
public class AbpEventBusRabbitMqModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpRabbitMqEventBusOptions>(configuration.GetSection("RabbitMQ:EventBus"));
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context
            .ServiceProvider
            .GetRequiredService<IRabbitMqDistributedEventBus>()
            .Initialize();
    }
}
