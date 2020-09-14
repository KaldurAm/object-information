using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;
using PagedList;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class RegionController : BaseController
    {
        public ActionResult Region()
        {
            return View();
        }

        /// <summary>
        /// Метод возвращает справочник Регионов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTableRegions(int contryId)
        {
            try
            {
                var projectsList = db.Regions.Where(w=>w.CountryId == contryId).Select(s => new { s.RegionId, s.RegionName }).ToList();

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
                return Json(new {  recordsTotal = 0, recordsFiltered = 0, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Метод возвращает список стран
        /// </summary>
        /// <param name="value">ID страны</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult getCountryList()
        {
            return Json(db.Countries.Select(s=>new {s.CountryId, s.CountryName}), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод возвращает обхект для отображения
        /// </summary>
        /// <param name="value">ID страны</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult viewForEditRegion(int value)
        {
            return Json(db.Regions.Where(w => w.RegionId == value).Select(s => new
            {
                s.CountryId, s.RegionId, s.RegionName
            
            }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод производит изменения в наименование страны
        /// </summary>
        /// <param name="json">Строка с данными в формате Json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult modifyRegion(string json)
        {
            try
            {
                Region jRegion = JsonConvert.DeserializeObject<Region>(json, new Helper.MyDateTimeConverter());

                Region region = db.Regions.Find(jRegion.RegionId);
                if (region != null)
                {
                    region.RegionName = jRegion.RegionName;
                    region.CountryId = jRegion.CountryId;
                    db.SaveChanges();
                }
                else
                {
                    db.Regions.Add(jRegion);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Метод удаляет регион
        /// </summary>
        /// <param name="value">ID свойства</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleteRegion(int value = 0)
        {
            try
            {
                Region region = db.Regions.Find(value);
                if (region != null)
                {
                    db.Regions.Remove(region);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        [HttpPost]
        public JsonResult GetCountries()
        {
            List<Country> countries = db.Countries.ToList();
            JsonResult jsonResult = Json(countries.Select(s => new
            {
                id = s.CountryId, name = s.CountryName
            
            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }







        public ActionResult DeleteRegion(int regionId = 0)
        {
            if (regionId != 0)
            {
                Region findRegion = db.Regions.Find(regionId);
                if (findRegion == null)
                    return RedirectToAction("Region", new { isErrorMessage = 1, message = "Объект был не найден" });

                try
                {
                    db.Regions.Remove(findRegion);
                    db.SaveChanges();
                    return RedirectToAction("Region",
                        new { isErrorMessage = 0, message = "Удаление прошло успешно." });
                }
                catch (Exception e)
                {
                    return RedirectToAction("Region",
                        new
                        {
                            isErrorMessage = 2,
                            message = e
                        });
                }
            }
            return RedirectToAction("Region", new
            {
                isErrorMessage = 1,
                message = "Данные пришли пустыми."
            });
        }
        public ActionResult AddRegion(Region region)
        {
            if (region == null)
            {
                return RedirectToAction("Region", new { isErrorMessage = 1, Message = "Данные пришли пустыми." });
            }

            Region findRegion = db.Regions.Find(region.RegionId);
            try
            {
                if (findRegion == null)
                {
                    db.Regions.Add(region);
                    db.SaveChanges();
                }
                else
                {
                    findRegion.RegionName = region.RegionName;
                    findRegion.CountryId = region.CountryId;
                    db.Entry(findRegion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Region",
                        new { isErrorMessage = 0, Message = "Данные были изменены успешно." });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Region",
                    new { isErrorMessage = 2, Message = e });
            }
            return RedirectToAction("Region",
                new { isErrorMessage = 0, Message = "Данные были добавлены успешно." });

        }
    }
}