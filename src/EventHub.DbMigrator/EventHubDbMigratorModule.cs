using EventHub.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EventHub.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EventHubEntityFrameworkCoreModule),
    typeof(EventHubApplicationContractsModule)
)]
public class EventHubDbMigratorModule : AbpModule
{
}
