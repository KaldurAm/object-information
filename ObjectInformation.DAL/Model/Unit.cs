namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Unit")]
    public partial class Unit
    {
        public Unit()
        {
            Properties = new HashSet<Property>();
        }

        [Key]
        public int UnitId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int Code { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}