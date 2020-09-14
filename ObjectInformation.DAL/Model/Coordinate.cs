namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Coordinate")]
    public class Coordinate
    {
        [Key]
        [Required(ErrorMessage = "Это поле является обязательным!")]
        public int CoordinateId { get; set; }

        public int PolygonId { get; set; }

        public double lat { get; set; }
                
        public double lng { get; set; }

        
    }
}