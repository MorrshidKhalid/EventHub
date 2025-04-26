using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace EventHub.Organizations.Plans;

public class PlanManager(IRepository<Organization, Guid> organizationRepository) :  ITransientDependency
{
    private bool IsPeriodAllowed(Organization org) => org.TrialPeriod <= DateTime.Now;
    public async Task<bool> CanCreateNewEventAsync(Guid organizationId)
    {
        var organization = await organizationRepository.GetAsync(organizationId);
        return IsPeriodAllowed(organization);
    }
}