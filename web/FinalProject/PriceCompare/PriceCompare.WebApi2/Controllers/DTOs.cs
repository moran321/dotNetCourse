using System;
using System.Collections.Generic;


namespace PriceCompare.WebApi2.Controllers
{
    public class ItemDTO
    {
        public long ItemCode { get; set; }
        public string Name { get; set; }
        public string UnitQuantity { get; set; }
    }

    public class ChainDTO
    {
        public long ChainNumber { get; set; }
        public string Name { get; set; }
        public virtual ICollection<StoreDTO> Stores { get; set; }
    }

    public class StoreDTO
    {
        public long StoreId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public virtual ChainDTO Chain { get; set; }
        public virtual ICollection<PriceDTO> Prices { get; set; }
    }

    public class PriceDTO
    {
        public long StoreId { get; set; }
        public long ChainId { get; set; }
        public double ItemPrice { get; set; }
        public virtual ItemDTO Item { get; set; }
    }
}
