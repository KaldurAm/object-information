namespace ObjectInformation.DAL.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(1000)]
        public string CommentText { get; set; }

        [Required]
        public int ObjectRealtyId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public DateTime CommentDateTime { get; set; }

        public virtual ObjectRealty ObjectRealty { get; set; }
    }
}