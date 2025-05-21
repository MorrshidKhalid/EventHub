using EventHub;
using EventHub.Events;
using EventHub.Tracks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EventHubuilder.Configuration;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ConfigureByConvention();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(TrackConsts.MaxNameLength);

        builder.HasMany(x => x.Sessions).WithOne().IsRequired().HasForeignKey(x => x.TrackId);
        
        builder.ToTable("EventTracks");
    }
}