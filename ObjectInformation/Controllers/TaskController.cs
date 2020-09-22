using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    public class TaskController : Controller, IDisposable
    {
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
            var userList = await userManager.Users.ToListAsync();
            ViewBag.UserList = userList;
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
        public async Task<ActionResult> Create(DAL.Model.Task newTask)
        {
            using (var taskService = new TaskService())
            {
                newTask.Creator = User.Identity.GetUserId();
                await taskService.Create(newTask);
            }

            return RedirectToAction("Index", "ObjectRealty");
        }
    }
}