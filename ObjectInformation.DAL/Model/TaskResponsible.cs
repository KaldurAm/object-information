using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectInformation.DAL.Model
{
    [Table("TaskResponsible")]
    public partial class TaskResponsible
    {
        [Key]
        public int Id { get; set; }

        public string ResponsibleUserId { get; set; }

        //[ForeignKey(nameof(ResponsibleUserId))]
        //public virtual ApplicationUser ResponsibleUser { get; set; }

        public int TaskId { get; set; }

        //[ForeignKey(nameof(TaskId))]
        //public virtual Model.Task Task { get; set; }
    }
}