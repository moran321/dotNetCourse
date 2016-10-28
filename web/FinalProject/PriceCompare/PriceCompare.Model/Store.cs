namespace PriceCompare.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Store : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            Prices = new HashSet<Price>();
        }


          public int Id { get; set; }

      //  [Key]
     //   [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long StoreId { get; set; }

     //   [Key]
      //  [Column(Order = 1)]
        public int ChainId { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string City { get; set; }


        [ForeignKey("ChainId")]
        public virtual Chain Chain { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Price> Prices { get; set; }


        public override int GetHashCode()
        {
            // return Chain.Id.GetHashCode() ^ StoreId.GetHashCode();
            return Chain.ChainNumber.GetHashCode() ^ StoreId.GetHashCode();

        }
        public override bool Equals(object obj)
        {
            var duplicate = (Store)obj;
            return Chain.ChainNumber.Equals(duplicate.Chain.ChainNumber) && StoreId.Equals(duplicate.StoreId);
            // return Chain.Id.Equals(duplicate.Chain.Id) && StoreId.Equals(duplicate.StoreId);
        }

    }
}
