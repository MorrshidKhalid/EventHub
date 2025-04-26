using System;
using System.Threading.Tasks;
using EventHub.Organizations;
using EventHub.Organizations.Plans;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EventHub.Events;

public class EventManager : DomainService
{
    
    private readonly IRepository<Organization, Guid> _organizationRepository;
    private readonly PlanManager _planManager;

    public EventManager(IRepository<Organization, Guid> organizationRepository, PlanManager planManager)
    {
        _organizationRepository = organizationRepository;
        _planManager = planManager;
    }

    public async Task<Event> CreateAsync(
        Guid organizationId,
        string title,
        DateTime startDateTime,
        DateTime endDateTime,
        string description)
    {

        if (!await _planManager.CanCreateNewEventAsync(organizationId))
        {
            throw new BusinessException("Can't create new event, your free trial period is finished.");
        }
        
        
        return new Event(
            GuidGenerator.Create(),
            organizationId,
            title,
            startDateTime,
            endDateTime,
            description
            );
    }
}