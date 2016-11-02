
using System.Collections.Generic;
using System.Linq;
using PriceCompare.Model;


namespace PriceCompare.ViewModel
{
    //this class connecting between GUI and DB
    public class DataManager
    {
        private DataGetter _dataGetter;
        public SearchType SearchType { get; set; }
        // public List<string> SuppliersList  { get; set; }
        public Cart Cart { get; private set; }
        /*---------------------------------*/

        public DataManager()
        {
            _dataGetter = new DataGetter();
            Cart = new Cart();
        }
        /*---------------------------------*/



        public Price GetPrice(string chain_name, string store_name, string item_code)
        {
            return _dataGetter.GetPrice(chain_name, store_name, item_code);
        }
        /*---------------------------------*/



        public void AddSelectedItems(IEnumerable<SelectedItem> selectedItems)
        {
            Cart.Items = new List<ItemQuantity>(); //reset
            var itemQuantity = from var in selectedItems
                               select new ItemQuantity
                               {
                                   Item = _dataGetter.GetItemByCode(var.item.ItemCode),
                                   Quantity = var.Quantity
                               };
            Cart.Items.AddRange(itemQuantity);
        }
        /*---------------------------------*/

        public void RemoveItem(ItemQuantity item)
        {
            Cart.Items.Remove(item);
        }
        /*---------------------------------*/



        public void SetSelection(string chain_name, string store_name)
        {
            Cart.StoreName = store_name;
            Cart.ChainName = chain_name;
        }
        /*---------------------------------*/


        //return the cheapest cart in each chain 
        public List<ViewCart> GetCartsPrice()
        {
            var all_carts = _dataGetter.GetFullCartPrices(Cart.Items);

            var carts = from r in all_carts
                        orderby r.CartPrice ascending
                        select r;

            var allitemsstores = from c in carts
                                 where c.Items.Count == Cart.Items.Count
                                 select c;

            var result = from r in allitemsstores
                         select new ViewCart
                         {
                             ChainName = r.ChainName,
                             StoreName = r.StoreName,
                             CartPrice = r.CartPrice,
                             Items = Convert(r.Items)
                         };

            return result.ToList();
        }
        /*---------------------------------*/


        private List<SelectedItem> Convert(List<ItemQuantity> items)
        {
            var converted = from i in items
                            select new SelectedItem
                            {
                                item = new ViewItem() { ItemCode = i.Item.ItemCode.ToString(), ItemName = i.Item.Name },
                                Quantity = i.Quantity,
                                Price = i.Price
                            };
            return converted.ToList();
        }
        /*---------------------------------*/

        /*
    public IEnumerable GetCommonItems()
    {
        var items = _dataGetter.GetCommonItems();
        var list = from it in items
                   select new
                   {
                       it.Name,
                       it.ItemCode
                   };
        return list;
    }
    /*---------------------------------*/

        public List<ViewItem> GetItemsInStore( string chainName, string storeName)
        {
            var items = _dataGetter.GetItemsInStore(chainName,storeName);
            var list = from it in items
                       select new ViewItem
                       {
                           ItemName = it.Name,
                           ItemCode = it.ItemCode.ToString()
                           
                       };
            return list.ToList();
        }
        /*---------------------------------*/



        public List<ViewItem> GetItemsInStore()
        {
            var items = _dataGetter.GetItemsInStore(Cart.ChainName, Cart.StoreName);
            var list = from it in items
                       select new ViewItem
                       {
                           ItemName = it.Name,
                           ItemCode = it.ItemCode.ToString()
                       };
            return list.ToList();
        }
        /*---------------------------------*/



        public List<string> GetStoreNamesOfChain(string chain_name)
        {
            var storesList = _dataGetter.GetStoresOfChain(chain_name);
            var list = from it in storesList
                       select
                           it.Name;


            return list.ToList();
        }
        /*---------------------------------*/


        public List<string> GetSuppliers()
        {
            return GetChainsNames();
        }
        /*---------------------------------*/

        private List<string> GetChainsNames()
        {

            var chainsList = _dataGetter.GetChains();
            var names = new List<string>();
            foreach (var c in chainsList)
            {
                names.Add(c.Name);
            }

            return names;
        }
        /*---------------------------------*/



    }
}
