using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace EventHub.Events;

public class Session : Entity<Guid>
{
    public Guid TrackId { get; private set; }

    public string Title { get; private set; }

    public DateTime StartTime { get; private set; }

    public DateTime EndTime { get; private set; }

    public string Description { get; private set; }

    public string Language { get; set; }
        
    public ICollection<Speaker> Speakers { get; private set; }
}