using AbpIssue.Events;
using AbpIssue.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Distributed;

namespace AbpIssue.Services;

/* Inherit your application services from this class. */
public abstract class AbpIssueAppService : ApplicationService
{
    protected AbpIssueAppService()
    {
        LocalizationResource = typeof(AbpIssueResource);
    }
}