//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PriceCompare.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Price
    {
        public int Id { get; set; }
        public long StoreId { get; set; }
        public long ChainId { get; set; }
        public double ItemPrice { get; set; }
        public string UnitQty { get; set; }
        public string Quantity { get; set; }
        public string UpdateDate { get; set; }
        public int Item_Id { get; set; }
        public Nullable<int> Store_Id { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Store Store { get; set; }
    }
}
