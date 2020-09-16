using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectInformation.DAL.Model
{
    public enum TaskStatusEnum
    {
        СозданаЗадача = 1,
        ЗадачаНазначена,
        НаИсполнении,
        ОсталосьПятьДней,
        Выполнено,
        Просрочено,
        Отменено
    }

    [Table("TaskStatus")]
    public partial class TaskStatus
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }
    }
}