using EventHub.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EventHub.Configuration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ConfigureByConvention();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(CountryConsts.MaxNameLength);

        builder.HasIndex(x => new {x.Name});
        
        builder.ToTable("Country");
    }
}