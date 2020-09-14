namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PropertyObjectType")]
    public partial class PropertyObjectType
    {

        [Key]
        public int PropertyObjectTypeId { get; set; }
        public int PropertyId { get; set; }
        public int ObjectTypeId { get; set; }
    }
}