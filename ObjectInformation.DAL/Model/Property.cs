namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Property")]
    public partial class Property
    {
        public Property()
        {
            ObjectProperties = new HashSet<ObjectProperty>();
        }

        [Key]
        public int PropertyId { get; set; }

        [Required]
        public string Name { get; set; }

        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        
        public virtual ICollection<ObjectProperty> ObjectProperties { get; set; }
    }

    public class ViewPropertyObjectType
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public int CountInObject { get; set; }
        public string UnitName { get; set; }

        public List<string> ObjectType = new List<string>();
    }
}