using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInformation.DAL.Model
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(255)]
        public string Name { get; set; }
    }
}
