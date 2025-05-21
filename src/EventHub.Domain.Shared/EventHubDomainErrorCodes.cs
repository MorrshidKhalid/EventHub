using System;
using System.Runtime.CompilerServices;

namespace EventHub;

public static class EventHubDomainErrorCodes
{
    public const string EndTimeCantBeEarlierThanStartTime = "EventHub:EndTimeCantBeEarlierThanStartTime";
    public const string? OrganizationNameAlreadyExists = "EventHub:OrganizationNameAlreadyExists";
    public const string SessionTimeShouldBeInTheEventTime = "EventHub:SessionTimeShouldBeInTheEventTime";
    public const string TrackNameAlreadyExist = "EventHub:TrackNameAlreadyExists";
    public const string TrackNotFound = "EventHub:TrackNotFound";
    public const string NotAuthorizedToUpdateOrganizationProfile = "EventHub:NotAuthorizedToUpdateOrganizationProfile";

    public const string NotAuthorizedToCreateEventInThisOrganization = "EventHub:NotAuthorizedToCreateEventInThisOrganization";
}
