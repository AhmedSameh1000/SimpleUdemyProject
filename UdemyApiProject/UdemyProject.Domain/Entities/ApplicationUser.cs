using Microsoft.AspNetCore.Identity;

namespace UdemyProject.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public UserProfile userProfile { get; set; }
        public List<UserRefreshToken>? RefreshTokens { get; set; }

        public List<Course> coursesInrollments { get; set; }

        public List<Course> CoursesICreated { get; set; }
        public List<Cart> carts { get; set; }
        public List<CartItem> cartItems { get; set; }
    }
}