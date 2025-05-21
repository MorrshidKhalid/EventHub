using System;
using System.Collections.Generic;
using EventHub.Events;
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

    public Track AddSession(Guid sessionId, string title, string description, DateTime startTime, DateTime endTime, string language, ICollection<Guid> speakerUserIds)
    {

        return this;
    }

    public void SetName(string name)
    {
        throw new NotImplementedException();
    }

    public void UpdateSession(
        Guid sessionId,
        string title,
        string description,
        DateTime startTime,
        DateTime endTime,
        string language,
        ICollection<Guid> speakerUserIds)
    {
        throw new NotImplementedException();
    }

    public void RemoveSession(Guid sessionId)
    {
        throw new NotImplementedException();
    }
}