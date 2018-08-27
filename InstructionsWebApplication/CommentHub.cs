
using InstructionsWebApplication.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstructionsWebApplication
{
    public class CommentHub : Hub
    {
        private ApplicationDbContext _db;


        //private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CommentHub(ApplicationDbContext db)
        {
            _db = db;    
        }

        public async Task AddComment(string comment, string userURL, string id, string UserName)
        {
            await Clients.All.SendAsync("ReceiveMessage", comment,userURL,UserName);
            _db.Instructions.Include(i => i.Comments).SingleOrDefault(i=>i.InstructionId == id).Comments.Add(new Models.Comment {Text = comment,UserURL = userURL,UserName = UserName});
            await _db.SaveChangesAsync();
        }

       
    }
}
