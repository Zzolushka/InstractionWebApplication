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
using InstructionsWebApplication.Models.InstructionViewModels;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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
        [HttpGet]
        public ActionResult Create(string UserId)
        {
            return View();
        }

        // POST: Instruction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string UserId, IFormCollection collection)
        {
            try
            {
                
                _userManager.Users.Include(u=>u.Instructions).SingleOrDefault(u => u.Id == UserId).Instructions.Add(new Instruction { Tags = collection["Tags"], Description = collection["Description"], ImageURL = collection["ImageURL"],Category = collection["Category"],Name = collection["Name"] });
                
                _db.SaveChanges();

                return RedirectToAction("showuserinformation", "home", new { UserId = UserId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string InstructionId)
        { 
            var instruction = _db.Instructions.Include(i => i.Pages).Include(i => i.Comments).SingleOrDefault(i => i.InstructionId == InstructionId);
            ViewBag.InstructionId = InstructionId;
            ViewBag.InstructionName = instruction.Name;
            return View(instruction.Pages);
        }

        public ActionResult Delete(string UserId,string InstructionId)
        {
            var instruction = _db.Instructions.Include(i=>i.Pages).Include(i=>i.Comments).SingleOrDefault(i=>i.InstructionId == InstructionId);
            _db.Instructions.Remove(instruction);

            _db.SaveChanges();
            return  RedirectToAction("showuserinformation","home",new { UserId = UserId});
        }

        public ActionResult AddPage(string InstructionId)
        {
            _db.Instructions.Include(i => i.Pages).Include(i => i.Comments).SingleOrDefault(i => i.InstructionId == InstructionId).Pages.Add(new Page { Text = "", ImageURL = "http://www.clipartbest.com/cliparts/aiq/x7q/aiqx7qXAT.jpg" });
            _db.SaveChanges();
            return RedirectToAction("Edit","Instruction", new {InstructionId = InstructionId});
        }

        
        public ActionResult EditPage(string InstructionId,string PageId)
        {
            var page = _db.Pages.SingleOrDefault(p=>p.PageId == PageId);
            ViewBag.PageId = PageId;
            return View(page);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditPage(string InstructionId, string PageId, string ImageURL,  IFormCollection collection)
        {

            var page = _db.Pages.SingleOrDefault(p => p.PageId == PageId);
            page.Text = collection["Text"];
            page.ImageURL = collection["ImageURL"];

            _db.SaveChanges();
            return RedirectToAction("Edit", "Instruction", new { InstructionId = InstructionId });
        }

        public ActionResult ShowInstruction(string InstructionId)
        {
            var currentInstruction = _db.Instructions.Include(i=>i.Pages).Include(i => i.Comments).SingleOrDefault(i=>i.InstructionId ==InstructionId);

            var UserId = _userManager.GetUserId(User);
            var UserName = _userManager.GetUserName(User);

            ViewBag.InstructionId = InstructionId;

            ViewBag.UserId = UserId;

            ViewBag.UserName = UserName;

            ViewBag.InstructionName = currentInstruction.Name;


            var model = new ShowInstructionModel()
            {
                Pages = currentInstruction.Pages,
                Comments = currentInstruction.Comments
            };

            return View(model);
        }












      





















    }
}