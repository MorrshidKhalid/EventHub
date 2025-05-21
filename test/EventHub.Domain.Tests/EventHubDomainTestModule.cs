using Volo.Abp.Modularity;

namespace EventHub;

[DependsOn(
    typeof(EventHubDomainModule),
    typeof(EventHubTestBaseModule)
)]
public class EventHubDomainTestModule : AbpModule
{

}
