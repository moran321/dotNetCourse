using System;
using System.Collections.Generic;
using System.Text;


namespace PriceCompare.ViewModel
{
    public class ViewCart
    {
        public string ChainName { get; set; }
        public string StoreName { get; set; }
        public List<SelectedItem> Items { get; set; }
        public double CartPrice { get; set; }
        /*---------------------------------*/

        public ViewCart()
        {
            Items = new List<SelectedItem>();
        }
        /*---------------------------------*/

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{ChainName}, {StoreName}, {CartPrice}0 ₪");
            return sb.ToString();
        }
        /*---------------------------------*/
    }
}
