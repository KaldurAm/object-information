namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Currency")]
    public partial class Currency
    { 
        [Key]
        public int CurrencyId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}