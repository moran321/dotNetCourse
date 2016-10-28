namespace PriceCompare.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Chain : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chain()
        {
            Stores = new List<Store>();
        }

        public int Id { get; set; }

       // [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public long ChainNumber { get; set; }

        public string Name { get; set; }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Store> Stores { get; set; }

        public override int GetHashCode()
        {
            return ChainNumber.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            var chain = obj as Chain;
            if (chain == null)
            {
                return false;
            }
            return ChainNumber.Equals(((Chain)obj).ChainNumber);
        }


    }
}
