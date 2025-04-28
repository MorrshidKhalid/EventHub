using EventHub.Countries;
using EventHub.Events;
using EventHub.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EventHubuilder.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ConfigureByConvention();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(EventConsts.MaxTitleLength);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(EventConsts.MaxDescriptionLength);
        builder.Property(x => x.UrlCode).IsRequired().HasMaxLength(EventConsts.UrlCodeLength);
        builder.Property(x => x.Url).IsRequired().HasMaxLength(EventConsts.MaxUrlLength);
        builder.Property(x => x.OnlineLink).HasMaxLength(EventConsts.MaxOnlineLinkLength);
        builder.Property(x => x.City).HasMaxLength(EventConsts.MaxCityLength);
        builder.Property(x => x.Language).HasMaxLength(EventConsts.MaxLanguageLength);

        builder.HasOne<Organization>().WithMany().HasForeignKey(x => x.OrganizationId).IsRequired().OnDelete(DeleteBehavior.NoAction);
               
        builder.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.IsTimingChangeEmailSent).HasDefaultValue(true);

        builder.HasIndex(x => new {x.OrganizationId, x.StartTime});
        builder.HasIndex(x => x.StartTime);
        builder.HasIndex(x => x.UrlCode);
        builder.HasIndex(x => new {x.IsRemindingEmailSent, x.StartTime});
        builder.HasIndex(x => x.IsEmailSentToMembers);

        builder.HasMany(x => x.Tracks).WithOne().IsRequired().HasForeignKey(x => x.EventId);
        
        builder.ToTable("Event");
    }
}