using System;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace EventHub.User
{
    public interface IUserRepository : IRepository<IdentityUser, Guid>
    {
        
    }
}
