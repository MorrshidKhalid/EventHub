using EventHub.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EventHub.Configuration;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {

        builder.ConfigureByConvention();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(SessionConsts.MaxTitleLength);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(SessionConsts.MaxDescriptionLength);
        builder.Property(x => x.Language).IsRequired().HasMaxLength(SessionConsts.MaxLanguageLength);
               
        builder.HasMany(x => x.Speakers).WithOne().IsRequired().HasForeignKey(x => x.SessionId);
        
        builder.ToTable("EventSessions");
    }
}