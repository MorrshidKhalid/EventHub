using EventHub.User;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace EventHub.EntityFrameworkCore.Users
{
    public class UserRepository : EfCoreRepository<EventHubDbContext, IdentityUser, Guid>, IUserRepository
    {
        public UserRepository(IDbContextProvider<EventHubDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
