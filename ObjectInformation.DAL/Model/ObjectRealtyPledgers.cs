using System;

namespace ObjectInformation.DAL.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ObjectRealtyPledgers")]
    public partial class ObjectRealtyPledgers
    {
        [Key]
        public int ObjectRealtyPledgersId { get; set; }

        public DateTime CreateDate { get; set; }

        public int ObjectRealtyId { get; set; }
        public int PledgersId { get; set; }

        public DateTime? PledgeDate { get; set; }

        public double MortgageValue { get; set; }

        public double AssessedValue { get; set; }

        public DateTime? EvaluationDate { get; set; }

        public Pledgers Pledgers { get; set; }
    }
}