using System;
using System.Collections.Generic;
using EventHub.Tracks;
using EventHub.Utility;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EventHub.Events;

public class Event : FullAuditedAggregateRoot<Guid>
{
    public Guid OrganizationId { get; private set; }

    public string Title { get; private set; }

    public DateTime StartTime { get; private set; }

    public DateTime EndTime { get; private set; }

    public string Description { get; private set; }

    public string? Language { get; private set; }

    public int? Capacity { get; private set; }

    public bool IsOnline { get; private set; }

    public bool IsDraft { get; private set; }

    public ICollection<Track?> Trackes { get; set; }

    private Event()
    {
        //Required By EF Core
    }

    internal Event(
        Guid id, Guid organizationId,
        string title, DateTime startDateTime,
        DateTime endDateTime, string description) 
        : base(id)
    {
        SetOrganizationId(organizationId);
        SetTitle(title);
        SetTime(startDateTime, endDateTime);
        SetDescription(description);
    }


    internal Event SetOrganizationId(Guid organizationId)
    {
        OrganizationId = Check.NotNull(organizationId, nameof(organizationId));
        return this;
    }

    public Event SetTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), EventConsts.MaxTitleLength, EventConsts.MinTitleLength);
        return this;
    }

    public Event SetDescription(string description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(description), EventConsts.MaxDescriptionLength, EventConsts.MinDescriptionLength);
        return this;
    }
    
    public Event SetTime(DateTime startTime, DateTime endTime)
    {
        if (startTime == StartTime && endTime == EndTime)
        {
            return this;
        }
        
        DateValidation.IsValidTime(startTime, endTime);
        
        StartTime = startTime;
        EndTime = endTime;

        return this;
    }
    
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