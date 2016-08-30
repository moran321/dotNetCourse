using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceCompare.Model;
using System.Data.Entity;
using System.Collections;

namespace PriceCompare.ViewModel
{
    //this class connecting between GUI and DB
    public class DataManager
    {
        private DataGetter _dataGetter;
        public SearchType SearchType { get; set; }
        //   public IEnumerable<Item> Selecteditems { get; private set; }
        // public List<ItemQuantity> Selecteditems { get; private set; }
        //   public Chain SelectedChain { get; private set; }
        //   public Store SelectedStore { get; private set; }
        //public List<Cart> Carts { get; private set; }
        public Cart Cart { get; private set; }
        public DataManager()
        {
            _dataGetter = new DataGetter();
            Cart = new Cart();
        }
        /*---------------------------------*/



        public void AddSelectedItem(string code, string name, int quantity)
        {
            var item = _dataGetter.GetItemByCode(code);
            Cart.Items.Add(new ItemQuantity() { Item = item, Quantity = quantity });
        }
        /*---------------------------------*/



        /*---------------------------------*/

        public void AddSelectedItems(IEnumerable<SelectedItem> selectedItems)
        {
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

        /*
    public void SetSelecteditems(IEnumerable<ItemQuantity> items)
    {
        var list = _dataGetter.GetItemsInStore(SelectedChain.Name, SelectedStore.Name);

        var q = from sp in list
                join p in items on sp.Name equals p.ToString()
                select sp;
        Selecteditems = q;
    }
    /*---------------------------------*/



        public void SetSelection(string chain_name, string store_name)
        {
            Cart.StoreName = store_name;
            Cart.ChainName = chain_name;
            //Cart.Store = _dataGetter.GetStore(supplier, branch);
            //Cart.Chain = _dataGetter.GetChain(supplier);
        }
        /*---------------------------------*/

        /*
                public List<Cart> CalculateCartsPrice()
                {

                    double totalPrice = 0;
                    var chainsList = _dataGetter.GetChains();
                    foreach (var ch in chainsList)
                    {
                        var storesList = _dataGetter.GetStoresOfChain(ch.Name);
                        foreach (var st in storesList)
                        {
                            //calculate cart total price
                            foreach (ItemQuantity item in Selecteditems)
                            {
                                var price = _dataGetter.GetPrice(ch, st, item.ItemCode);
                                if (price == null) continue;
                                double d_price;
                                if (double.TryParse(price.ItemPrice, out d_price))
                                {
                                    totalPrice += d_price;
                                }
                            }
                            var cart = new Cart() { Chain = ch, Store = st, CartPrice = Convert.ToInt32(Math.Round(totalPrice)), Items = Selecteditems };
                            Carts.Add(cart);

                            totalPrice = 0;
                        }

                    }
                    return Carts;
                }
                /*---------------------------------*/

        //return the prices in all stores with all items
        public List<ViewCart> GetCartsPrice()
        {
            var result = _dataGetter.GetFullCartPrices(Cart.Items);
            var carts = from r in result
                        select new ViewCart
                        {
                            ChainName = r.ChainName,
                            StoreName = r.StoreName,
                            CartPrice = r.CartPrice,
                            Items = Convert(r.Items)
                        };
            return carts.ToList();
        }
        /*---------------------------------*/


        private List<SelectedItem> Convert(List<ItemQuantity> items)
        {
            var converted = from i in items
                            select new SelectedItem
                            {
                                item = new ViewItem() { ItemCode = i.Item.ItemCode, ItemName = i.Item.Name },
                                Quantity = i.Quantity
                            };
            return converted.ToList();
        }
        /*---------------------------------*/

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

        public ICollection GetItemsInStore()
        {
            var items = _dataGetter.GetItemsInStore(Cart.ChainName, Cart.StoreName);
            var list = from it in items
                       select new
                       {
                           Name =it.Name,
                           Code =it.ItemCode
                       };
            return list.ToList();
        }




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
