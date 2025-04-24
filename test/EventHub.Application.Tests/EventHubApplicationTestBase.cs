using Volo.Abp.Modularity;

namespace EventHub;

public abstract class EventHubApplicationTestBase<TStartupModule> : EventHubTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
