using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ObjectInformation.DAL.Model;
using System.Data.Entity.Migrations;
using System.Security.Principal;
using System.Threading.Tasks;
using NLog;

namespace ObjectInformation.DAL
{
    /// <summary>
    /// Вспомогательный класс для работы с бд
    /// </summary>
    public class Service
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Сущность бд
        /// </summary>
        private static OInformation db = new OInformation();

        #region ObjectType

        /// <summary>
        /// Метод получения всех типов объектов отсортированных по <see cref="ObjectType.ObjectTypeId"/>
        /// </summary>
        /// <returns>Список типов объектов</returns>
        public static List<ObjectType> GetObjectTypes()
        {
            try
            {
                return db.ObjectTypes.OrderBy(o => o.ObjectTypeId).ToList();
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("GetObjectTypes, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return null;
            }
        }
        /// <summary>
        /// Метод добавления нового типа объекта
        /// </summary>
        /// <param name="objectType">Тип объекта</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool AddObjectType(ObjectType objectType, out string message)
        {
            try
            {
                db.ObjectTypes.Add(objectType);
                db.SaveChanges();
                message = null;
                return true;
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("AddObjectType, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                message = msg;

                return false;
            }
        }
        /// <summary>
        /// Метод обновлени типа объекта
        /// </summary>
        /// <param name="objectType">Тип объекта</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool UpdateObjectType(ObjectType objectType, out string message)
        {
            try
            {
                ObjectType localObjectType = db.ObjectTypes.Find(objectType.ObjectTypeId);
                if (localObjectType != null)
                {
                    localObjectType = objectType;
                    db.Set<ObjectType>().AddOrUpdate(localObjectType);
                    //db.Entry(localObjectType).State = EntityState.Modified;
                    //db.ObjectTypes.Attach(localObjectType);
                    db.SaveChanges();
                    message = null;
                    return true;
                }
                else
                {
                    message = "Такой записи не существует!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("UpdateObjectType, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                message = msg;

                return false;
            }
        }
        /// <summary>
        /// Метод удаления типа объекта по его id
        /// </summary>
        /// <param name="objectTypeId">id типа объекта</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool DeleteObjectType(int objectTypeId, out string message)
        {
            try
            {
                ObjectType objectType = db.ObjectTypes.Find(objectTypeId);
                if (objectType != null)
                {
                    db.ObjectTypes.Remove(objectType);
                    //db.Set<ObjectType>().Remove(objectType);
                    db.SaveChanges();
                    message = null;
                    return true;
                }
                else
                {
                    message = "Объект не был найден";
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("DeleteObjectType, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                message = msg;

                return false;
            }
        }

        #endregion

        #region Property

        /// <summary>
        /// Метод получения всех свойств объектов отсортированных по <see cref="Property.PropertyId"/>
        /// </summary>
        /// <returns>Список свойств объектов</returns>
        public static List<Property> GetProperties()
        {
            return db.Properties.OrderBy(o => o.PropertyId).ToList();
        }
        /// <summary>
        /// Метод добавления нового свойства
        /// </summary>
        /// <param name="property">Свойство</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool AddProperty(Property property, out string msg)
        {
            try
            {
                db.Properties.Add(property);
                db.SaveChanges();
                msg = null;
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Метод обновлени свойства
        /// </summary>
        /// <param name="property">Свойство</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool UpdateProperty(Property property, out string msg)
        {
            try
            {
                Property localObjectType = db.Properties.Find(property.PropertyId);
                if (localObjectType != null)
                {
                    localObjectType = property;
                    db.Set<Property>().AddOrUpdate(localObjectType);
                    //db.Entry(localObjectType).State = EntityState.Modified;
                    //db.ObjectTypes.Attach(localObjectType);
                    db.SaveChanges();
                    msg = null;
                    return true;
                }
                else
                {
                    msg = "Такой записи не существует!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Метод удаления свойства по его id
        /// </summary>
        /// <param name="propertyId">id свойства</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool DeleteProperty(int propertyId, out string msg)
        {
            try
            {
                Property property = db.Properties.Find(propertyId);
                if (property != null)
                {
                    db.Properties.Remove(property);
                    //db.Set<ObjectType>().Remove(objectType);
                    db.SaveChanges();
                    msg = null;
                    return true;
                }
                else
                {
                    msg = "Объект не был найден";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        #endregion



        #region ObjectRealty













        public static bool UploadPhoto(Upload upload)
        {
            try
            {
                db.Uploads.Add(upload);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("UploadPhoto, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return false;
            }
        }
        public static bool EditPhoto(Upload upload)
        {
            try
            {
                Upload up = db.Uploads.Find(upload.UploadId);
                if (up != null)
                {
                    up.FileHeader = upload.FileHeader;
                    up.FileDescription = upload.FileDescription;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("EditPhoto, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return false;
            }
        }
        public static bool UpdateUploadPhoto(Upload upload, out string oldFilePath)
        {
            oldFilePath = "";
            try
            {
                var fUpload = db.Uploads.Find(upload.UploadId);
                if (fUpload != null)
                {
                    oldFilePath = fUpload.Path;

                    fUpload.Path = upload.Path;
                    fUpload.DocumentTypeId = upload.DocumentTypeId;
                    fUpload.FileName = upload.FileName;
                    fUpload.ObjectRealtyId = upload.ObjectRealtyId;
                    fUpload.FileHeader = upload.FileHeader;
                    fUpload.FileDescription = upload.FileDescription;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("UpdateUploadPhoto, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return false;
            }
        }
        public static bool UploadFile(string path, int objectId, string fileName, int documentTypeId)
        {
            try
            {
                db.Uploads.Add(new Upload()
                {
                    Path = path,
                    ObjectRealtyId = objectId,
                    DocumentTypeId = documentTypeId,
                    FileName = fileName
                });
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("UploadFile, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return false;
            }
        }
        public static List<string> GetObjectPhotos(int objectId)
        {
            try
            {
                return db.Uploads
              .Where(w => w.ObjectRealtyId == objectId)
              .Select(s => s.Path)
              .ToList();
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("GetObjectPhotos, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return null;
            }
          
        }

        
        #endregion

        #region Country

        /// <summary>
        /// Метод получения всех стран отсортированных по <see cref="Country.CountryId"/>
        /// </summary>
        /// <returns>Список стран</returns>
        public static List<Country> GetCountries()
        {
            try
            {
                return db.Countries.OrderBy(o => o.CountryId).ToList();
            }
            catch (Exception ex)
            {
                string msg =
                   string.Format("GetCountries, возникла ошибка: {0}", ex.Message);

                if (ex.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.Message;

                if (ex.InnerException?.InnerException != null)
                    msg += "\nInnerException: " + ex.InnerException.InnerException.Message;

                logger.Error(msg);

                return null;
            }
            
        }

        #endregion

        #region Region

        /// <summary>
        /// Метод получения всех регионов страны по его id
        /// </summary>
        /// <param name="countryId">id страны</param>
        /// <returns>список регионов</returns>
        public static List<Region> GetRegions(int countryId)
        {
            return db.Regions.Where(w => w.CountryId == countryId).ToList();
        }
        #endregion

        #region City

        /// <summary>
        /// Метод получения всех городов региона по его id
        /// </summary>
        /// <param name="regionId">id региона</param>
        /// <returns>список городов</returns>
        public static List<City> GetCities(int regionId)
        {
            return db.Cities.Where(w => w.RegionId == regionId).ToList();
        }

        #endregion

        #region District

        /// <summary>
        /// Метод получения всех районов города по его id
        /// </summary>
        /// <param name="cityId">id города</param>
        /// <returns>список районов</returns>
        public static List<District> GetDistricts(int cityId)
        {
            return db.Districts.Where(w => w.CityId == cityId).ToList();
        }

        #endregion

        public static DocumentType GetDocumentTypeImage()
        {
            if (db.DocumentTypes.FirstOrDefault(f => f.DocumentTypeName == "Photo") == null)
            {
                db.DocumentTypes.Add(new DocumentType() { DocumentTypeName = "Photo" });
                db.SaveChanges();
            }
            return db.DocumentTypes.FirstOrDefault(f => f.DocumentTypeName == "Photo");
        }

        public static DocumentType GetDocumentTypeById(int id)
        {
            return db.DocumentTypes.FirstOrDefault(f => f.DocumentTypeId == id);
        }

        public static List<Comment> GetCommentsByObjectRealtyId(int id)
        {
            return db.Comments.Where(w => w.ObjectRealtyId == id).ToList();
        }

        public static List<Upload> GetObjectDocumentsById(int id)
        {
            return db.Uploads.Where(w => w.DocumentType.DocumentTypeName != "Photo" && w.ObjectRealtyId == id).ToList();
        }
    }
}