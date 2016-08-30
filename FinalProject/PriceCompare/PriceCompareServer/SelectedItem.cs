

namespace PriceCompare.ViewModel
{
    public class SelectedItem
    {
        public ViewItem item;
        public int Quantity { get; set; }
        public double Price { get; set; }

        public SelectedItem()
        {
            item = new ViewItem();
        }

        public override string ToString()
        { 
            
            return $"{item.ItemName} {Price}0₪";
        }
    }
}
