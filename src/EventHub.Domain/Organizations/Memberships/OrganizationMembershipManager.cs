using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace EventHub.Organizations.Memberships
{
    public class OrganizationMembershipManager : DomainService
    {
        private readonly IRepository<OrganizationMembership, Guid> _organizationMembershipRepository;

        public OrganizationMembershipManager(IRepository<OrganizationMembership, Guid> organizationMembershipRepository)
        {
            _organizationMembershipRepository = organizationMembershipRepository;
        }

        public async Task JoinAsyns(Organization organization, IdentityUser user)
        {
            if (await IsJoinedAsync(organization, user))
            {
                return;
            }

            await _organizationMembershipRepository.InsertAsync(
                new OrganizationMembership(
                    GuidGenerator.Create(),
                    organization.Id,
                    user.Id
                    ));
        }

        private async Task<bool> IsJoinedAsync(
            Organization organization,
            IdentityUser user)
        {
            return await _organizationMembershipRepository
                .AnyAsync(x => x.OrganizationId == organization.Id && x.UserId == user.Id);
        }
    }
}
