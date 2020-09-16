using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectInformation.DAL.Model
{
    [Table("TaskFile")]
    public partial class TaskFile
    {
        [Key]
        public int Id { get; set; }

        public string DocPath { get; set; }

        public int DocumentTypeId { get; set; }

        public string FileName { get; set; }

        public string FileDescription { get; set; }

        public int TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public Task Task { get; set; }

        public DateTime CreateDate { get; set; }
    }
}