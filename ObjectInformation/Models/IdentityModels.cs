using Microsoft.AspNet.Identity.EntityFramework;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("data source=KMERGAZIYEV\\SQLEXPRESS;initial catalog=ObjectInformation;MultipleActiveResultSets=True;User ID=kmerg; Password=!Kpo4322002;")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}