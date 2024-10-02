using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AbpIssue;

[Dependency(ReplaceServices = true)]
public class AbpIssueBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AbpIssue";
}
