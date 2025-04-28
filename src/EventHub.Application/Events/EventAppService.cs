using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EventHub.Events;

public class EventAppService : EventHubAppService, IEventAppService
{
    private readonly IRepository<Event, Guid> _organizationRepository;
    private readonly EventManager _eventManager;

    public EventAppService(IRepository<Event, Guid> organizationRepository, EventManager eventManager)
    {
        _organizationRepository = organizationRepository;
        _eventManager = eventManager;
    }

    public Task<EventDto> CreateAsync(CreateEventDto input)
    {
        // Get the org
        
        // Check if current user is the org user
        
        // Create an event obj
        
        // Set location and language
        
        // Set Capacity
        
        // Insert to database
        
        // return the dto
        throw new NotImplementedException();
    }
}