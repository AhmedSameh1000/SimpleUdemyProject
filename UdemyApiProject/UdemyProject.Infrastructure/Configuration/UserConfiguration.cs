using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
               .HasOne(u => u.userProfile)
               .WithOne(up => up.applicationUser)
               .HasForeignKey<UserProfile>(up => up.applicationUserId);
        }
    }
}