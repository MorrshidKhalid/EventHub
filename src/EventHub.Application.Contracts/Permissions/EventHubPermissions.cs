namespace EventHub.Permissions;

public static class EventHubPermissions
{
    public const string EventHubGroup = "EventHub";
    
    public static class Organization
    {
        public const string Default = $"{EventHubGroup}.Organization";
        public const string Read = $"{Default}.Read";
        public const string Create = $"{Default}.Create";
        public const string Update = $"{Default}.Update";
        public const string Delete = $"{Default}.Delete";
    }
}
