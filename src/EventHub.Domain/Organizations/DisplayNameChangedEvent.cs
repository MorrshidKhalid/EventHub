namespace EventHub.Organizations;

public class DisplayNameChangedEvent
{
    public Organization Organization { get; }

    public DisplayNameChangedEvent(Organization @organization)
    {
        Organization = @organization;
    }
}