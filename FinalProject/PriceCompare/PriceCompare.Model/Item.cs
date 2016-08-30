namespace PriceCompare.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Prices = new HashSet<Price>();
        }

      //  [Key]
     //   [Column(Order = 0)]
        public int Id { get; set; }

        public string ItemCode { get; set; }
   
       // public string ChainNumber { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string UnitQuantity { get; set; }

        public string UpdateTime { get; set; }

        public string ManufacturerName { get; set; }

      //  [StringLength(128)]
       // public string StoreId { get; set; }

      //  [StringLength(128)]
      //  public string ChainName { get; set; }


     //   public virtual Store Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Price> Prices { get; set; }


    }
}
