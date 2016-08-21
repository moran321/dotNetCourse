using PriceCompare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompare.ViewModel
{
    public class Cart
    {
        public Chain Chain { get; set; }
        public Store Store { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public int CartPrice { get; set; }
        /*---------------------------------*/

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Chain.Name}, {Store.Name} {CartPrice}₪");
            return sb.ToString();
        }

        /*---------------------------------*/
    }
}
