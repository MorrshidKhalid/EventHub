using EventHub.Organizations;
using EventHub.Organizations.Memberships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;

namespace EventHub.Configuration;

public class OrganizationMembershipConfiguration : IEntityTypeConfiguration<OrganizationMembership>
{
    public void Configure(EntityTypeBuilder<OrganizationMembership> builder)
    {
        builder.ConfigureByConvention();
        
        builder.HasOne<Organization>().WithMany()
            .HasForeignKey(x => x.OrganizationId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne<IdentityUser>().WithMany()
            .HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(x => new {x.OrganizationId, x.UserId});
        
        builder.ToTable("OrganizationMembership");
    }
}