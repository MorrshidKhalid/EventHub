using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace EventHub.Organizations.Memberships
{
    public interface IOrganizationMembershipAppService : IApplicationService
    {
        Task JoinAsync(Guid organizationId);

        Task LeaveAsync(Guid organizationId);

        Task<PagedResultDto<OrganizationMemberDto>> GetMembersAsync(OrganizationMemberListFilterDto input);
    }
}
