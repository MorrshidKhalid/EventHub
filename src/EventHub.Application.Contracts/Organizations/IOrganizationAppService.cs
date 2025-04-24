using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EventHub.Organizations;

public interface IOrganizationAppService : IApplicationService
{
    Task<OrganizationDto> CreateAsync(CreateOrganizationDto input);
}