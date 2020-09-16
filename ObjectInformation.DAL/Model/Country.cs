namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            Regions = new HashSet<Region>();
        }

        [Key]
        [Required(ErrorMessage = "Это поле является обязательным!")]
        public int CountryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
    }
}