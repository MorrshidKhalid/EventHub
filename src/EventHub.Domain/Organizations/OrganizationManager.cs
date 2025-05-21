using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EventHub.Organizations;

public class OrganizationManager : DomainService
{
    private readonly IRepository<Organization, Guid> _organizationRepository;

    public OrganizationManager(IRepository<Organization, Guid> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    private async Task<bool> IsOrgExistByName(string organizationName) 
        => await _organizationRepository.AnyAsync(x => x.Name == organizationName);
    
    public async Task<Organization> CreateAsync(
        Guid ownerUserId,
        string name,
        string displayName,
        string description)
    {
        name = name.Trim().Replace(' ', '-').ToKebabCase().ToLowerInvariant();
        
        if (await IsOrgExistByName(name))
        {
            throw new BusinessException(EventHubDomainErrorCodes.OrganizationNameAlreadyExists)
                .WithData(nameof(name), name);
        }
        
        return new Organization(
            GuidGenerator.Create(),
            ownerUserId,
            name,
            displayName,
            description
            );
    }
}