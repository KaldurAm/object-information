using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using ObjectInformation.DAL;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class TaskController : BaseController, IDisposable
    {
        [HttpGet]
        public async Task<ActionResult> Index(int objectId)
        {
            var objectTasks = db.Tasks.Where(x => x.ObjectRealtyId == objectId)
                .Include(i => i.TaskStatus)
                .Include(i => i.Responsibles)
                .OrderBy(o => o.Deadline)
                .ToList();
            var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            ViewBag.Users = await userManager.Users.ToListAsync();
            return View(objectTasks);
        }

        [HttpGet]
        public async Task<ActionResult> CreateTask(int objectId)
        {
            var newTask = new DAL.Model.Task
            {
                ObjectRealtyId = objectId,
                StatusId = (int)TaskStatusEnum.СозданаЗадача,
                Creator = User.Identity.Name,
                DaysCount = 1,
                Responsibles = new List<TaskResponsible>(),
                CreateDate = DateTime.Now,
                Files = new List<TaskFile>()
            };
            var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            ViewBag.Users = await userManager.Users.ToListAsync();
            return PartialView(newTask);
        }

        [HttpGet]
        public async Task<string> AddUser(int taskId, string userId)
        {
            var responsiveUser = new TaskResponsible
            {
                ResponsibleUserId = userId,
                TaskId = taskId
            };

            using (var taskService = new TaskService())
            {
                await taskService.AddResponsibleUser(responsiveUser);
            }

            var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            return (await userManager.FindByIdAsync(userId)).Email;
        }

        [HttpPost]
        public async Task<ActionResult> Create(string task, string[] users)
        {
            DAL.Model.Task newTask = JsonConvert.DeserializeObject<DAL.Model.Task>(task);
            using (var taskService = new TaskService())
            {
                var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                newTask.Creator = userManager.FindByName(User.Identity.Name).Id;
                await taskService.Create(newTask, users);
            }

            return RedirectToAction("Index", "Task", new { objectId = newTask.ObjectRealtyId });
        }

        [HttpGet]
        public async Task<string> SearchUsers(string key)
        {
            var userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            var userList = await userManager.Users.Where(x => x.Email.Contains(key)).Select(s => new { 
                id = s.Id,
                text = s.UserName
            }).ToListAsync();
            return JsonConvert.SerializeObject(userList);
        }
    }
}