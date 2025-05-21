using EventHub.Organizations;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;

namespace EventHub.Events;

public class EventAppService : EventHubAppService, IEventAppService
{
    private readonly IRepository<Organization, Guid> _organizationRepository;
    private readonly EventManager _eventManager;

    public EventAppService(
        IRepository<Organization, Guid> organizationRepository,
        EventManager eventManager)
    {
        _organizationRepository = organizationRepository;
        _eventManager = eventManager;
    }

    [Authorize]
    public async Task<EventDto> CreateAsync(CreateEventDto input)
    {
        var orgnization = await _organizationRepository.GetAsync(input.OrganizationId);

        if (CurrentUser.Id != orgnization.OwnerUserId)
        {
            throw new AbpAuthorizationException(
                L["EventHub:NotAuthorizedToCreateEventInThisOrganization", orgnization.DisplayName],
                EventHubDomainErrorCodes.NotAuthorizedToCreateEventInThisOrganization);
        }

        await _eventManager.CreateAsync(
            orgnization,
            input.Title,
            input.StartDateTime,
            input.EndDateTime,
            input.Description);
    }
}