namespace PriceCompare.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Price
    {
        public int PriceId { get; set; }

        public long StoreId { get; set; }

        public long ChainId { get; set; }

        [Required]
        public double ItemPrice { get; set; }

        public string UnitQty { get; set; }

        public string Quantity { get; set; }

        public string UpdateDate { get; set; }

        public virtual Item Item { get; set; }

       // [ForeignKey("StoreId")]
      //  public virtual Store Store { get; set; } //@@


    }
}
