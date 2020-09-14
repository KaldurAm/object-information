using System;

namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ObjectRealty")]
    public partial class ObjectRealty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObjectRealty()
        {
            Comments = new HashSet<Comment>();
            ObjectProperties = new HashSet<ObjectProperty>();
            Uploads = new HashSet<Upload>();
        }

        [Key]
        public int ObjectRealtyId { get; set; }

        [Required]
        public int ObjectTypeId { get; set; }

        [Required(ErrorMessage = "Это поле является обязательным!")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Это поле является обязательным!")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Это поле является обязательным!")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Это поле является обязательным!")]
        public int DistrictId { get; set; }



        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        public int CurrencyId { get; set; }

        public DateTime? CostDate { get; set; }
        public DateTime? DateOfSale { get; set; }

        public double CurrencyRate { get; set; }

        [Required]
        public double Square { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }

        public string lat { get; set; }
        public string lng { get; set; }
        public string zoom { get; set; }

        [Column(name: "CadastralNumber")]
        public string CadastralNumber { get; set; }

        [Column(name: "CostDCT")]
        public double CostDCT { get; set; }
        public string Address { get; set; }



        public virtual Country Country { get; set; }
        public virtual Region Region { get; set; }
        public virtual City City { get; set; }
        public virtual District District { get; set; }

        public string GetFullAddress()
        {
            string[] addressArr = new string[5];
            addressArr[0] = Country!=null ? Country.CountryName : "";
            addressArr[1] = Region!=null?Region.RegionName: "";
            addressArr[2] = City!=null ? City.CityName: "";
            addressArr[3] = District!=null ? District.DistrictName: "";
            addressArr[3] = Address;

            string address = string.Join(", ", addressArr);
            return address.Substring(0,address.Length-2);
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjectProperty> ObjectProperties { get; set; }

        public virtual ObjectType ObjectType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Upload> Uploads { get; set; }

        public virtual Currency Currency { get; set; }
    }
}