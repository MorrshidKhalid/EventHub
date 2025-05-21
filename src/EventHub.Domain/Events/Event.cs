using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using EventHub.Exceptions;
using EventHub.Tracks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace EventHub.Events;

public class Event : FullAuditedAggregateRoot<Guid>
{
    public Guid OrganizationId { get; private set; }
    
    public string UrlCode { get; private set; }
    
    public string Url { get; private set; }

    public string Title { get; private set; }

    public DateTime StartTime { get; private set; }

    public DateTime EndTime { get; private set; }

    public string Description { get; private set; }
    
    public bool IsOnline { get; private set; }
    
    public string OnlineLink { get; private set; }
    
    public Guid CountryId { get; private set; }
    
    public string CountryName { get; private set; }
    
    public string City { get; private set; }
    
    public string? Language { get; private set; }

    public int? Capacity { get; private set; }

    public bool IsRemindingEmailSent { get; private set; }
    
    public bool IsEmailSentToMembers { get; set; }
    
    public int TimingChangeCount  { get; set; }
    
    public bool IsTimingChangeEmailSent { get; set; }
    
    public bool IsDraft { get; private set; }

    public ICollection<Track> Tracks { get; set; }

    private Event()
    {
        //Required By EF Core
    }

    internal Event(
        Guid id,
        Guid organizationId,
        string urlCode,
        string title,
        DateTime startTime,
        DateTime endTime,
        string description) 
        : base(id)
    {
        OrganizationId = organizationId;
        UrlCode = Check.NotNullOrWhiteSpace(urlCode, nameof(urlCode), EventConsts.UrlCodeLength, EventConsts.UrlCodeLength);
        
        SetTitle(title);
        SetDescription(description);
        SetTimeInternal(startTime, endTime);
        
        Publish(false);
        
        Tracks = new Collection<Track>();
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
    
    private Event SetTimeInternal(DateTime startTime, DateTime endTime)
    {
        if (startTime > endTime)
        {
            new HandleGlobalException(new EndTimeEarlierThanStartTimeException()).GenerateExceptionCode(
                EventHubDomainErrorCodes.EndTimeCantBeEarlierThanStartTime,
                endTime.ToString(CultureInfo.InvariantCulture));
        }
        

        StartTime = startTime;
        EndTime = endTime;

        return this;
    }

    public Event AddTrack(Guid trackId, string name)
    {
        if (Tracks.Any(x => x.Name == name))
        {
            new HandleGlobalException(new TrackException()).GenerateExceptionCode(
                EventHubDomainErrorCodes.TrackNameAlreadyExist, name);
        }
        
        Tracks.Add(new Track(trackId, Id, name));
        return this;
    }

    public Event UpdateTrack(Guid trackId, string name)
    {
        if (Tracks.Any(x => x.Name == name))
        {
            new HandleGlobalException(new TrackException()).GenerateExceptionCode(
                EventHubDomainErrorCodes.TrackNameAlreadyExist, name);
        }
        
        var track = Tracks.SingleOrDefault(x => x.Id == trackId);
        if (track is null)
        {
            new HandleGlobalException(new TrackException()).GenerateExceptionCode(
                EventHubDomainErrorCodes.TrackNotFound, trackId.ToString());
            
        }
        
        track.SetName(name);

        return this;
    }

    public Event RemoveTrack(Guid trackId)
    {
        var track = Tracks.SingleOrDefault(x => x.Id == trackId);
        
        if (track is null)
        {
            new HandleGlobalException(new TrackException())
                .GenerateExceptionCode(EventHubDomainErrorCodes.TrackNotFound, trackId.ToString());
        }
        
        Tracks.Remove(track);
        
        return this;
    }

    public Event AddSession(
        Guid trackId,
        Guid sessionId,
        string title,
        string description,
        DateTime startTime, 
        DateTime endTime,
        string language,
        ICollection<Guid> speakerUserIds)
    {
        CheckIfValidSessionTime(startTime, endTime);
        
        var track = GetTrack(trackId);
        track.AddSession(sessionId, title, description,startTime, endTime, language, speakerUserIds);

        return this;
    }

    public Event UpdateSession(
        Guid trackId,
        Guid sessionId,
        string title,
        string description,
        DateTime startTime,
        DateTime endTime,
        string language,
        ICollection<Guid> speakerUserIds)
    {
        CheckIfValidSessionTime(startTime, endTime);
        
        var track = GetTrack(trackId);
        track.UpdateSession(sessionId, title, description, startTime, endTime, language, speakerUserIds);
        return this;
    }

    public Event RemoveSession(Guid trackId, Guid sessionId)
    {
        var track = Tracks.SingleOrDefault(x => x.Id == trackId);

        if (track is null)
        {
            new HandleGlobalException(new TrackException()).GenerateExceptionCode(
                EventHubDomainErrorCodes.TrackNotFound, sessionId.ToString());
        }

        track.RemoveSession(sessionId);
        return this;
    }
    
    public Event Publish(bool isPublished = true)
    {
        IsDraft = !isPublished;

        return this;
    }

    public bool IsLive(DateTime now) => now.IsBetween(StartTime, EndTime);

    private Track GetTrack(Guid trackId)
    {
        return Tracks.FirstOrDefault(t => t.Id == trackId) 
               ?? throw new EntityNotFoundException(typeof(Track), trackId);
    }

    public void CheckIfValidSessionTime(DateTime startTime, DateTime endTime)
    {
        if (startTime < StartTime || endTime < StartTime)
        {
            new HandleGlobalException(new ValidateSessionException()).GenerateExceptionCode(
                    EventHubDomainErrorCodes.SessionTimeShouldBeInTheEventTime,
                    $"s:{startTime} e:{endTime}");
        }
    }
    
    
    //AddSession
    
    //UpdateSession
    
    //RemoveSession
    
    //Publish

    //Checks whether the new session's start time and end time conflict with other session in the same track.
    //Also, the session time should not overflow the event time.
    //Limit the number of session in an event.
    //Check whether the session speaker has another talk in the same time range.
}