using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace EventHub.Organizations;

public class DisplayNameHandler : ILocalEventHandler<DisplayNameChangedEvent>, ITransientDependency

{
    public async Task HandleEventAsync(DisplayNameChangedEvent eventData)
    {
        var organization = eventData.Organization;
       
        Console.WriteLine($"******** Display Name: {organization.DisplayName}");
    }
}