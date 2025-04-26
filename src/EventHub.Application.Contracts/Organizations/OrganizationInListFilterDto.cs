using System;
using Volo.Abp.Application.Dtos;

namespace EventHub.Organizations;

public class OrganizationInListFilterDto : PagedResultRequestDto
{
    public Guid? RegisteredUserId { get; set; }
}