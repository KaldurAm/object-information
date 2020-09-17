using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;
using PagedList;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class PledgersController : BaseController
    {
        // GET: Pledgers
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Метод возвращает справочник Залогодателей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTablePledgers()
        {
            try
            {
                var projectsList = db.Pledgers.Select(s => new
                {
                    s.PledgersId,
                    s.NameOfPledger,
                    s.ControllingShareholder
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

        /// <summary>
        /// Метод возвращает щалогодателя для отображения
        /// </summary>
        /// <param name="value">ID страны</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult viewForEditPledgers(int value)
        {
            return Json(db.Pledgers.Where(w => w.PledgersId == value).Select(s => new
            {
                s.PledgersId,
                s.NameOfPledger,
                s.ControllingShareholder

            }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод производит изменения в залогодателе
        /// </summary>
        /// <param name="json">Строка с данными в формате Json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModifyPledgers(string json)
        {
            try
            {
                Pledgers jPledgers = JsonConvert.DeserializeObject<Pledgers>(json, new Helper.MyDateTimeConverter());

                Pledgers pledgers = db.Pledgers.Find(jPledgers.PledgersId);
                if (pledgers != null)
                {
                    pledgers.NameOfPledger = jPledgers.NameOfPledger;
                    pledgers.ControllingShareholder = jPledgers.ControllingShareholder;
                    db.SaveChanges();
                }
                else
                {
                    using (OInformation db_ = new OInformation())
                    {
                        db_.Pledgers.Add(jPledgers);
                        db_.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Метод удаляет залогодателя
        /// </summary>
        /// <param name="value">ID свойства</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeletePledgers(int value = 0)
        {
            try
            {
                Pledgers pledgers = db.Pledgers.Find(value);
                if (pledgers != null)
                {
                    db.Pledgers.Remove(pledgers);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}