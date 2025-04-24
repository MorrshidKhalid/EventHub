using System;
using System.Collections.Generic;
using EventHub.Tracks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EventHub.Events;

public class Event : FullAuditedAggregateRoot<Guid>
{
    //Here I implement only the main prperties
    public Guid OrganizationId { get; private set; }
    public string Title { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public string Description { get; private set; }
    public string? Language { get; private set; }
    public int? Capacity { get; private set; }
    public bool IsOnline { get; private set; }
    public bool IsDraft { get; private set; }
    public ICollection<Track?> Trackes { get; set; }

    //AddTrack
    
    //UpdateTrack
    
    //RemoveTrack
    
    //AddSession
    
    //UpdateSession
    
    //RemoveSession
    
    //Publish

    //Checks whether the new session's start time and end time conflict with other session in the same track.
    //Also, the session time should not overflow the event time.
    //Limit the number of session in an event.
    //Check whether the session speaker has another talk in the same time range.
}