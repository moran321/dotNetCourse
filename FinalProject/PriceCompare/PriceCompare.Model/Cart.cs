

using System.Collections.Generic;


namespace PriceCompare.Model
{
    public class Cart
    {
        public string ChainName { get; set; }
        public string StoreName { get; set; }
        public List<ItemQuantity> Items { get; set; }
        public double CartPrice { get; set; }
        /*---------------------------------*/

        public Cart()
        {
            Items = new List<ItemQuantity>();
        }
        /*---------------------------------*/

    }
}
