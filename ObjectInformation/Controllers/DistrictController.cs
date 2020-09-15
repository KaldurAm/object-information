using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;
using PagedList;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class DistrictController : BaseController
    {
        // GET: District
        public ActionResult Index()
        {
            ViewBag.Countries = db.Countries.ToList();
            ViewBag.Cities = db.Cities.ToList();
            ViewBag.Districts = db.Districts.ToList();

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

            return Json(country.Select(s => new { s.CountryId, s.CountryName }), JsonRequestBehavior.AllowGet);
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
                w.RegionName,
                w.RegionId

            }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод возвращает список городов
        /// </summary>
        /// <param name="value">ID страны</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult getCyties(int regionId)
        {
            var cities = db.Cities.Where(w => w.RegionId == regionId).ToList();

            return Json(cities.Select(w => new
            {
                w.CityId,
                w.CityName,
                w.RegionId
            }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод возвращает справочник Городов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTableDistrict(int cityId)
        {
            try
            {
                var projectsList = db.Districts.Where(w => w.CityId == cityId).Select(s => new
                {
                    s.DistrictId,
                    s.DistrictName,
                    s.CityId
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
        public JsonResult GetCities()
        {
            List<City> cities = db.Cities.ToList();
            JsonResult jsonResult = Json(cities.Select(s => new
            {
                id = s.CityId,
                name = s.CityName

            }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        /// <summary>
        /// Метод производит изменения в наименование города
        /// </summary>
        /// <param name="json">Строка с данными в формате Json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult modifyDistrict(string json)
        {
            try
            {
                District jDistrict = JsonConvert.DeserializeObject<District>(json, new Helper.MyDateTimeConverter());

                District district = db.Districts.Find(jDistrict.DistrictId);
                if (district != null)
                {
                    district.DistrictName = jDistrict.DistrictName;
                    district.CityId = jDistrict.CityId;
                    db.SaveChanges();
                }
                else
                {
                    db.Districts.Add(jDistrict);
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
        public ActionResult deleteDistrict(int value = 0)
        {
            try
            {
                District district = db.Districts.Find(value);
                if (district != null)
                {
                    db.Districts.Remove(district);
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
        public JsonResult viewForEditDistrict(int value)
        {
            return Json(db.Districts.Where(w => w.DistrictId == value).Select(s => new
            {
                s.DistrictId,
                s.DistrictName,
                s.CityId

            }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateDistrict(District district)
        {

            try
            {
                db.Districts.Add(district);
                db.SaveChanges();
                return RedirectToAction("Create", "District", new { status = 0, message = "Запись успешно добавлена" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Create", "District", new { status = 2, message = e.ToString() });
            }
        }

        public ActionResult Edit(int districtId)
        {
            District findedDistrict = db.Districts.Find(districtId);
            if (findedDistrict != null)
            {
                ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
                return View(findedDistrict);
            }
            return RedirectToAction("Create", "District", new { status = 1, message = "Данные пришли пустыми." });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(District district)
        {
            try
            {
                db.Entry(district).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "District", new { status = 0, message = "Данные были изменены успешно." });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "District", new { status = 2, message = e.ToString() });
            }
        }
        public ActionResult Details()
        {
            return RedirectToAction("Index", "District");
        }
        public async Task<ActionResult> Delete(int districtId = 0)
        {
            District findedDistrict = await db.Districts.FindAsync(districtId);
            int indexStatus;
            string indexMessage;
            if (findedDistrict != null && districtId != 0)
            {
                if (findedDistrict.ObjectRealties.Any())
                {
                    indexStatus = 1;
                    indexMessage = "Вы не можете удалить данную запись , так как на нее ссылаются куча других записей";
                }
                else
                {
                    try
                    {
                        db.Districts.Remove(findedDistrict);
                        await db.SaveChangesAsync();
                        indexStatus = 0;
                        indexMessage = "Данные были удалены успешно.";
                    }
                    catch (Exception e)
                    {
                        indexStatus = 2;
                        indexMessage = e.ToString();
                    }
                }
            }
            else
            {
                indexStatus = 1;
                indexMessage = "Данные пришли пустыми";
            }


            return RedirectToAction("Index", "District", new { status = indexStatus, message = indexMessage });
        }
    }
}