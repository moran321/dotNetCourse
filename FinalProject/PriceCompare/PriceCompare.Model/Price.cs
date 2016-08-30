namespace PriceCompare.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Price
    {
        public int Id { get; set; }

        public string StoreId { get; set; }
        public string ChainId { get; set; }

        [Required]
        public double ItemPrice { get; set; }

        public string UnitQty { get; set; }

        public string Quantity { get; set; }

        public string UpdateDate { get; set; }

      

        public virtual Item Item { get; set; }

       // public virtual Store Store { get; set; }

    }
}
