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

        [Required]
        public string ItemPrice { get; set; }

        public string UnitQty { get; set; }

        public string Quantity { get; set; }

        public string UpdateDate { get; set; }

       // public string ItemCode { get; set; }

        public virtual Item Item { get; set; }

    }
}
