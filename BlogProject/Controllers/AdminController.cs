using BlogProject.Context;
using BlogProject.Identity;
using BlogProject.Models;
using BlogProject.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly BlogContext _context;
        private readonly UserManager<BlogIdentityUser> _usermanager;
        private readonly SignInManager<BlogIdentityUser> _signInManager;

        public AdminController(BlogContext blogcontext, UserManager<BlogIdentityUser> usermanager, SignInManager<BlogIdentityUser> signInManager)
        {
            _context = blogcontext;
            _usermanager = usermanager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var toplamblogSayisi = _context.Blogs.Count();
            ViewBag.toplamBlogSayisi = toplamblogSayisi;

            var toplamgoruntulenmeSayisi = _context.Blogs.Select(x => x.ViewCount).Sum();
            ViewBag.toplamGoruntulenmeSayisi = toplamgoruntulenmeSayisi;

            var encokGoruntulenBlog = _context.Blogs.OrderByDescending(x => x.ViewCount).FirstOrDefault();
            ViewBag.enCokGoruntulenenBlog = encokGoruntulenBlog;

            var ensonYayinlananBlog = _context.Blogs.OrderByDescending(x => x.PublishDate).FirstOrDefault();
            ViewBag.enSonYayinlananBlog = ensonYayinlananBlog;

            var encokYorumAlanBlogId = _context.Comments.GroupBy(x => x.BlogID).OrderByDescending(x => x.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.enCokYorumAlanBlogId = encokYorumAlanBlogId;

            var bugunyapilanYorumSayisi = _context.Comments.Where(x => x.PublishDate == DateTime.Today).Count();
            ViewBag.bugunYapilanYorumSayisi = bugunyapilanYorumSayisi;

            return View();
        }
        public IActionResult Blogs()
        {
            var blogs = _context.Blogs.ToList();
            return View(blogs);
        }
        public IActionResult EditBlog(int id)
        {
            var blogs = _context.Blogs.Where(x => x.ID == id).FirstOrDefault();
            return View(blogs);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            var blogs = _context.Blogs.Where(x => x.ID == blog.ID).FirstOrDefault();
            blogs.Name = blog.Name;
            blogs.Description = blog.Description;
            blogs.Tags = blog.Tags;
            blogs.ImageUrl = blog.ImageUrl;
            blogs.Status = blog.Status;
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }
        public IActionResult ToggleStatus(int id)
        {
            var blogs = _context.Blogs.Where(x => x.ID == id).FirstOrDefault();

            if (blogs.Status == true)
            {
                blogs.Status = false;
            }
            else
            {
                blogs.Status = true;
            }
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }
        public IActionResult DeleteBlog(int id)
        {
            var blogs = _context.Blogs.Where(x => x.ID == id).FirstOrDefault();
            _context.Remove(blogs);
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }
        public IActionResult CreateBlog()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBlog(Blog blog)
        {
            blog.PublishDate = DateTime.Now;
            blog.Status = true;
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }
        public IActionResult Comments(int? blogId)
        {
            var comments = new List<Comment>();

            if (blogId == null)
            {
                comments = _context.Comments.ToList();
            }
            else
            {
                comments = _context.Comments.Where(x => x.BlogID == blogId).ToList();
            }

            return View(comments);
        }
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.Where(x => x.ID == id).FirstOrDefault();
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Comments");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Password == model.Repassword)
            {
                var user = new BlogIdentityUser
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _usermanager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Blog");
        }
        public IActionResult Contact()
        {
            var contacts =_context.Contacts.ToList();
            return View(contacts);
        }
    }
}
