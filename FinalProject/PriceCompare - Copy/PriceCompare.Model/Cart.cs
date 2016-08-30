
using System;
using System.Collections.Generic;
using System.Text;


namespace PriceCompare.Model
{
    public class Cart
    {
        //   public Chain Chain { get; set; }
        //   public Store Store { get; set; }
        public string ChainName { get; set; }
        public string StoreName { get; set; }
        public List<ItemQuantity> Items { get; set; }
        public string CartPrice { get; set; }
        /*---------------------------------*/


        public Cart()
        {
            Items = new List<ItemQuantity>();
        }
        /*---------------------------------*/

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{ChainName}, {StoreName}: {CartPrice}₪");
            return sb.ToString();
        }
        /*---------------------------------*/
    }
}
