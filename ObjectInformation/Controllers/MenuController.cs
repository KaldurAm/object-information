using System.Web.Mvc;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class MenuController : BaseController
    {     
        public ActionResult Index()
        {
            return View();
        }                  
    }
}