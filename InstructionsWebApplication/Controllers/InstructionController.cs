using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstructionsWebApplication.Data;
using InstructionsWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Korzh.EasyQuery.Linq;

namespace InstructionsWebApplication.Controllers
{
    public class InstructionController : Controller
    {

        private ApplicationDbContext _db;

        private UserManager<ApplicationUser> _userManager;

        //private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public InstructionController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        // GET: Instruction
        public ActionResult Index()
        {
            return View();
        }

        // GET: Instruction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Instruction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instruction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
                // TODO: Add insert logic here
                _db.Instructions.Add(new Instruction { Tags = collection["Tags"],Description = collection["Description"], User = user, Pages = new List<Page>() ,ImageURL = "http://s1.1zoom.me/b5050/137/371625-svetik_1440x900.jpg" });
                _db.SaveChanges();

                return RedirectToAction("ShowAllInstruction");
            }
            catch
            {
                return View();
            }
        }

        // GET: Instruction/Edit/5
        public ActionResult Edit(string InstructionId)
        {
            var currentInstruction = _db.Instructions.SingleOrDefault(i=>i.InstructionId == InstructionId);
            var currentPages = _db.Pages.Include(p=>p.Instruction).Where(p=>p.Instruction == currentInstruction);
            ViewBag.InstructionId = InstructionId;
           
            return View(currentPages);
          
        }

        // POST: Instruction/Edit/5
      

        // GET: Instruction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Instruction/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch                                                                                            
            {
                return View();
            }
        }

        public ActionResult ShowAllInstruction()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            var instructions = _db.Instructions.Include(c => c.User).Where(c => c.User == user);
            return View(instructions.ToList());
        }

        public ActionResult AddPage(string InstructionId)
        {
            Instruction currentInstruction = _db.Instructions.SingleOrDefault(i=>i.InstructionId == InstructionId);
            _db.Pages.Add(new Page { Instruction = currentInstruction,Text = "",ImageURL = "http://www.clipartbest.com/cliparts/aiq/x7q/aiqx7qXAT.jpg" });
            _db.SaveChanges();
            return RedirectToAction("Edit","Instruction", new {InstructionId = InstructionId});
        }

        
        public ActionResult EditPage(string InstructionId,string PageId)
        {
            var currentPage = _db.Pages.SingleOrDefault(p=>p.PageId == PageId);
            return View(currentPage);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditPage(string InstructionId, string PageId, IFormCollection collection)
        {
            var currentPage = _db.Pages.SingleOrDefault(p => p.PageId == PageId);
            currentPage.Text = collection["Text"];
            currentPage.ImageURL = collection["ImageURL"];
            _db.SaveChanges();
            return RedirectToAction("Edit", "Instruction", new { InstructionId = InstructionId });
        }

        public IActionResult ShowUserInstruction()
        {
            return View();
        }



        







    }
}