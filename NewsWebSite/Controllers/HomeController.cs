using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsWebSite.Models;
using NewsWebSite.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NewsWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private NewsDbContext context;

        public HomeController(ILogger<HomeController> logger, NewsDbContext _context)
        {
            _logger = logger;
            context = _context;
        }


        public IActionResult Index()
        {
            var tupleModel = new Tuple<List<New>, List<Category>>(GetItemNews(), GetCategory());
            return View(tupleModel);
        }

        private List<Category> GetCategory()
        {
            var listCategories = context.Categories.ToList();
            return listCategories;
        }

        private List<New> GetItemNews()
        {
            return context.News.ToList();
        }

        //  [Authorize]
        public IActionResult News(int id)
        {
            var category = context.Categories.Find(id);
            var news = context.News.Where(n => n.CategoryId == id).FirstOrDefault();
            var viewmodel = new NewsViewModel()
            {
                News= news
            };
            ViewBag.NameCategory = category.Name;//.OrderByDescending(c=>c.Date)
           
            ViewBag.categoriesCount = context.Categories.Count();
            ViewBag.teamsCount = context.TemMembers.Count();
            ViewBag.userCount = context.Contacts.Count();
            ViewBag.newsCount = context.News.Count();
            ViewBag.CategoryId = category.Id;
            return View(viewmodel);
        }
        public IActionResult GetNewsRelatedCategory(int CategoryId)
        {
            var newItem= context.News.Find(CategoryId);
            var categoryid = newItem.CategoryId;
            var news = context.News.Where(n => n.CategoryId == categoryid).ToList();
            return PartialView("GetNewsRelatedCategory", news);
        }

        public IActionResult TeamsIndex()
        {
            var Teams = context.TemMembers.ToList();
            return View(Teams);
        }


        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveContact(ContactUs model)
        {
            if (ModelState.IsValid)
            {
                context.Contacts.Add(model);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Contact", model);
            }
        }

        public IActionResult ShowMessage()
        {
            return View(context.Contacts.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
