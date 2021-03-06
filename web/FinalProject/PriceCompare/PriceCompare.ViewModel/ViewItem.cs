﻿

namespace PriceCompare.ViewModel
{
   public class ViewItem
    {
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public double ItemPrice { get; set; }

        public override string ToString()
        {
            return $"{ItemName} {ItemCode} {ItemPrice}0₪";
        }
    }
}
