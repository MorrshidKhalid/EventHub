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


    public Track AddSession()
    {

        return this;
    }
}