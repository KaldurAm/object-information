using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.DAL
{
    public class ServiceMap : IDisposable
    {
        private static OInformation db = new OInformation();

        /// <summary>
        /// Метод создает полигон
        /// </summary>
        /// <param name="polygon"></param>
        public static void CreatePolygon(Polygon polygon)
        {
            using (OInformation db_ = new OInformation())
            {
                using (DbContextTransaction tr = db_.Database.BeginTransaction())
                {
                    try
                    {
                        db_.Polygon.Add(polygon);
                        db_.SaveChanges();

                        foreach (Coordinate coordinate in polygon.coords)
                        {
                            coordinate.PolygonId = polygon.PolygonId;
                            db_.Coordinate.Add(coordinate);
                            db_.SaveChanges();
                        }

                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                    }
                }
            }
        }

        /// <summary>
        /// Метод обновляет данные по точке
        /// </summary>
        /// <param name="polygon"></param>
        public static void UpdatePolygon(Polygon polygon)
        {
            try
            {
                Polygon pol = db.Polygon.Find(polygon.PolygonId);
                if (pol != null)
                {
                    pol.PolygonName = polygon.PolygonName;
                    pol.PolygonDescription = polygon.PolygonDescription;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Метод удаляет все точки одного объекта
        /// </summary>
        /// <param name="objectRealtyId">Уникальный ID объекта</param>
        public static void DeleteAllPolygon(int objectRealtyId)
        {
            try
            {
                foreach (var polygon in db.Polygon.Where(w => w.ObjectRealtyId == objectRealtyId))
                {
                    DeletePolygon(polygon.PolygonId);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Метод удаляет точку на карте вместе с координатами
        /// </summary>
        /// <param name="polygonId">Уникальный ID точки</param>
        public static void DeletePolygon(int polygonId)
        {
            try
            {
                Polygon polygon = db.Polygon.Find(polygonId);
                if (polygon != null)
                {
                    List<Coordinate> coordinates = db.Coordinate.Where(w => w.PolygonId == polygon.PolygonId).ToList();
                    foreach (Coordinate coordinate in coordinates)
                    {
                        db.Coordinate.Remove(coordinate);
                        db.SaveChanges();
                    }

                    db.Polygon.Remove(polygon);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Метод возвращает все точки на карте объекта
        /// </summary>
        /// <param name="objectRealtyId">Уникальный ID объекта</param>
        /// <returns></returns>
        public static List<Polygon> GetPolygons(int objectRealtyId)
        {
            List<Polygon> polygons = new List<Polygon>();
            if (objectRealtyId == 0)
            {
                //Исправить это!
                foreach (var item in db.Polygon.ToList())
                {
                    item.coords = db.Coordinate.Where(w => w.PolygonId == item.PolygonId).ToList();
                    polygons.Add(item);
                }
            }
            else
            {
                foreach (var item in db.Polygon.Where(w => w.ObjectRealtyId == objectRealtyId))
                {
                    item.coords = db.Coordinate.Where(w => w.PolygonId == item.PolygonId).ToList();
                    polygons.Add(item);
                }
            }

            return polygons;
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}
