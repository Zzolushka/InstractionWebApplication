using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InstructionsWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using InstructionsWebApplication.Data;
using Korzh.EasyQuery.Linq;
using InstructionsWebApplication.Models.HomeViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace InstructionsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        private UserManager<ApplicationUser> _userManager;

        //private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var instruction = _db.Instructions;
            var model = new IndexViewModel()
            {
                Instructions = instruction
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            if(!string.IsNullOrEmpty(model.Text))
            {
                model.Instructions = _db.Instructions.FullTextSearchQuery(model.Text);
            }
            else
            {
                model.Instructions = _db.Instructions;
            }

            return View(model);
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Chat()
        {
            return View();
        }
        //public IActionResult ShowUserInformation()
        //{

        //}

        public IActionResult AdminPanel()
        {
            //_userManager.AddToRoleAsync();
            return View();
        }

        //[Authorize]
        //public ActionResult ShowCurrnetUserInformation()
        //{
        //    var userId = _userManager.GetUserId(HttpContext.User);
        //    var user = _userManager.Users.Include(u => u.Instructions).SingleOrDefault(u => u.Id == userId);

        //    ViewBag.UserName = user.NormalizedUserName;
        //    ViewBag.UserId = user.Id;
        //    return View(user.Instructions.ToList());
        //}

        
        public ActionResult ShowUserInformation(string userId)
        {
            var user = _userManager.Users.Include(u => u.Instructions).SingleOrDefault(u => u.Id == userId);

            var currentUserId = _userManager.GetUserId(User);
            
            ViewBag.UserName = user.NormalizedUserName;

            ViewBag.UserId = user.Id;

            if (user.Id == currentUserId || User.IsInRole("Admin"))
                return View(user.Instructions.ToList());
            else
              return RedirectToAction("ShowNotFullUserInformation", new { UserId = userId });
        }

       

        public ActionResult ShowNotFullUserInformation(string userId)
        {
            var user = _userManager.Users.Include(u => u.Instructions).SingleOrDefault(u => u.Id == userId);

            ViewBag.UserName = user.NormalizedUserName;

            return View(user.Instructions.ToList());
        }

        public IActionResult ChangeSettings()
        {
            return View();
        }






    }
}
