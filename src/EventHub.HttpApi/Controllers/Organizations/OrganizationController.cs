//using Asp.Versioning;
//using EventHub.Organizations;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;
//using Volo.Abp;
//using Volo.Abp.Application.Dtos;

//namespace EventHub.Controllers.Organizations
//{
//    [RemoteService(Name = EventHubRemoteServiceConsts.RemoteServiceName)]
//    [Area("eventhub")]
//    [ControllerName("Organization")]
//    [Route("api/eventhub/organization")]
//    public class OrganizationController : EventHubController, IOrganizationAppService
//    {
//        private readonly IOrganizationAppService _organizationAppService;

//        public OrganizationController(IOrganizationAppService organizationAppService)
//        {
//            _organizationAppService = organizationAppService;
//        }

//        [HttpPost]
//        public async Task<OrganizationDto> CreateAsync([FromForm]CreateOrganizationDto input)
//        {
//            return await _organizationAppService.CreateAsync(input);
//        }
        
//        public Task<PagedResultDto<OrganizationInListDto>> GetListAsync(OrganizationInListFilterDto input)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<OrganizationProfileDto> GetProfileAsync(string name)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ListResultDto<OrganizationInListDto>> GetOrganizationsByUserIdAsync(Guid userId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> IsOrganizationOwnerAsync(Guid organizationId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task UpdateAsync(Guid id, UpdateOrganizationDto input)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
