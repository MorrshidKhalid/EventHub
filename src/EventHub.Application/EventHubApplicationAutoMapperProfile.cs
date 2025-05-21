using AutoMapper;
using EventHub.Organizations;

namespace EventHub;

public class EventHubApplicationAutoMapperProfile : Profile
{
    public EventHubApplicationAutoMapperProfile()
    {
        CreateMap<Organization, OrganizationDto>();
        CreateMap<Organization, OrganizationInListDto>();
        CreateMap<Organization, OrganizationProfileDto>();
    }
}
