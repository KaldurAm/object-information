using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       protected OInformation db = new OInformation();
    }
}