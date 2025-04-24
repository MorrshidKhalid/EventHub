using Microsoft.AspNetCore.Builder;
using EventHub;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("EventHub.Web.csproj"); 
await builder.RunAbpModuleAsync<EventHubWebTestModule>(applicationName: "EventHub.Web");

public partial class Program
{
}
