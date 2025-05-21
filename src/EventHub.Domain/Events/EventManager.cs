using System;
using System.Threading.Tasks;
using EventHub.Organizations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EventHub.Events;

public class EventManager : DomainService
{
    
    private readonly IRepository<Organization, Guid> _organizationRepository;

    public EventManager(IRepository<Organization, Guid> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Event> CreateAsync(
        Organization organization,
        string title,
        DateTime startDateTime,
        DateTime endDateTime,
        string description)
    {

    }
}