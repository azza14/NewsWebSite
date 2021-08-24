using Microsoft.AspNetCore.Mvc;
using NewsWebSite.Models;
using System.Linq;

namespace NewsWebSite.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class DefaultController : Controller
    {
        private NewsDbContext context;
        public DefaultController(NewsDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            int categoriesCount = context.Categories.Count();
            int teamsCount = context.TemMembers.Count();
            int userCount = context.Contacts.Count();
            int newsCount = context.News.Count();

            return View(new AdminNumbers
            {
                cats = categoriesCount,
                teams = teamsCount,
                user = userCount,
                news = newsCount
            });
        }


    }
    public class AdminNumbers
    {
        public int cats { get; set; }
        public int teams { get; set; }
        public int user { get; set; }
        public int news { get; set; }
    }
}
