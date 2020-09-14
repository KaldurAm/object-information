using System;

namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Checklist")]
    public partial class Checklist
    {
        [Key]
        public int ChecklistId { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}