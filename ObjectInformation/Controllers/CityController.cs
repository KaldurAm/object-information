using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;
using PagedList;

namespace ObjectInformation.Controllers
{
    public class CountryAdd
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public List<Region> Regions = new List<Region>();
    }

    [Authorize]
    public class CityController : BaseController, IDisposable
    {
        public ActionResult City()
        {
            ViewBag.Countries = db.Countries.ToList();
            ViewBag.Cities = db.Cities.ToList();
            return View();
        }

        /// <summary>
        /// Метод возвращает список стран
        /// </summary>
        /// <param name="value">ID страны</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult getCountries()
        {
            var country = db.Countries.ToList();

            return Json(country.Select(s => new{s.CountryId,s.CountryName}), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод возвращает список стран
        /// </summary>
        /// <param name="value">ID страны</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult getRegions(int countryId)
        {
            var regions = db.Regions.Where(w => w.CountryId == countryId).ToList();

            return Json(regions.Select(w => new
            {
                w.RegionName, w.RegionId
            
            }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод возвращает справочник Городов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTableCity(int contryId, int regionId)
        {
            try
            {
                var projectsList = db.Cities.Where(w => w.RegionId == regionId).Select(s => new
                {
                    s.RegionId,
                    s.CityId,
                    s.CityName
                }).ToList();

                int recordsCount = projectsList.Count();
                int pageSize = int.Parse(Request["length"]);

                pageSize = pageSize < 0 ? recordsCount : pageSize;
                int fromRow = int.Parse(Request["start"]);
                int sEcho = int.Parse(Request["draw"]);
                int pageNumber = (fromRow + pageSize) / pageSize;
                if (pageNumber == 0) pageNumber = 1;

                return Json(new { draw = sEcho, recordsTotal = recordsCount, recordsFiltered = recordsCount, data = projectsList.ToPagedList(pageNumber, pageSize) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { recordsTotal = 0, recordsFiltered = 0, data = "" }, JsonRequestBehavior.AllowGet);
            }
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

        /// <summary>
        /// Метод производит изменения в наименование города
        /// </summary>
        /// <param name="json">Строка с данными в формате Json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult modifyCity(string json)
        {
            try
            {
                City jCity = JsonConvert.DeserializeObject<City>(json, new Helper.MyDateTimeConverter());

                City city = db.Cities.Find(jCity.CityId);
                if (city != null)
                {
                    city.CityName = jCity.CityName;
                    city.RegionId = jCity.RegionId;
                    db.SaveChanges();
                }
                else
                {
                    db.Cities.Add(jCity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Метод удаляет город
        /// </summary>
        /// <param name="value">ID свойства</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleteCity(int value = 0)
        {
            try
            {
                City city = db.Cities.Find(value);
                if (city != null)
                {
                    db.Cities.Remove(city);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        /// <summary>
        /// Метод возвращает обхект для отображения
        /// </summary>
        /// <param name="value">ID страны</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult viewForEditCity(int value)
        {
            return Json(db.Cities.Where(w => w.CityId == value).Select(s => new
            {
                s.CityId,
                s.RegionId,
                s.CityName

            }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
    }
}