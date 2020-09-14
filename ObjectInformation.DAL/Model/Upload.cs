using System;

namespace ObjectInformation.DAL.Model
{   
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Upload")]
    public partial class Upload
    {
        [Key]
        public int UploadId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Path { get; set; }

        [Required]
        public int DocumentTypeId { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        public string FileHeader { get; set; }
        public string FileDescription { get; set; }

        [Required]
        public int ObjectRealtyId { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual ObjectRealty ObjectRealty { get; set; }
        public DateTime CreateDate { get; set; }
    }
}