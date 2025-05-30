using BlogProject.Context;
using BlogProject.Identity;
using BlogProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogContext _context;
        private readonly SignInManager<BlogIdentityUser> _signInManager;

        public BlogController(BlogContext context, SignInManager<BlogIdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var blogs = _context.Blogs.Where(x => x.Status == true).ToList();
            return View(blogs);
        }
        public IActionResult Detail(int id)
        {
            var comments = _context.Comments.Where(x => x.BlogID == id).ToList();
            ViewBag.Comment = comments.ToList();

            var details = _context.Blogs.Where(x => x.ID == id).FirstOrDefault();
            details.ViewCount += 1;
            _context.SaveChanges();

            return View(details);
        }
        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {
            comment.PublishDate = DateTime.Now;
            _context.Comments.Add(comment);

            var commentCount = _context.Blogs.Where(x => x.ID == comment.BlogID).FirstOrDefault();
            commentCount.CommentCount += 1;

            _context.SaveChanges();
            return RedirectToAction("Detail", new { id = comment.BlogID });
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Support()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateContact(Contact contact)
        {
            contact.CreatedDate = DateTime.Now;
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
