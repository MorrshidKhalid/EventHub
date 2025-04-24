using System;
using Volo.Abp.Domain.Entities;

namespace EventHub.Speakers;

public class Speaker : Entity
{
    public Guid SessionId { get; private set; }
    public Guid UserId { get; private set; }
    
    public override object?[] GetKeys()
    {
        return [SessionId, UserId]; //-The same as -> new object?[] { SessionId, UserId };
    }
}