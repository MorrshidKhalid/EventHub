using EventHub.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;

namespace EventHub.Configuration;

public class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder.ConfigureByConvention();

        builder.HasKey(x => new {x.SessionId, x.UserId});

        builder.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired();
        
        builder.ToTable("EventSpeakers");
    }
}