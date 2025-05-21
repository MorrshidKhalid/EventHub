using System;
using Volo.Abp.Domain.Repositories;

namespace EventHub.Organizations.Memberships
{
    public interface IOrganizationMembershipRepository : IRepository<OrganizationMembership, Guid>
    {
    }
}