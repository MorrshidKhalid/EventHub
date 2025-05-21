using Volo.Abp.Modularity;

namespace EventHub;

/* Inherit from this class for your domain layer tests. */
public abstract class EventHubDomainTestBase<TStartupModule> : EventHubTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
