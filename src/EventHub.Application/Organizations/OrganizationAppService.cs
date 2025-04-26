using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
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

    public async Task<PagedResultDto<OrganizationInListDto>> GetListAsync(OrganizationInListFilterDto input)
    {
        var orgQueryable = await organizationRepository.GetQueryableAsync();

        var query = orgQueryable;
        if (input.RegisteredUserId.HasValue)
        {
            query = orgQueryable.Where(x => x.OwnerUserId == input.RegisteredUserId);
        }
        
        var count = await AsyncExecuter.CountAsync(query);
        var result = await AsyncExecuter.ToListAsync(query);
        var orgList = ObjectMapper.Map<List<Organization>, List<OrganizationInListDto>>(result);
        
        return new PagedResultDto<OrganizationInListDto>(count, orgList);
    }

    public async Task<ListResultDto<OrganizationDto>> GetAllAsync()
    {
        var orgList = await organizationRepository.GetListAsync();
        var orgDtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(orgList);
        
        return new ListResultDto<OrganizationDto>(orgDtos);
    }

    public async Task<OrganizationProfileDto> GetOrganizationProfileAsync(string name)
    {
        var selectedOrg = await organizationRepository.GetAsync(x => x.Name == name);
        return ObjectMapper.Map<Organization, OrganizationProfileDto>(selectedOrg);
    }

    public async Task UpdateAsync(Guid id, UpdateOrganizationDto input)
    {
        var org = await organizationRepository.GetAsync(id);

        org.SetDisplayName(input.DisplayName);
        org.SetDescription(input.Description);
        
        org.Website = input.Website;
        org.FacebookUsername = input.FacebookUsername;
        org.InstagramUsername = input.InstagramUsername;
        org.TwitterUsername = input.TwitterUsername;
        org.GitHubUsername = input.GitHubUsername;
        org.MediumUsername = input.MediumUsername;
        
        ObjectMapper.Map(input, org);
    }
}