using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Identity
{
    public class BlogIdentityDbContext : IdentityDbContext<BlogIdentityUser, BlogIdentityRole, string>
    {
        public BlogIdentityDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
