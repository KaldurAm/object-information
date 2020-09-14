namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Pledgers")]
    public partial class Pledgers
    {
      
        [Key]
        public int PledgersId { get; set; }

        public string NameOfPledger { get; set; }

        public string ControllingShareholder { get; set; }
    }
}