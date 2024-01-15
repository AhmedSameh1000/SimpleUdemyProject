using Microsoft.AspNetCore.Identity;
using SimpleEcommerce.Infrastructure.RepositoryImplementation;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Domain.Entities;

namespace UdemyProject.Infrastructure.Seeding
{
    public class SeedAdminWithRolesinitialData
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserProfileRepository _UserProfileRepository;

        public SeedAdminWithRolesinitialData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IUserProfileRepository userProfileRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _UserProfileRepository = userProfileRepository;
        }

        public async Task SeedData()
        {
            if (_roleManager.Roles.Any())
            {
                return;
            }

            var UserRole = new IdentityRole()
            {
                Id = "1",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = ""
            };
            var AdminRole = new IdentityRole()
            {
                Id = "2",

                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = ""
            };
            await _roleManager.CreateAsync(UserRole);
            await _roleManager.CreateAsync(AdminRole);

            var UserToSeed = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "Admin@gmail.com",
                UserName = "Admin@gmail.com",
                Name = "Admin",
                EmailConfirmed = true,
            };
            var Result = await _userManager.CreateAsync(UserToSeed, "ahmeds1490");

            if (Result.Succeeded)
            {
                await _userManager.AddToRolesAsync(UserToSeed, new List<string> { UserRole.Name, AdminRole.Name });
            }

            var UdemyProfile = new UserProfile()
            {
                applicationUserId = UserToSeed.Id,
                FullName = UserToSeed.Name,
            };
            await _UserProfileRepository.Add(UdemyProfile);
            await _UserProfileRepository.SaveChanges();
        }
    }
}