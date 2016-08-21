namespace PriceCompare.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
       // [Key]
       // [Column(Order = 0)]
        public string StoreId { get; set; }

     //   [Key]
     //   [Column(Order = 1)]
      //  public long? ChainId { get; set; }

     //   public string ChainName { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string City { get; set; }

        public string UpdateTime { get; set; }


        public virtual Chain Chain { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }


    }
}
