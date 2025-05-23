﻿using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EventHub.Pages;

[Collection(EventHubTestConsts.CollectionDefinitionName)]
public class Index_Tests : EventHubWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
