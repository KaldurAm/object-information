namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Polygon")]
    public class Polygon
    {
        [Key]
        [Required(ErrorMessage = "Это поле является обязательным!")]
        public int PolygonId { get; set; }

        public string PolygonName { get; set; }
                
        public string PolygonDescription { get; set; }

        public int ObjectRealtyId { get; set; }

        [NotMapped]
        public List<Coordinate> coords = new List<Coordinate>();
    }
}