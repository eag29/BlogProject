using Microsoft.AspNetCore.Identity;

namespace BlogProject.Identity
{
    public class BlogIdentityUser: IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
