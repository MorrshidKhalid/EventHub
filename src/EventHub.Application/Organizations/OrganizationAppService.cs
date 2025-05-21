using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHub.Organizations.Memberships;
using EventHub.User;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace EventHub.Organizations;

public class OrganizationAppService : EventHubAppService, IOrganizationAppService
{
    private readonly OrganizationManager _organizationManager;
    private readonly IOrganizationMembershipRepository _organizationMembershipRepository;
    private readonly IRepository<Organization, Guid> _organizationRepository;
    private readonly IUserRepository _userRepository;

    public OrganizationAppService(
        OrganizationManager organizationManager,
        IRepository<Organization, Guid> organizationRepository,
        IUserRepository userRepository,
        IOrganizationMembershipRepository organizationMembershipRepository)
    {
        _organizationManager = organizationManager;
        _organizationRepository = organizationRepository;
        _userRepository = userRepository;
        _organizationMembershipRepository = organizationMembershipRepository;
    }

    [Authorize]
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

        // To use with user profile.
        if (input.RegisteredUserId.HasValue)
        {
            var registeredOrganization = organizationMemberQueryable
                .Where(x => x.UserId == input.RegisteredUserId)
                .Select(x => x.OrganizationId);

            var organizationIds = await AsyncExecuter.ToListAsync(registeredOrganization);
            query = query.Where(x => organizationIds.Contains(x.Id));
        }

        var totalCount = await AsyncExecuter.CountAsync(query);
        query = query.PageBy(input);

        var organizationDto = ObjectMapper
                .Map<List<Organization>, List<OrganizationInListDto>>(await AsyncExecuter.ToListAsync(query));

        return new PagedResultDto<OrganizationInListDto>(
                totalCount,
                organizationDto
            );
    }

    public async Task<OrganizationProfileDto> GetProfileAsync(string name)
    {
        var organization = await _organizationRepository.GetAsync(o => o.Name == name);
        var organizationProfileDto = ObjectMapper.Map<Organization, OrganizationProfileDto>(organization);

        var owner = await _userRepository.GetAsync(organization.OwnerUserId);
        organizationProfileDto.OwnerUserName = owner.UserName;
        organizationProfileDto.OwnerEmail = owner.Email;

        return organizationProfileDto;
    }

    public async Task<ListResultDto<OrganizationInListDto>> GetOrganizationsByUserIdAsync(Guid userId)
    {
        var organizationsList = await _organizationRepository.GetListAsync(o => o.OwnerUserId == userId);
        var orgInListDtos = ObjectMapper.Map<List<Organization>, List<OrganizationInListDto>>(organizationsList);

        return new ListResultDto<OrganizationInListDto>(orgInListDtos);
    }

    public async Task<bool> IsOrganizationOwnerAsync(Guid organizationId)
    {
        return CurrentUser.Id.HasValue && await _organizationRepository.AnyAsync(
            o => o.Id == organizationId && o.OwnerUserId == CurrentUser.GetId());
    }

    [Authorize]
    public async Task UpdateAsync(Guid id, UpdateOrganizationDto input)
    {
        var org = await _organizationRepository.GetAsync(id);

        if (org.OwnerUserId != CurrentUser.GetId())
        {
            throw new AbpAuthorizationException(EventHubDomainErrorCodes.NotAuthorizedToUpdateOrganizationProfile)
                .WithData("OrganizationName", org.DisplayName);
        }

        org.SetDisplayName(input.DisplayName);
        org.SetDescription(input.Description);

        org.Website = input.Website;
        org.FacebookUsername = input.FacebookUsername;
        org.InstagramUsername = input.InstagramUsername;
        org.TwitterUsername = input.TwitterUsername;
        org.GitHubUsername = input.GitHubUsername;
        org.MediumUsername = input.MediumUsername;

        await _organizationRepository.UpdateAsync(org, autoSave: true);
    }
}