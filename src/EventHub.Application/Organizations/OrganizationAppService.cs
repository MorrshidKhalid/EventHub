using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHub.Organizations.Memberships;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace EventHub.Organizations;

public class OrganizationAppService : EventHubAppService, IOrganizationAppService
{
    
    private readonly OrganizationManager _organizationManager;
    private readonly IRepository<Organization, Guid> _organizationRepository;
    private readonly IOrganizationMembershipRepository _organizationMembershipRepository;

    public OrganizationAppService(OrganizationManager organizationManager, IRepository<Organization, Guid> organizationRepository, IOrganizationMembershipRepository organizationMembershipRepository)
    {
        _organizationManager = organizationManager;
        _organizationRepository = organizationRepository;
        _organizationMembershipRepository = organizationMembershipRepository;
    }

    public async Task<OrganizationDto> CreateAsync(CreateOrganizationDto input)
    {
        Organization org = await _organizationManager.CreateAsync(
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

        await _organizationRepository.InsertAsync(org, autoSave: true);
        
        return ObjectMapper.Map<Organization, OrganizationDto>(org);
    }

    public async Task<PagedResultDto<OrganizationInListDto>> GetListAsync(OrganizationInListFilterDto input)
    {
        var organizationQueryable = await _organizationRepository.GetQueryableAsync();
        var organizationMemberQueryable = await _organizationMembershipRepository.GetQueryableAsync();
        
        var query = organizationQueryable;
        
        if (input.RegisteredUserId.HasValue)
        {
            var registeredOrganizations = organizationMemberQueryable
                .Where(organizationMember => organizationMember.UserId == input.RegisteredUserId.Value)
                .Select(organizationMember => organizationMember.OrganizationId);
            
            var orgIds = await AsyncExecuter.ToListAsync(registeredOrganizations);
            query = query.Where(organizationMember => orgIds.Contains(organizationMember.Id));
        }
        
        var totalCount = await AsyncExecuter.CountAsync(query);
        query = query.PageBy(input);
        
        var organizationDto = ObjectMapper
            .Map<List<Organization>, List<OrganizationInListDto>>(await AsyncExecuter.ToListAsync(query));
        
        return new PagedResultDto<OrganizationInListDto>(totalCount, organizationDto);
    }

    // public async Task<ListResultDto<OrganizationDto>> GetAllAsync()
    // {
    //     var orgList = await _organizationRepository.GetListAsync();
    //     var orgDtos = ObjectMapper.Map<List<Organization>, List<OrganizationDto>>(orgList);
    //     
    //     return new ListResultDto<OrganizationDto>(orgDtos);
    // }

    public async Task<OrganizationProfileDto> GetOrganizationProfileAsync(string name)
    {
        var selectedOrg = await _organizationRepository.GetAsync(x => x.Name == name);
        return ObjectMapper.Map<Organization, OrganizationProfileDto>(selectedOrg);
    }

    public async Task<ListResultDto<OrganizationInListDto>> GetOrganizationsByUserIdAsync(Guid userId)
    {
        //If you want to make it more flaxable with more paging, join, and more,
        //use (GetQueryableAsync).
        var organizationsList = await _organizationRepository.GetListAsync(x => x.OwnerUserId == userId);
        var orgInListDtos = ObjectMapper.Map<List<Organization>, List<OrganizationInListDto>>(organizationsList);
        
        return new ListResultDto<OrganizationInListDto>(orgInListDtos);
    }

    public async Task<bool> IsOrganizationOwnerAsync(Guid organizationId)
    {
        return CurrentUser.Id.HasValue && await _organizationRepository.AnyAsync(
            o => o.Id == organizationId && o.OwnerUserId == CurrentUser.GetId()); 
    }

    public async Task UpdateAsync(Guid id, UpdateOrganizationDto input)
    {
        var org = await _organizationRepository.GetAsync(id);

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