namespace ObjectInformation.DAL.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ObjectProperty")]
    public partial class ObjectProperty
    {
        [Key]
        public int ObjectPropertyId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(200)]
        public string Value { get; set; }

        [Required]
        public int ObjectRealtyId { get; set; }

        public virtual ObjectRealty ObjectRealty { get; set; }

        public virtual Property Property { get; set; }
    }
}