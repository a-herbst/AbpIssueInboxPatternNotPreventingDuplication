using AbpIssue.Localization;
using Volo.Abp.Application.Services;

namespace AbpIssue.Services;

/* Inherit your application services from this class. */
public abstract class AbpIssueAppService : ApplicationService
{
    protected AbpIssueAppService()
    {
        LocalizationResource = typeof(AbpIssueResource);
    }
}