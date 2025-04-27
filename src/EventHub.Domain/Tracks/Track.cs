using System;
using System.Collections.Generic;
using EventHub.Events;
using EventHub.Sessions;
using Volo.Abp.Domain.Entities;

namespace EventHub.Tracks;

public class Track : Entity<Guid>
{
    public Guid EventId { get; private set; }
    public string Name { get; private set; }
    public ICollection<Session> Sessions { get; private set; }

    public Track(
        Guid id,
        Guid eventId,
        string name) 
        : base(id)
    {
        EventId = eventId;
        Name = name;
    }

    public Track AddSession()
    {

        return this;
    }

    public void SetName(string name)
    {
        throw new NotImplementedException();
    }
}