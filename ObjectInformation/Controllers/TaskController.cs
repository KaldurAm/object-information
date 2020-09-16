using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ObjectInformation.DAL;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ObjectInformation.Controllers
{
    public class TaskController : Controller
    {
        public async Task<ActionResult> Index(int objectRealtyId)
        {
            var newTask = new DAL.Model.Task();
            newTask.ObjectRealtyId = objectRealtyId;
            newTask.Creator = User.Identity.GetUserId();
            var userManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            var userList = await userManager.UserManager.Users.Select(s => 
                new UserInfoModelView { 
                    Id = s.Id,
                    Email = s.Email
                }).ToListAsync();
            ViewBag.Users = userList;
            return View(newTask);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(DAL.Model.Task newTask)
        {
            using (var taskService = new TaskService())
            {
                newTask.Creator = User.Identity.GetUserId();
                await taskService.Create(newTask);
            }
            return RedirectToAction(nameof(Index), "ObjectRealty");
        }
    }
}