using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using EventHub.Localization;

namespace EventHub.Web;

[Dependency(ReplaceServices = true)]
public class EventHubBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<EventHubResource> _localizer;

    public EventHubBrandingProvider(IStringLocalizer<EventHubResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
