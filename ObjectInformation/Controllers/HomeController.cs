using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ObjectInformation.DAL;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var s = dbContext.Users.FirstOrDefault(f => f.UserName == User.Identity.Name);
            var countryList = db.Countries.ToList();
            return View(countryList);
        }




        public ContentResult Squere()
        {
            return Content("<h2>Площадб треугольника с оноввнием а</h2>");
        }

        public ViewResult Test1()
        {
            return View(@"~\Views\Country\Country.cshtml");
        }
        
        public ActionResult About()
        {
            City city = new City();
            city.CityName = "Almaty";
            TempData["CityModel"] = city;

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            var city = (City)TempData["CityModel"];
            TempData.Keep("CityModel");

            var city2 = (City)TempData.Peek("CityModel");

            ViewBag.Message = "Your contact page.";

            //return RedirectToAction("");
            return View();
        }

        [HttpPost]
        public JsonResult GetCity(int cityId)
        {
            OInformation db = new OInformation();

            List<City> data = new List<City>();
            data.Add(new City() { CityName = "Almaty" });
            data.Add(new City() { CityName = "Stana" });
            //var data = db.Cities.ToList();
            return Json(data);
        }

        public RedirectResult Redirect(string URL)
        {
            return Redirect(URL);
        }

        public RedirectResult Redirect2()
        {
            return RedirectPermanent("View/somePage.cshtml");
        }

        public RedirectToRouteResult Redirect3()
        {
            //return RedirectToAction("Test", new {obj= djhfkjsh});
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index",
                ID = "MyID"
            });
        }

        [HttpPost]
        public ActionResult ObjectRealtyListInfo(int districtId, int objectTypeId)
        {
            var objectRealties = ServiceObjectRealty.GetObjectRealties().Where(w => w.DistrictId == districtId && w.ObjectTypeId == objectTypeId).ToList();

            return PartialView(objectRealties);
        }

        [HttpGet]
        public ActionResult PartialComments(int objectRealtyId)
        {
            var obModel = new ObjectModel(objectRealtyId);
            return PartialView(obModel);
        }

        [HttpPost]
        public ActionResult AddComment(int ObjectRealtyId, string CommentText)
        {
            Comment comment = new Comment()
            {
                ObjectRealtyId = ObjectRealtyId,
                CommentText = CommentText,
                UserId = User.Identity.Name,
                CommentDateTime = DateTime.Now,
            };

            db.Comments.Add(comment);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex){}
            return PartialView(comment);
        }
    }
}