using Microsoft.AspNet.Identity.EntityFramework;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("data source=LAPTOP-RCGUPIJ6\\SQLEXPRESS;initial catalog=ObjectInformation;MultipleActiveResultSets=True;Trusted_Connection=True;")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}