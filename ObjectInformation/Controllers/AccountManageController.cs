using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObjectInformation.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AccountManageController : Controller
    {
        // GET: AccountManage
        public ActionResult Index()
        {
            return View();
        }
    }
}