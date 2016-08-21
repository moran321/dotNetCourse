using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceCompare.Model;
using System.Data.Entity;

namespace PriceCompare.ViewModel
{
    //this class connecting between GUI and DB
    //the GUI asks for data and this class searches the data in DB
    public class DataManager
    {
        DataGetter _dataGetter;
        public IEnumerable<Item> Selecteditems { get; private set; }
        public Chain SelectedChain { get; private set; }
        public Store SelectedStore { get; private set; }
        public List<Cart> Carts { get; private set; }

        public DataManager()
        {
            _dataGetter = new DataGetter();
            Carts = new List<Cart>();
        }

        /*---------------------------------*/


        public void SetSelecteditems(IEnumerable<string> items)
        {
            var list = _dataGetter.GetItemsInStore(SelectedChain.Name, SelectedStore.Name);

            var q = from sp in list
                    join p in items on sp.Name equals p.ToString()
                    select sp;

            // Selecteditems = list.Where(x => x.Name.Equals(items.Select(y => y.ToString())));
            Selecteditems = q;

        }
        /*---------------------------------*/

        public void SetSelection(string supplier, string branch)
        {
            SelectedStore = _dataGetter.GetStore(supplier, branch);
            SelectedChain = _dataGetter.GetChain(supplier);
        }
        /*---------------------------------*/


        public List<Cart> CalculateCartsPrice()
        {
            bool isHaveAll = true;
            double totalPrice = 0;
            var chainsList = _dataGetter.GetChains();
            foreach (var ch in chainsList)
            {
                var storesList = _dataGetter.GetStoresOfChain(ch.Name);
                foreach (var st in storesList) 
                {
                    //calculate cart total price
                    foreach (Item item in Selecteditems)
                    {
                        var price = _dataGetter.GetPrice(ch, st, item);
                        if (price == null) //one item not found
                        {
                            isHaveAll = false;
                            break;
                        }
                           
                        double d_price;
                        if (double.TryParse(price.ItemPrice, out d_price))
                        {
                            totalPrice += d_price;
                        }
                    }
                    if (totalPrice > 0 && isHaveAll)
                    {
                        var cart = new Cart() { Chain = ch, Store = st, CartPrice = Convert.ToInt32(Math.Round(totalPrice)), Items = Selecteditems };
                        Carts.Add(cart);
                    }
                    totalPrice = 0;
                }

            }
            return Carts;
        }
        /*---------------------------------*/



        public List<string> GetItems()
        {
            var list = _dataGetter.GetItemsInStore(SelectedChain.Name, SelectedStore.Name);
            var names = new List<string>();
            foreach (var c in list)
            {
                names.Add(c.Name);
            }
            return names;
        }
        /*---------------------------------*/


        public List<string> GetStoreNamesOfChain(string chain_name)
        {
            var storesList = _dataGetter.GetStoresOfChain(chain_name);
            var names = new List<string>();
            foreach (var c in storesList)
            {
                names.Add(c.Name);
            }

            return names;
        }
        /*---------------------------------*/



        //get all suppliers
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
