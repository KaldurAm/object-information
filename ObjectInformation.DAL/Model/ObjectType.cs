namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ObjectType")]
    public partial class ObjectType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObjectType()
        {
            ObjectRealties = new HashSet<ObjectRealty>();
        }

        [Key]
        public int ObjectTypeId { get; set; }

        [Required]
        [StringLength(200)]
        public string ObjectTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectRealty> ObjectRealties { get; set; }
    }

    public class ViewObjectType
    {
        public int ObjectTypeId { get; set; }
        public string ObjectTypeName { get; set; }
        public int CountInObject { get; set; }
    }
}