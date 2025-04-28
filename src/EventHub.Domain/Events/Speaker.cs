using System;
using Volo.Abp.Domain.Entities;

namespace EventHub.Events;

public class Speaker : Entity<Guid>
{
    public Guid SessionId { get; private set; }
    public Guid UserId { get; private set; }
    
    public override object?[] GetKeys()
    {
        return [SessionId, UserId]; //-The same as -> new object?[] { SessionId, UserId };
    }
}