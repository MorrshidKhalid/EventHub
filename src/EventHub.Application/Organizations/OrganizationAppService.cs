using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace EventHub.Organizations;

public class OrganizationAppService(
    OrganizationManager organizationManager,
    IRepository<Organization, Guid> organizationRepository)
    : EventHubAppService, IOrganizationAppService
{
    public async Task<OrganizationDto> CreateAsync(CreateOrganizationDto input)
    {
        var org = await organizationManager.CreateAsync(
            CurrentUser.GetId(),
            input.Name,
            input.DisplayName,
            input.Description);

        org.Website = input.Website;
        org.TwitterUsername = input.TwitterUsername;
        org.GitHubUsername = input.GitHubUsername;
        org.FacebookUsername = input.FacebookUsername;
        org.InstagramUsername = input.InstagramUsername;
        org.MediumUsername = input.MediumUsername;

        await organizationRepository.InsertAsync(org);
        
        return ObjectMapper.Map<Organization, OrganizationDto>(org);
    }
}