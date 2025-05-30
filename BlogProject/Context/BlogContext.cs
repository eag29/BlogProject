using BlogProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Context
{
    public class BlogContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MSI; Database=BlogDb; Integrated Security=True; TrustServerCertificate=True;");
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
