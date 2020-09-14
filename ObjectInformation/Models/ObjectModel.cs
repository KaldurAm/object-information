using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObjectInformation.DAL;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.Models
{
    public class ObjectModel
    {
        public int ObjectRealtyId { get; set; }
        public List<Comment> Comments;
        public List<Upload> Documents;

        public ObjectModel(int objectRealtyId)
        {
            this.ObjectRealtyId = objectRealtyId;
            Comments = Service.GetCommentsByObjectRealtyId(objectRealtyId);
            Documents = Service.GetObjectDocumentsById(objectRealtyId);
        }
    }
}