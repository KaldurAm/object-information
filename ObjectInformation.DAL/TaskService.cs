using NLog;
using ObjectInformation.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInformation.DAL
{
    public class TaskService : IDisposable
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        private readonly OInformation db;

        public TaskService()
        {
            db = new OInformation();
        }

        public async System.Threading.Tasks.Task Create(Model.Task newTask)
        {
            try
            {
                newTask.CreateDate = DateTime.Now;
                db.Tasks.Add(newTask);
                await db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async System.Threading.Tasks.Task Update(Model.Task newTask)
        {
            try
            {
                db.Entry(newTask).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<IEnumerable<Model.Task>> Get(int ObjectId)
        {
            try
            {
                return await db.Tasks.Where(x => x.ObjectRealtyId == ObjectId).ToListAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<IEnumerable<Model.Task>> Get(string userId)
        {
            try
            {
                return await db.Tasks.Where(x => x.Responsibles.Select(s=>s.ResponsibleUserId).Contains(userId)).ToListAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}
