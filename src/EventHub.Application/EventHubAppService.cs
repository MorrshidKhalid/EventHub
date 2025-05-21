using EventHub.Localization;
using Volo.Abp.Application.Services;

namespace EventHub;

public abstract class EventHubAppService : ApplicationService
{
    protected EventHubAppService()
    {
        LocalizationResource = typeof(EventHubResource);
    }
}
