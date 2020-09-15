using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using ObjectInformation.DAL;
using ObjectInformation.DAL.Model;
using ObjectInformation.Models;
using PagedList;

namespace ObjectInformation.Controllers
{
    [Authorize]
    public class ObjectRealtyController : BaseController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            List<ObjectRealty> objectRealties = ServiceObjectRealty.GetObjectRealties();
            ViewBag.ObjectProperty = db.ObjectProperties.ToList();
            ViewBag.Uploads = db.Uploads.ToList();

            return View(objectRealties);
        }

        public ActionResult AddObject(int objectRealtyId = 0)
        {
            ObjectRealty objectRealty = new ObjectRealty();

            if (objectRealtyId == 0)
            {
                objectRealty.CreateDate = DateTime.Now;
                objectRealty.CreateUser = User.Identity.Name;

                string msg;
                if (ServiceObjectRealty.AddObjectRealty(ref objectRealty, out msg))
                {
                    ViewBag.ObjectPropertiesM =db.ObjectProperties.Where(w => w.ObjectRealtyId == objectRealty.ObjectRealtyId).ToList();
                    ViewBag.ObjectImage = db.Uploads.Where(w => w.ObjectRealtyId == objectRealty.ObjectRealtyId).ToList();
                    ViewBag.Properties = Service.GetProperties().Select(s => new SelectListItem() { Text = s.Name, Value = s.PropertyId.ToString() }).ToList();
                    ViewBag.ObjectRealtyPledgers = ServicePledgers.GetObjectRealtyPledgersByObjectId(0);
                    return View(objectRealty);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                objectRealty = db.ObjectRealties.Find(objectRealtyId);
                if (objectRealty != null)
                {
                    ViewBag.ObjectPropertiesM = db.ObjectProperties.Where(w => w.ObjectRealtyId == objectRealty.ObjectRealtyId).ToList();
                    ViewBag.ObjectImage = db.Uploads.Where(w => w.ObjectRealtyId == objectRealty.ObjectRealtyId).ToList();
                    ViewBag.Properties = Service.GetProperties().Select(s => new SelectListItem() { Text = s.Name, Value = s.PropertyId.ToString() }).ToList();
                    ViewBag.ObjectRealtyPledgers = ServicePledgers.GetObjectRealtyPledgersByObjectId(objectRealtyId);

                    return View(objectRealty);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        public ActionResult AddObject(ObjectRealty objectRealty)
        {
            string msg;

            DateTime costDate;
            DateTime.TryParse(Request.Form["CostDate"], out costDate);
            objectRealty.CostDate = costDate;

            if (ServiceObjectRealty.UpdateObjectRealty(objectRealty, out msg))
            {
                return RedirectToAction("Index");
            }


            return View(objectRealty);
        }

        #region  РАБОТА со свойствами объекта
        [HttpGet]
        public JsonResult ViewForEditObjectProperty(int value)
        {
            var objectProperty = db.ObjectProperties.Where(w => w.ObjectPropertyId == value);

            return Json(objectProperty.Select(s => new { s.ObjectPropertyId, s.PropertyId, s.Value, s.ObjectRealtyId }).SingleOrDefault(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ModifyObjectProperty(string json)
        {
            try
            {
                dynamic data = JObject.Parse(json);

                ObjectProperty jObjectProperty = JsonConvert.DeserializeObject<ObjectProperty>(json, new Helper.MyDateTimeConverter());
                jObjectProperty.ObjectRealtyId = Convert.ToInt32(data.PropObjectRealtyId);


                ObjectProperty objectProperty = db.ObjectProperties.Find(jObjectProperty.ObjectPropertyId);
                if (objectProperty != null)
                {
                    objectProperty.PropertyId = jObjectProperty.PropertyId;
                    objectProperty.Value = jObjectProperty.Value;
                    objectProperty.ObjectRealtyId = jObjectProperty.ObjectRealtyId;
                    db.SaveChanges();
                }
                else
                {
                    db.ObjectProperties.Add(jObjectProperty);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpPost]
        public ActionResult DeleteObjectProperty(int value = 0)
        {
            try
            {
                ObjectProperty objectProperty = db.ObjectProperties.Find(value);
                if (objectProperty != null)
                {
                    db.ObjectProperties.Remove(objectProperty);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }
        #endregion

        #region РАБОТА С КАРТИНКАМИ
        [HttpGet]
        public JsonResult ViewForEditObjectImage(int value)
        {
            var uploads = db.Uploads.Where(w => w.UploadId == value);

            return Json(uploads.Select(s => new { s.UploadId, s.FileHeader, s.FileDescription }).SingleOrDefault(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult UploadImages(int objectId, int uploadId = 0)
        {
            try
            {

                Upload upload = new Upload();
                upload.ObjectRealtyId = objectId;
                upload.FileHeader = Request.Form["FileHeader"];
                upload.FileDescription = Request.Form["FileDescription"];
                upload.CreateDate = DateTime.Now;

                upload.DocumentType = db.DocumentTypes.First(f => f.DocumentTypeName == "Photo");
                upload.DocumentTypeId = upload.DocumentType.DocumentTypeId;
                upload.UploadId = uploadId;

                var file = Request.Files["FileUploadImage"];

                if (file == null || file.ContentLength == 0)
                    return Json(new { isUploaded = false, message = "Файл поврежден или пустой!" }, "text/html");

                if (Save(upload, file))
                    return Json(new { isUploaded = true, message = "Файл был загружен" }, "text/html");

                return Json(new { isUploaded = false, message = "При загрузке файла произошла ошибка!" }, "text/html");
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("Save, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return Json(new { isUploaded = false, message = "При загрузке файла произошла ошибка!" }, "text/html");
            }
        }


        [HttpPost]
        public virtual ActionResult EditUpload(string json)
        {
            Upload upload = JsonConvert.DeserializeObject<Upload>(json);

            Service.EditPhoto(upload);

            return Json(null, "text/html");
        }

        [HttpPost]
        public ActionResult DeleteObjectImageRow(int value = 0)
        {
            try
            {
                Upload upload = db.Uploads.Find(value);
                if (upload != null)
                {
                    db.Uploads.Remove(upload);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }
        #endregion

        #region РАБОТА С ФАЙЛАМИ
        [HttpGet]
        public JsonResult ViewForEditObjectDocument(int value)
        {
            var uploads = db.Uploads.Where(w => w.UploadId == value);

            return Json(uploads.Select(s => new { DocUploadId = s.UploadId, DocFileHeader = s.FileHeader, DocFileDescription = s.FileDescription, s.DocumentTypeId }).SingleOrDefault(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult UploadDocument(int objectId, int uploadId = 0)
        {
            Upload upload = new Upload();
            upload.ObjectRealtyId = objectId;
            upload.FileHeader = Request.Form["DocFileHeader"];
            upload.FileDescription = Request.Form["DocFileDescription"];
            upload.CreateDate = DateTime.Now;

            int DocTypeId = 3;
            Int32.TryParse(Request.Form["DocumentTypeId"].ToString(), out DocTypeId);

            upload.DocumentTypeId = DocTypeId;
            upload.DocumentType = db.DocumentTypes.First(f => f.DocumentTypeId == DocTypeId);


            upload.UploadId = uploadId;

            var file = Request.Files["FileUploadDoc"];

            if (file == null || file.ContentLength == 0)
                return Json(new { isUploaded = false, message = "Файл поврежден или пустой!" }, "text/html");

            if (Save(upload, file))
                return Json(new { isUploaded = true, message = "Файл был загружен" }, "text/html");

            return Json(new { isUploaded = false, message = "При загрузке файла произошла ошибка!" }, "text/html");
        }

        [HttpPost]
        public ActionResult DeleteObjectDocumentRow(int value = 0)
        {
            try
            {
                Upload upload = db.Uploads.Find(value);
                if (upload != null)
                {
                    db.Uploads.Remove(upload);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }
        #endregion

        #region РАБОТА С КАРТАМИ
        [HttpPost]
        public JsonResult SavePolygonData(string json, int objectRealtyId, string lat, string lng, int zoom)
        {
            Polygon polygon = JsonConvert.DeserializeObject<Polygon>(json);

            if (polygon.PolygonId == 0)
                ServiceMap.CreatePolygon(polygon);
            else
                ServiceMap.UpdatePolygon(polygon);

            string message = "";
            ServiceObjectRealty.UpdateObjectRealty(objectRealtyId, lat, lng, zoom, out message);

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPolygons(int objectRealtyId)
        {
            List<Polygon> polygons = ServiceMap.GetPolygons(objectRealtyId);
            // string jPolygons = JsonConvert.SerializeObject(polygons.ToArray());
            return Json(polygons, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeletePolygon(int polygonId)
        {
            ServiceMap.DeletePolygon(polygonId);
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        #endregion

        private bool Save(Upload upload, HttpPostedFileBase file)
        {
            try
            {
                string directoryPath = "~/ObjectRealtys/" + upload.ObjectRealtyId + "/" + upload.DocumentType.DocumentTypeName;

                string directoryFullPath = Server.MapPath(directoryPath);

                if (!Directory.Exists(directoryFullPath))
                    Directory.CreateDirectory(directoryFullPath);

                string guid = Guid.NewGuid().ToString();
                string filePath = directoryPath + "/" + (guid + "-" + file.FileName);
                string fileFullPath = Path.Combine(directoryFullPath, guid + "-" + file.FileName);
                file.SaveAs(fileFullPath);

                upload.Path = filePath.Replace("~", "");
                upload.DocumentType = null;

                if (upload.UploadId == 0)
                {
                    if (Service.UploadPhoto(upload))
                        return true;

                    System.IO.File.Delete(fileFullPath);
                    return false;
                }
                else
                {
                    string oldFilePath = "";
                    if (Service.UpdateUploadPhoto(upload, out oldFilePath))
                    {
                        if (!string.IsNullOrWhiteSpace(oldFilePath))
                            System.IO.File.Delete(Server.MapPath(oldFilePath));

                        return true;
                    }

                    System.IO.File.Delete(fileFullPath);
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("Save, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return false;
            }
        }

        #region РАБОТА С ЗАЛОГАМИ
        [HttpGet]
        public JsonResult ViewForEditObjectPledgers(int value)
        {
            var orp = db.ObjectRealtyPledgers.Where(w => w.ObjectRealtyPledgersId == value);
           // orp.Include(c => c.Pledgers);

            return Json(orp.Select(s => new { s.ObjectRealtyPledgersId, s.CreateDate, s.ObjectRealtyId, s.PledgersId, s.PledgeDate, s.MortgageValue, s.AssessedValue, s.EvaluationDate }).SingleOrDefault(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ModifyObjectRealtyPledgers(string json)
        {
            try
            {
                dynamic data = JObject.Parse(json);

                ObjectRealtyPledgers jObjectRealtyPledgers = new ObjectRealtyPledgers();
                //JsonConvert.DeserializeObject<ObjectRealtyPledgers>(json, new Helper.MyDateTimeConverter());
                jObjectRealtyPledgers.ObjectRealtyId = Convert.ToInt32(data.orpObjectRealtyId);
                jObjectRealtyPledgers.PledgersId = Convert.ToInt32(data.PledgersId);

                DateTime pledgeDate;
                if(DateTime.TryParse(data.PledgeDate.ToString(), out pledgeDate))
                    jObjectRealtyPledgers.PledgeDate = pledgeDate;

                DateTime evaluationDate;
                if (DateTime.TryParse(data.EvaluationDate.ToString(), out evaluationDate))
                    jObjectRealtyPledgers.EvaluationDate = evaluationDate;

                double mv = 0;
                if(double.TryParse(data.MortgageValue.ToString(), out mv))
                    jObjectRealtyPledgers.MortgageValue = mv;

                double av = 0;
                if (double.TryParse(data.AssessedValue.ToString(), out av))
                    jObjectRealtyPledgers.AssessedValue = av;

                ObjectRealtyPledgers objectRealtyPledgers = db.ObjectRealtyPledgers.Find(jObjectRealtyPledgers.ObjectRealtyPledgersId);
                if (objectRealtyPledgers != null)
                {
                    objectRealtyPledgers.ObjectRealtyId = jObjectRealtyPledgers.ObjectRealtyId;
                    objectRealtyPledgers.PledgersId = jObjectRealtyPledgers.PledgersId;
                    objectRealtyPledgers.PledgeDate = jObjectRealtyPledgers.PledgeDate;

                    objectRealtyPledgers.MortgageValue = jObjectRealtyPledgers.MortgageValue;
                    objectRealtyPledgers.AssessedValue = jObjectRealtyPledgers.AssessedValue;
                    objectRealtyPledgers.EvaluationDate = jObjectRealtyPledgers.EvaluationDate;

                    db.SaveChanges();
                }
                else
                {
                    jObjectRealtyPledgers.CreateDate = DateTime.Now;
                    db.ObjectRealtyPledgers.Add(jObjectRealtyPledgers);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpPost]
        public ActionResult DeleteObjectRealtyPledgers(int value = 0)
        {
            try
            {
                ObjectRealtyPledgers objectRealtyPledgers = db.ObjectRealtyPledgers.Find(value);
                if (objectRealtyPledgers != null)
                {
                    db.ObjectRealtyPledgers.Remove(objectRealtyPledgers);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }
        //
        #endregion

        public ActionResult ObjectType(int isErrorMessage = -1, string message = "")
        {
            ViewBag.isErrorMessage = isErrorMessage;
            ViewBag.message = message;
            ViewBag.ObjectTypes = Service.GetObjectTypes();
            return View();
        }

        public ActionResult AddObjectType(ObjectType objectType)
        {
            if (ModelState.IsValid)
            {
                string msg;
                if (objectType.ObjectTypeId == 0)
                {
                    if (Service.AddObjectType(objectType, out msg))
                    {
                        return RedirectToAction("ObjectType", new { isErrorMessage = 0, Message = "Данные были добавлены успешно." });
                    }
                    else
                    {
                        return RedirectToAction("ObjectType", new { isErrorMessage = 1, Message = msg });
                    }
                }
                else
                {
                    if (Service.UpdateObjectType(objectType, out msg))
                    {
                        return RedirectToAction("ObjectType", new { isErrorMessage = 0, Message = "Данные были изменены успешно." });
                    }
                }
            }
            return RedirectToAction("ObjectType");
        }

        /// <summary>
        /// Метод возвращает справочник свойст
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTableProperties()
        {
            try
            {
                var objectProperties = db.ObjectProperties.ToList();
                var propertyObjectType = db.PropertyObjectType.ToList();
                var objectTypes = db.ObjectTypes.ToList();
                var units = db.Units.ToList();

                List<ViewPropertyObjectType> projectsList = new List<ViewPropertyObjectType>();
                foreach (var item in db.Properties)
                {
                    ViewPropertyObjectType v = new ViewPropertyObjectType();
                    v.PropertyId = item.PropertyId;
                    v.Name = item.Name;
                    v.CountInObject = objectProperties.Count(w => w.PropertyId == item.PropertyId);

                    var unit = units.FirstOrDefault(f => f.UnitId == item.UnitId);
                    v.UnitName = unit != null ? string.Format("{0} ({1})", unit.Name, unit.Description) : "";

                    var pot = propertyObjectType.Where(w => w.PropertyId == item.PropertyId).Select(s => s.ObjectTypeId).ToList();
                    v.ObjectType = objectTypes.Where(w => pot.Contains(w.ObjectTypeId)).Select(s => s.ObjectTypeName).ToList();
                    projectsList.Add(v);
                }

                int recordsCount = projectsList.Count();
                int pageSize = int.Parse(Request["length"]);

                pageSize = pageSize < 0 ? recordsCount : pageSize;
                int fromRow = int.Parse(Request["start"]);
                int sEcho = int.Parse(Request["draw"]);
                int pageNumber = pageSize > 0 ? (fromRow + pageSize) / pageSize : 1;
                if (pageSize < pageNumber)
                    pageSize = pageNumber;


                var test = Json(new { draw = sEcho, recordsTotal = recordsCount, recordsFiltered = recordsCount, data = projectsList.ToPagedList(pageNumber, pageSize) }, JsonRequestBehavior.AllowGet);


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
        /// <param name="value">ID свойства</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult viewForEditProperties(int value)
        {
            var objectTypes =
                db.PropertyObjectType.Where(w => w.PropertyId == value).Select(s => s.ObjectTypeId).ToList();

            var objectTypesName =
                db.ObjectTypes.Where(w => objectTypes.Contains(w.ObjectTypeId)).Select(s => s.ObjectTypeName).ToList();

            string objectTypeId = "";
            if (objectTypesName != null && objectTypesName.Count() > 0)
            {
                objectTypeId = string.Join(",", objectTypesName);
            }


            return Json(db.Properties.Where(o => o.PropertyId == value).Select(s => new { s.PropertyId, s.Name, s.UnitId, ObjectTypeId = objectTypeId }).SingleOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод производит изменения добалвение свойства
        /// </summary>
        /// <param name="json">Строка с данными в формате Json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult modifyProperty(string json)
        {
            try
            {
                Property jProperty = JsonConvert.DeserializeObject<Property>(json, new Helper.MyDateTimeConverter());

                dynamic data = JObject.Parse(json);
                string objectTypeId = data.ObjectTypeId;

                Property property = db.Properties.Find(jProperty.PropertyId);
                if (property != null)
                {
                    property.Name = jProperty.Name;
                    property.UnitId = jProperty.UnitId;
                    db.SaveChanges();

                    if (!string.IsNullOrEmpty(objectTypeId))
                    {

                        foreach (PropertyObjectType pot in db.PropertyObjectType.Where(w => w.PropertyId == jProperty.PropertyId).ToList())
                        {
                            db.PropertyObjectType.Remove(pot);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    db.Properties.Add(jProperty);
                    db.SaveChanges();
                }

                string[] objectTypes = objectTypeId.Split(',');
                if (objectTypes.Any())
                {
                    foreach (ObjectType objectType in db.ObjectTypes.Where(w => objectTypes.Contains(w.ObjectTypeName)).ToList())
                    {
                        PropertyObjectType pot = new PropertyObjectType();
                        pot.ObjectTypeId = objectType.ObjectTypeId;
                        pot.PropertyId = jProperty.PropertyId;
                        db.PropertyObjectType.Add(pot);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Метод удаляет свойство
        /// </summary>
        /// <param name="value">ID свойства</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult deleteProperty(int value = 0)
        {
            try
            {
                Property property = db.Properties.Find(value);
                if (property != null)
                {
                    db.Properties.Remove(property);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        /// <summary>
        /// Метод возвращает наименование типов обьектов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetObjectTypes()
        {
            var objectType = db.ObjectTypes.Select(s => s.ObjectTypeName).ToArray();

            return Json(new { ObjectType = objectType }, JsonRequestBehavior.AllowGet); ;
        }

        /// <summary>
        /// Метод возвращает список Ед.измерений
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetListsUnits()
        {
            var dGroupListsUnits = db.Units.GroupBy(g => g.Category).Select(s => new { group = s.Key }).ToList();
            var dListsUnits = db.Units.ToList().Select(x => new { id = x.UnitId, name = string.Format("{0} ({1})", x.Name, x.Description), group = x.Category });
            return Json(new
            {
                dGroupListsUnits = dGroupListsUnits,
                dListsUnits = dListsUnits
            }, JsonRequestBehavior.AllowGet);
        }

        //Типы объектов
        /// <summary>
        /// Метод возвращает справочник типов объектов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult jGetTableObjectTypes()
        {
            try
            {
                var objectRealty = db.ObjectRealties.ToList();

                List<ViewObjectType> projectsList = new List<ViewObjectType>();
                foreach (var item in db.ObjectTypes.ToList())
                {
                    ViewObjectType v = new ViewObjectType();
                    v.ObjectTypeId = item.ObjectTypeId;
                    v.ObjectTypeName = item.ObjectTypeName;
                    v.CountInObject = objectRealty.Count(c => c.ObjectTypeId == item.ObjectTypeId);
                    projectsList.Add(v);
                }

                int recordsCount = projectsList.Count();
                int pageSize = int.Parse(Request["length"]);

                pageSize = pageSize < 0 ? recordsCount : pageSize;
                int fromRow = int.Parse(Request["start"]);
                int sEcho = int.Parse(Request["draw"]);
                int pageNumber = pageSize > 0 ? (fromRow + pageSize) / pageSize : 1;
                if (pageSize < pageNumber)
                {
                    pageSize = pageNumber;
                }

                var test = Json(new { draw = sEcho, recordsTotal = recordsCount, recordsFiltered = recordsCount, data = projectsList.ToPagedList(pageNumber, pageSize) }, JsonRequestBehavior.AllowGet);


                return Json(new { draw = sEcho, recordsTotal = recordsCount, recordsFiltered = recordsCount, data = projectsList.ToPagedList(pageNumber, pageSize) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Метод возвращает типы объектов для просмотра
        /// </summary>
        /// <param name="value">ID типа объекта</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult ViewForEditObjectType(int value)
        {
            return Json(db.ObjectTypes.Where(o => o.ObjectTypeId == value).Select(s => new { s.ObjectTypeId, s.ObjectTypeName }).SingleOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Метод производит изменения добалвение тип объекта
        /// </summary>
        /// <param name="json">Строка с данными в формате Json</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModifyObjectType(string json)
        {
            try
            {
                ObjectType jObjectType = JsonConvert.DeserializeObject<ObjectType>(json, new Helper.MyDateTimeConverter());

                ObjectType objectType = db.ObjectTypes.Find(jObjectType.ObjectTypeId);
                if (objectType != null)
                {
                    objectType.ObjectTypeName = jObjectType.ObjectTypeName;
                    db.SaveChanges();
                }
                else
                {
                    db.ObjectTypes.Add(jObjectType);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Метод удаляет тип объекта
        /// </summary>
        /// <param name="value">ID nbgf j,]trnf</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteObjectType(int value = 0)
        {
            try
            {
                ObjectType objectType = db.ObjectTypes.Find(value);
                if (objectType != null)
                {
                    db.ObjectTypes.Remove(objectType);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public ActionResult Property(int isErrorMessage = -1, string message = "")
        {
            ViewBag.isErrorMessage = isErrorMessage;
            ViewBag.message = message;
            ViewBag.Properties = Service.GetProperties();
            return View();
        }
        public ActionResult AddProperty(Property property)
        {
            if (ModelState.IsValid)
            {
                string msg;
                if (property.PropertyId == 0)
                {
                    if (Service.AddProperty(property, out msg))
                    {
                        return RedirectToAction("Property", new { isErrorMessage = 0, Message = "Данные были добавлены успешно." });
                    }
                    else
                    {
                        return RedirectToAction("Property", new { isErrorMessage = 1, Message = msg });
                    }
                }
                else
                {
                    if (Service.UpdateProperty(property, out msg))
                    {
                        return RedirectToAction("Property", new { isErrorMessage = 0, Message = "Данные были изменены успешно." });
                    }
                }
            }
            return RedirectToAction("Property");
        }
        public ActionResult DeleteProperty(int propertyId)
        {
            string msg;
            if (Service.DeleteProperty(propertyId, out msg))
            {
                return RedirectToAction("Property", new { isErrorMessage = 0, message = "Удаление прошло успешно." });
            }
            else
            {
                return RedirectToAction("Property", new { isErrorMessage = 1, message = msg });
            }
        }

        public ActionResult DeleteObject(int objectId)
        {
            string msg;
            if (ServiceObjectRealty.DeleteObjectRealty(objectId, out msg))
            {
                if (Directory.Exists("~/Files/" + objectId))
                {
                    Directory.Delete("~/Files/" + objectId);
                }
                if (Directory.Exists("~/Images/" + objectId))
                {
                    Directory.Delete("~/Images/" + objectId);
                }
                return RedirectToAction("Object", new { isErrorMessage = 0, message = "Удаление прошло успешно." });
            }
            else
            {
                return RedirectToAction("Object", new { isErrorMessage = 1, message = msg });
            }
        }

        public ActionResult ObjectPhotos(int objectId)
        {
            var photos = db.Uploads.Where(w => w.ObjectRealtyId == objectId && w.DocumentType.DocumentTypeName == "Photo").ToList();
            return PartialView("ObjectPhotos", photos);
        }
        public ActionResult ObjectFiles(int objectId)
        {
            var photos = db.Uploads.Where(w => w.ObjectRealtyId == objectId && w.DocumentType.DocumentTypeName == "Photo").ToList();
            return PartialView("ObjectPhotos", photos);
        }
        public FileResult SendFile(string path, string fileName)
        {
            return File(path, MimeMapping.GetMimeMapping(fileName), fileName);
        }

        //[HttpPost]
        //public virtual ActionResult UploadFiles(int objectId, int fileType)
        //{
        //    var file = Request.Files["FileUpload"];
        //    if (file == null || file.ContentLength == 0)
        //        return Json(new { isUploaded = false, message = "Файл поврежден или пустой!" }, "text/html");
        //    if (Save(objectId, Service.GetDocumentTypeById(fileType), file))
        //        return Json(new { isUploaded = true, message = "Файл был загружен" }, "text/html");
        //    return Json(new { isUploaded = false, message = "При загрузке файла произошла ошибка!" }, "text/html");
        //}

        [HttpPost]
        public JsonResult GetRegions(int countryId)
        {
            List<Region> regions = Service.GetRegions(countryId);
            JsonResult jsonResult = Json(regions.Select(s => new { id = s.RegionId, name = s.RegionName }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetCities(int regionId)
        {
            List<City> cities = Service.GetCities(regionId);
            JsonResult jsonResult = Json(cities.Select(s => new { id = s.CityId, name = s.CityName }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetDistricts(int citiId)
        {
            List<District> districts = Service.GetDistricts(citiId);
            JsonResult jsonResult = Json(districts.Select(s => new { id = s.DistrictId, name = s.DistrictName }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetProperties()
        {
            List<Property> regions = Service.GetProperties();
            JsonResult jsonResult = Json(regions.Select(s => new { id = s.PropertyId, name = s.Name }), JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        [HttpPost]
        public ActionResult DeleteUpload(int id, int objectId)
        {
            var up = db.Uploads.FirstOrDefault(f => f.UploadId == id);
            if (up != null)
            {
                db.Uploads.Remove(up);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception) { }
            }
            return RedirectToAction("AddObject", new { id = objectId, isErrorMessage = 0, Message = "Данные были изменены успешно." });
        }
    }
}