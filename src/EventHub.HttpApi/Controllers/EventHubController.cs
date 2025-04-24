using EventHub.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EventHub.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EventHubController : AbpControllerBase
{
    protected EventHubController()
    {
        LocalizationResource = typeof(EventHubResource);
    }
}
