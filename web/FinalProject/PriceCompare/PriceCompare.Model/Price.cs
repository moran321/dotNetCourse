namespace PriceCompare.Model
{

    using System.ComponentModel.DataAnnotations;

    public partial class Price : IEntity
    {

        public long StoreId { get; set; }

        public long ChainId { get; set; }

        [Required]
        public double ItemPrice { get; set; }

        public string UnitQty { get; set; }

        public string Quantity { get; set; }

        public string UpdateDate { get; set; }

        public virtual Item Item { get; set; }

        public int Id { get; set; }


    }
}
