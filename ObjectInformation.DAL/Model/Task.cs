using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInformation.DAL.Model
{
    [Table("Task")]
    public partial class Task
    {
        public Task()
        {
            Files = new HashSet<TaskFile>();
            Responsibles = new HashSet<TaskResponsible>();
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Описание")]
        public string TaskDescription { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "Дней")]
        public int DaysCount { get; set; }

        [Display(Name = "Срок")]
        public DateTime? Deadline { get; set; }

        [Display(Name = "Автор")]
        public string Creator { get; set; }

        public int ObjectRealtyId { get; set; }

        [ForeignKey(nameof(ObjectRealtyId))]
        public virtual ObjectRealty ObjectRealty { get; set; }

        public int StatusId { get; set; } = (int)TaskStatusEnum.СозданаЗадача;

        [ForeignKey(nameof(StatusId))]
        public TaskStatus TaskStatus { get; set; }

        public virtual ICollection<TaskFile> Files { get; set; } = null;

        public virtual ICollection<TaskResponsible> Responsibles { get; set; }
    }
}
