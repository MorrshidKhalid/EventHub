using EventHub.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace EventHub.Permissions;

public class EventHubPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var eventHub = context.AddGroup(EventHubPermissions.EventHubGroup, L("Permission:EventHubGroup"));

        var orgParent = eventHub.AddPermission(EventHubPermissions.Organization.Default, L("Permission:EventHubOrganizationParent"));
        orgParent.AddChild(EventHubPermissions.Organization.Read, L("Permission:EventHubRead"));
        orgParent.AddChild(EventHubPermissions.Organization.Create, L("Permission:EventHubCreate"));
        orgParent.AddChild(EventHubPermissions.Organization.Update, L("Permission:EventHubUpdate"));
        orgParent.AddChild(EventHubPermissions.Organization.Delete, L("Permission:EventHubDelete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EventHubResource>(name);
    }
}
