using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.DAL
{
    public class ServiceDocument
    {
        private static OInformation db = new OInformation();

        public static List<DocumentType> GetDocumentTypes()
        {
            return db.DocumentTypes.Where(w => w.DocumentTypeName != "Photo").ToList();
        }
    }
}
