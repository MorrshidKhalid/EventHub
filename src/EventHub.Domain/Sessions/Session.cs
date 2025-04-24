using System;
using System.Collections.Generic;
using EventHub.Speakers;
using Volo.Abp.Domain.Entities;

namespace EventHub.Sessions;

public class Session : Entity<Guid>
{
    public Guid TrackId { get; private set; }
    public string Title { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public string Langauge { get; private set; }
    public ICollection<Speaker?> Speakers { get; private set; }
}