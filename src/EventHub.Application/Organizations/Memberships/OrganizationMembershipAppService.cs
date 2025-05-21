using EventHub.User;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace EventHub.Organizations.Memberships
{
    public class OrganizationMembershipAppService : EventHubAppService, IOrganizationMembershipAppService
    {
        private readonly IRepository<Organization, Guid> _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly OrganizationMembershipManager _organizationMembershipManager;
        private readonly IOrganizationMembershipRepository _organizationMembershipsRepository;

        public OrganizationMembershipAppService(
            IRepository<Organization, Guid> organizationRepository,
            OrganizationMembershipManager organizationMembershipManager,
            IUserRepository userRepository,
            IOrganizationMembershipRepository organizationMembershipsRepository)
        {
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _organizationMembershipManager = organizationMembershipManager;
            _organizationMembershipsRepository = organizationMembershipsRepository;
        }


        [Authorize]
        public async Task JoinAsync(Guid organizationId)
        {
            var user = await _userRepository.GetAsync(CurrentUser.GetId());
            var organization = await _organizationRepository.GetAsync(organizationId);

            await _organizationMembershipManager.JoinAsyns(organization, user);
        }

        [Authorize]
        public async Task LeaveAsync(Guid organizationId)
        {
            var user = await _userRepository.GetAsync(CurrentUser.GetId());

            await _organizationMembershipsRepository.DeleteAsync(
                x => x.OrganizationId == organizationId && x.UserId == user.Id
                );
        }

        public async Task<PagedResultDto<OrganizationMemberDto>> GetMembersAsync(OrganizationMemberListFilterDto input)
        {
            var organizationMembershipQueryable = await _organizationMembershipsRepository.GetQueryableAsync();
            var userQueryable = await _userRepository.GetQueryableAsync();

            var query = from organizationMembership in organizationMembershipQueryable
                        join user in userQueryable on organizationMembership.UserId equals user.Id
                        where organizationMembership.OrganizationId == input.OrganizationId
                        select user;

            var totalCount = await AsyncExecuter.CountAsync(query);
            var users = await AsyncExecuter.ToListAsync(query.PageBy(input));

            return new PagedResultDto<OrganizationMemberDto>(
                totalCount,
                ObjectMapper.Map<List<IdentityUser>, List<OrganizationMemberDto>>(users));
        }
    }
}