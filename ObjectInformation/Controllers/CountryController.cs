using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;
using PagedList;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class CountryController : BaseController
    {
        public ActionResult Country()
        {
            return View();
        }

        /// <summary>
        /// Метод возвращает справочник Стран
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTableCountries()
        {
            try
            {
                var projectsList = db.Countries.Select(s=>new {s.CountryId, s.CountryName}).ToList();

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
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Метод возвращает обхект для отображения
        /// </summary>
        /// <param name="value">ID страны</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult viewForEditCountry(int value)
        {
            return Json(db.Countries.Where(w=>w.CountryId == value).Select(s=>new {s.CountryId, s.CountryName}).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод производит изменения в наименование страны
        /// </summary>
        /// <param name="json">Строка с данными в формате Json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult modifyCountry(string json)
        {
            try
            {
                Country jCountry = JsonConvert.DeserializeObject<Country>(json, new Helper.MyDateTimeConverter());

                Country country = db.Countries.Find(jCountry.CountryId);
                if (country != null)
                {
                    country.CountryName = jCountry.CountryName;
                    db.SaveChanges();
                }
                else
                {
                    db.Countries.Add(jCountry);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Метод удаляет страну
        /// </summary>
        /// <param name="value">ID свойства</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleteCountry(int value = 0)
        {
            try
            {
                Country country = db.Countries.Find(value);
                if (country != null)
                {
                    db.Countries.Remove(country);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public ActionResult DeleteCountry(int countryId = 0)
        {
            if (countryId != 0)
            {
                Country findCountry = db.Countries.Find(countryId);
                if (findCountry == null)
                    return RedirectToAction("Country", new { isErrorMessage = 1, message = "Объект был не найден" });

                try
                {
                    db.Countries.Remove(findCountry);
                    db.SaveChanges();
                    return RedirectToAction("Country",
                        new { isErrorMessage = 0, message = "Удаление прошло успешно." });
                }
                catch (Exception e)
                {
                    return RedirectToAction("Country",
                        new
                        {
                            isErrorMessage = 2,
                            message = e
                        });
                }
            }
            return RedirectToAction("Country", new
            {
                isErrorMessage = 1,
                message = "Данные пришли пустыми."
            });
        }
        public ActionResult AddCountry(Country country)
        {
            if (country == null)
            {
                return RedirectToAction("Country", new { isErrorMessage = 1, Message = "Данные пришли пустыми." });
            }

            Country findCountry = db.Countries.Find(country.CountryId);
            try
            {
                if (findCountry == null)
                {
                    db.Countries.Add(country);
                    db.SaveChanges();
                }
                else
                {
                    findCountry.CountryName = country.CountryName;
                    db.Entry(findCountry).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Country",
                        new { isErrorMessage = 0, Message = "Данные были изменены успешно." });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Country",
                    new { isErrorMessage = 2, Message = e });
            }
            return RedirectToAction("Country",
                new { isErrorMessage = 0, Message = "Данные были добавлены успешно." });

        }
    }
}