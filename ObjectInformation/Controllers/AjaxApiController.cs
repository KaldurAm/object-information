using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class AjaxApiController : BaseController
    {
        [HttpPost]
        public JsonResult GetCountry()
        {
            List<Country> regions = db.Countries.ToList();
            JsonResult jsonResult = Json(regions.Select(s => new
            {
                id = s.CountryId,
                name = s.CountryName

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }


        [HttpPost]
        public JsonResult GetRegions()
        {
            List<Region> regions = db.Regions.ToList();
            JsonResult jsonResult = Json(regions.Select(s => new
            {
                id = s.RegionId,
                name = s.RegionName

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetRegionsInCountry(int countryId)
        {
            List<Region> regions = db.Regions.Where(w=>w.CountryId == countryId).ToList();
            JsonResult jsonResult = Json(regions.Select(s => new
            {
                id = s.RegionId,
                name = s.RegionName

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }


        [HttpPost]
        public JsonResult GetCity()
        {
            List<City> cities = db.Cities.ToList();
            JsonResult jsonResult = Json(cities.Select(s => new
            {
                id = s.CityId,
                name = s.CityName

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetCityInRegion(int regionId)
        {
            List<City> cities = db.Cities.Where(w=>w.RegionId== regionId).ToList();
            JsonResult jsonResult = Json(cities.Select(s => new
            {
                id = s.CityId,
                name = s.CityName

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }


        [HttpPost]
        public JsonResult GetDistrict()
        {
            List<District> distrrict = db.Districts.ToList();
            JsonResult jsonResult = Json(distrrict.Select(s => new
            {
                id = s.DistrictId,
                name = s.DistrictName

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetDistrictIdInCity(int cityId)
        {
            List<District> distrrict = db.Districts.Where(w => w.CityId == cityId).ToList();
            JsonResult jsonResult = Json(distrrict.Select(s => new
            {
                id = s.DistrictId,
                name = s.DistrictName

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetProperty()
        {
            List<Property> property = db.Properties.ToList();
            JsonResult jsonResult = Json(property.Select(s => new
            {
                id = s.PropertyId,
                name = s.Name + "("+s.Unit.Name+")"

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }


        [HttpPost]
        public JsonResult GetObjectTypes()
        {
            List<ObjectType> objectTypes = db.ObjectTypes.ToList();
            JsonResult jsonResult = Json(objectTypes.Select(s => new
            {
                id = s.ObjectTypeId,
                name = s.ObjectTypeName
            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetCurrency()
        {
            List<Currency> currency = db.Currency.ToList();
            JsonResult jsonResult = Json(currency.Select(s => new
            {
                id = s.CurrencyId,
                name = s.Name
            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetDocumentType()
        {
            List<DocumentType> docType = db.DocumentTypes.ToList();
            JsonResult jsonResult = Json(docType.Select(s => new
            {
                id = s.DocumentTypeId,
                name = s.DocumentTypeName
            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult GetPledgers()
        {
            List<Pledgers> pledgers = db.Pledgers.ToList();
            JsonResult jsonResult = Json(pledgers.Select(s => new
            {
                id = s.PledgersId,
                name = s.NameOfPledger
            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
    }

}