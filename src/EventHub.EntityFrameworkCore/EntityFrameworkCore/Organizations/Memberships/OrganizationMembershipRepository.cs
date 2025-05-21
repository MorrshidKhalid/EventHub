using EventHub.Organizations.Memberships;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EventHub.EntityFrameworkCore.Organizations.Memberships
{
    public class OrganizationMembershipRepository : EfCoreRepository<EventHubDbContext, OrganizationMembership, Guid>,
        IOrganizationMembershipRepository
    {
        public OrganizationMembershipRepository(IDbContextProvider<EventHubDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
