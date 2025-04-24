using Xunit;

namespace EventHub.EntityFrameworkCore;

[CollectionDefinition(EventHubTestConsts.CollectionDefinitionName)]
public class EventHubEntityFrameworkCoreCollection : ICollectionFixture<EventHubEntityFrameworkCoreFixture>
{

}
