using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using ObjectInformation.DAL.Model;
using PagedList;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class DocumentTypesController : BaseController
    {
        // GET: DocumentTypes
        public async Task<ActionResult> Index(int isErrorMessage = -1, string message = "")
        {
            ViewBag.isErrorMessage = isErrorMessage;
            ViewBag.Message = message;
            return View(await db.DocumentTypes.Where(w => w.DocumentTypeName != "Photo").ToListAsync());
        }

        // GET: DocumentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DocumentTypeId,DocumentTypeName")] DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                db.DocumentTypes.Add(documentType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(documentType);
        }

        // GET: DocumentTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documentType = await db.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return HttpNotFound();
            }
            return View(documentType);
        }

        // POST: DocumentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DocumentTypeId,DocumentTypeName")] DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(documentType);
        }

        // POST: DocumentTypes/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            DocumentType documentType = await db.DocumentTypes.FindAsync(id);
            int isErrorMessage;
            string message;
            if (documentType != null)
            {
                try
                {
                    db.DocumentTypes.Remove(documentType);
                    await db.SaveChangesAsync();
                    isErrorMessage = 0;
                    message = "Данные были добавлены успешно.";
                }
                catch (Exception e)
                {
                    isErrorMessage = 2;
                    message = e.ToString();
                }
            }
            else
            {
                isErrorMessage = 1;
                message = "Данные пришли пустыми.";
            }
            return RedirectToAction("Index", new { isErrorMessage, message });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ChecklistDocument()
        {
            List<Checklist> checklists = db.Checklists.ToList();
            return View(checklists);
        }

        /// <summary>
        /// Метод возвращает справочник типов документов для отпределенного чеклиста
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTableDocumentChecklist(int checklistId)
        {
            try
            {
                var projectsList = db.DocumentTypes.Where(w=>w.ChecklistId== checklistId).Select(s => new
                {
                    s.DocumentTypeId, s.DocumentTypeName
    
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
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Метод возвращает справочник чеклистов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTableChecklist()
        {
            try
            {
                var projectsList = db.Checklists.Select(s => new
                {
                    s.ChecklistId,
                    s.CreateDate,
                    s.Name,
                    s.Description
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
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
