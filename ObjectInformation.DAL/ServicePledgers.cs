using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.DAL
{
    public class ServicePledgers
    {
        private static OInformation db = new OInformation();

        //Pledgers
        public static List<Pledgers> GetPledgers()
        {
            return db.Pledgers.ToList();
        }

        public static Pledgers GetPledgersById(int pledgersId)
        {
            return db.Pledgers.Find(pledgersId);
        }

        public static bool AddPledgers(Pledgers pledgers)
        {
            try
            {
                db.Pledgers.Add(pledgers);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool EditPledgers(Pledgers pledgers)
        {
            try
            {
                var data = db.Pledgers.Find(pledgers.PledgersId);
                if (data != null)
                {
                    data.NameOfPledger = pledgers.NameOfPledger;
                    data.ControllingShareholder = pledgers.ControllingShareholder;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //ObjectRealtyPledgers
        public static List<ObjectRealtyPledgers> GetObjectRealtyPledgers()
        {
            //List<ObjectRealtyPledgers> objectRealtyPledgers = db.ObjectRealtyPledgers;
            return db.ObjectRealtyPledgers.ToList();
        }

        public static ObjectRealtyPledgers GetObjectRealtyPledgersById(int objectRealtyPledgersId)
        {
            return db.ObjectRealtyPledgers.Find(objectRealtyPledgersId);
        }

        public static List<ObjectRealtyPledgers> GetObjectRealtyPledgersByObjectId(int objectRealtyId)
        {
            return db.ObjectRealtyPledgers.Where(w=>w.ObjectRealtyId== objectRealtyId).Include(c=>c.Pledgers).ToList();
        }

        public static bool AddObjectRealtyPledgers(ObjectRealtyPledgers objectRealtyPledgers)
        {
            try
            {
                db.ObjectRealtyPledgers.Add(objectRealtyPledgers);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool EditObjectRealtyPledgers(ObjectRealtyPledgers objectRealtyPledgers)
        {
            try
            {
                var data = db.ObjectRealtyPledgers.Find(objectRealtyPledgers.ObjectRealtyPledgersId);
                if (data != null)
                {
                    data.PledgersId = objectRealtyPledgers.PledgersId;
                    data.PledgeDate = objectRealtyPledgers.PledgeDate;
                    data.MortgageValue = objectRealtyPledgers.MortgageValue;
                    data.AssessedValue = objectRealtyPledgers.AssessedValue;
                    data.EvaluationDate = objectRealtyPledgers.EvaluationDate;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
