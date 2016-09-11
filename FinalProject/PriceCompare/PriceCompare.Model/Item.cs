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

       // public int Id { get; set; }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ItemCode { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string UnitQuantity { get; set; }

        public string UpdateTime { get; set; }

        public string ManufacturerName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Price> Prices { get; set; }


        public override int GetHashCode()
        {        
            return ItemCode.GetHashCode() ^ Name.GetHashCode();

        }
        public override bool Equals(object obj)
        {
            var duplicate = (Item)obj;
            return ItemCode.Equals(duplicate.ItemCode) && Name.Equals(duplicate.Name);
        }
    }
}
