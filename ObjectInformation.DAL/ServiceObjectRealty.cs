using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.DAL
{
    public class ServiceObjectRealty
    {
        private static OInformation db = new OInformation();

        /// <summary>
        /// Метод получения всех объектов отсортированных по <see cref="ObjectType.ObjectTypeId"/>
        /// </summary>
        /// <returns>Список объектов</returns>
        public static List<ObjectRealty> GetObjectRealties()
        {
            List<ObjectRealty> objectRealty = db.ObjectRealties
                .Where(w => w.ObjectTypeId != 0)
               .Include(c => c.ObjectType)
               .Include(c => c.Currency)

                .Include(c => c.Country)
               .Include(c => c.Region)
                .Include(c => c.City)
               .Include(c => c.District)

                .OrderBy(o => o.ObjectRealtyId).ToList();

            return objectRealty;
        }

        /// <summary>
        /// Метод для получения объекта по id/>
        /// </summary>
        /// <param name="objectRealtyId">id объекта</param>
        /// <returns>объект с не null адресом</returns>
        public static ObjectRealty GetObjectRealtyById(int objectRealtyId)
        {
            return db.ObjectRealties.Find(objectRealtyId);
        }

        /// <summary>
        /// Метод добавления нового объекта
        /// </summary>
        /// <param name="objectRealty">Объект</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool AddObjectRealty(ref ObjectRealty objectRealty, out string msg)
        {
            try
            {
                objectRealty.lat = "47.69497434";
                objectRealty.lng = "68.57666016";
                objectRealty.zoom = "5";
                objectRealty.CreateDate = DateTime.Now;
                db.ObjectRealties.Add(objectRealty);
                db.SaveChanges();

                msg = "";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Метод обновлени объекта
        /// </summary>
        /// <param name="objectRealty">Объект</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool UpdateObjectRealty(ObjectRealty objectRealty, out string msg)
        {
            try
            {
                //поиск такой записи в бд
                ObjectRealty localObjectRealty = db.ObjectRealties.Find(objectRealty.ObjectRealtyId);
                if (localObjectRealty != null)
                {
                    //обновление объекта на новый
                    localObjectRealty.ObjectTypeId = objectRealty.ObjectTypeId;
                    localObjectRealty.DistrictId = objectRealty.DistrictId;
                    localObjectRealty.RegionId = objectRealty.RegionId;
                    localObjectRealty.CityId = objectRealty.CityId;
                    localObjectRealty.Name = objectRealty.Name;
                    localObjectRealty.Square = objectRealty.Square;
                    localObjectRealty.CadastralNumber = objectRealty.CadastralNumber;
                    localObjectRealty.CostDCT = objectRealty.CostDCT;
                    localObjectRealty.Address = objectRealty.Address;

                    localObjectRealty.Cost = objectRealty.Cost;
                    localObjectRealty.Description = objectRealty.Description;

                    localObjectRealty.CountryId = objectRealty.CountryId;
                    localObjectRealty.CurrencyId = objectRealty.CurrencyId == 0 ? 1 : objectRealty.CurrencyId;
                    localObjectRealty.CostDate = objectRealty.CostDate;
                    localObjectRealty.CurrencyRate = objectRealty.CurrencyRate;

                    db.SaveChanges();
                    msg = "Запись обновлена успешно";
                    return true;
                }
                else
                {

                    msg = "При обновлении записи возникла ошибка";
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
        /// Метод обновлени объекта для карты, изменяет всего 3 свойства lat, lng, zoom
        /// </summary>
        /// <param name="objectRealty">Объект</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool UpdateObjectRealty(int objectRealtyId, string lat, string lng, int zoom, out string msg)
        {
            try
            {
                //поиск такой записи в бд
                ObjectRealty localObjectRealty = db.ObjectRealties.Find(objectRealtyId);
                if (localObjectRealty != null)
                {
                    //обновление объекта на новый
                    localObjectRealty.lat = lat;
                    localObjectRealty.lng = lng;
                    localObjectRealty.zoom = zoom.ToString();

                    db.SaveChanges();
                    msg = "Запись обновлена успешно";
                    return true;
                }
                else
                {

                    msg = "При обновлении записи возникла ошибка";
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
        /// Метод удаления типа объекта по его id
        /// </summary>
        /// <param name="objectTypeId">id типа объекта</param>
        /// <param name="msg">Возвращаемое сообщение при ошибке</param>
        /// <returns>Возвращает true при успешном отрабатование, false при ошибке</returns>
        public static bool DeleteObjectRealty(int objectRealtyId, out string msg)
        {
            try
            {
                ObjectRealty objectRealty = db.ObjectRealties.Find(objectRealtyId);
                if (objectRealty != null)
                {
                    db.ObjectRealties.Remove(objectRealty);
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
    }
}
