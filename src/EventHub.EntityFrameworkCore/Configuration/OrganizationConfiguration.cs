using EventHub.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;

namespace EventHub.Configuration;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ConfigureByConvention();
        
        builder.HasOne<IdentityUser>().WithMany()
            .HasForeignKey(x => x.OwnerUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(OrganizationConsts.MaxNameLength);
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(OrganizationConsts.MaxDescriptionNameLength);
        
        builder.Property(x => x.DisplayName)
            .IsRequired()
            .HasMaxLength(OrganizationConsts.MaxDisplayNameLength);
        
        builder.Property(x => x.Website)
            .IsRequired(false)
            .HasMaxLength(OrganizationConsts.MaxWebsiteLength);
        
        builder.Property(x => x.TwitterUsername)
            .IsRequired(false)
            .HasMaxLength(OrganizationConsts.MaxTwitterUsernameLength);
        
        builder.Property(x => x.GitHubUsername)
            .IsRequired(false)
            .HasMaxLength(OrganizationConsts.MaxGitHubUsernameLength);
        
        builder.Property(x => x.FacebookUsername)
            .IsRequired(false)
            .HasMaxLength(OrganizationConsts.MaxFacebookUsernameLength);
        
        builder.Property(x => x.InstagramUsername)
            .IsRequired(false)
            .HasMaxLength(OrganizationConsts.MaxInstagramUsernameLength);
        
        builder.Property(x => x.MediumUsername)
            .IsRequired(false)
            .HasMaxLength(OrganizationConsts.MaxMediumUsernameLength);

        builder.Property(x => x.MemberCount)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(x => x.IsSendPaidEnrollmentReminderEmail)
            .HasDefaultValue(false);

        builder.ToTable("Organization");

    }
}