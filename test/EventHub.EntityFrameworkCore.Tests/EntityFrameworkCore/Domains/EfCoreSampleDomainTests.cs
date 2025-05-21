using EventHub.Samples;
using Xunit;

namespace EventHub.EntityFrameworkCore.Domains;

[Collection(EventHubTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EventHubEntityFrameworkCoreTestModule>
{

}
