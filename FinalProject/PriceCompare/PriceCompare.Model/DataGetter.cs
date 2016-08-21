using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCompare.Model
{

    public class DataGetter
    {


        public List<Chain> GetChains()
        {
            List<Chain> list = null;
            using (var db = new PricesContext())
            {
                list = db.Chains.ToList();
                return list;
            }

        }
        /*---------------------------------*/

        public IEnumerable<Store> GetStoresOfChain(string chain_name)
        {
            using (var db = new PricesContext())
            {
                //  var list = db.Stores.Where((c) => c.ChainName.Equals(chain_name)).ToList();
                var list = db.Stores.Where((c) => c.Chain.Name.Equals(chain_name)).ToList();
                return list;
            }
        }
        /*---------------------------------*/

        public List<Item> GetItemsInStore(string chain_name, string store_name)
        {
            // IQueryable<List<Item>> list = null;
            using (var db = new PricesContext())
            {
               //  var Id = db.Stores.Where(x => x.Name.Equals(store_name)).Select(y=>y.Chain.ChainId).FirstOrDefault();
                var chainid = (from c in db.Chains
                              where c.Name.Equals(chain_name)
                              select c.ChainNumber).FirstOrDefault();

                //string sId = Id.ToString();
                var list = (from x in db.Items
                            where x.ChainNumber.Equals(chainid)
                            select x).ToList();

                // var list = db.Items.
                //      Where(x => x.ChainId.ToString().Equals(Id.ToString())).ToList(); //check also store!

                return list;
            }
        }
        /*---------------------------------*/


        public Store GetStore(string chain_name, string store_name)
        {
            ;
            using (var db = new PricesContext())
            {
                //var store = db.Stores.Where(s => s.ChainName.Equals(chain_name) && s.Name.Equals(store_name)).FirstOrDefault();
                var store = db.Stores.Where(s => s.Chain.Name.Equals(chain_name) && s.Name.Equals(store_name)).FirstOrDefault();
                return store;
            }

        }
        /*---------------------------------*/

        public Chain GetChain(string chain_name)
        {
            using (var db = new PricesContext())
            {
                var chain = db.Chains.Where(s => s.Name.Equals(chain_name)).FirstOrDefault();
                return chain;
            }
        }
        /*---------------------------------*/



        public List<Price> GetPrices(Item item)
        {
            using (var db = new PricesContext())
            {
                var prices = db.Prices.Where(i => i.Item.ItemCode.Equals(item.ItemCode));
                return prices.ToList();
            }
        }
        /*---------------------------------*/


        public Price GetPrice(Chain chain, Store store,Item item)
        {
            using (var db = new PricesContext())
            {
                var price = (db.Prices.Where(i => i.Item.ItemCode.Equals(item.ItemCode) 
                && i.StoreId==store.StoreId.ToString()
                && i.Item.ChainNumber==(chain.ChainNumber))).FirstOrDefault();
                return price;
            }
        }
        /*---------------------------------*/

    }


}
