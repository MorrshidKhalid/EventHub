using EventHub.Samples;
using Xunit;

namespace EventHub.EntityFrameworkCore.Applications;

[Collection(EventHubTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EventHubEntityFrameworkCoreTestModule>
{

}
