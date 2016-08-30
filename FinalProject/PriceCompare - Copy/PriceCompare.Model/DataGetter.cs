using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


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

                return list;
            }
        }
        /*---------------------------------*/


        public Item GetItemByCode(string itemcode)
        {
            using (var db = new PricesContext())
            {
                var item = from i in db.Items
                           where i.ItemCode == itemcode
                           select i;
                return item.FirstOrDefault();
            }
        }

        public Store GetStore(string chain_name, string store_name)
        {
            using (var db = new PricesContext())
            {
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



        public double GetPrice(string chainNumber, string storeId, string itemCode)
        {
            using (var db = new PricesContext())
            {

                var price = from p in db.Prices
                            where p.Item.ItemCode == itemCode && p.StoreId == storeId && p.Item.ChainNumber == chainNumber
                            select p;
                if (price == null)
                    return 0;
                return Convert.ToDouble(price.First().ItemPrice);
            }
        }



        /*---------------------------------*/

        /*
    public Price GetPrice(Chain chain, Store store, string itemCode)
    {
        using (var db = new PricesContext())
        {
            var price = (db.Prices.Where(i => i.Item.ItemCode.Equals(itemCode)
            && i.StoreId.Equals(store.StoreId)
            && i.Item.ChainNumber.Equals(chain.ChainNumber))).FirstOrDefault();
            return price;
        }
    }
    /*---------------------------------*/

        //return the the most common items
        public List<Item> GetCommonItems()
        {
            using (var db = new PricesContext())
            {

                var num_of_stores = (from s in db.Stores select new { s.StoreId, s.Chain.ChainNumber }).Distinct().Count();

                var mutualItemsInStores = from it in db.Items
                                          group it.ItemCode by it.StoreId into Count
                                          orderby Count.Count()
                                          where Count.Count() == num_of_stores
                                          select (from it in db.Items
                                                  where it.ItemCode.Equals(Count.Key)
                                                  select it);

                /*
                //count appearance in stores
                var mutualItemsInStores = (from it in db.Items

                                           group it.Name by it.StoreId into Count
                                           orderby Count.Count() descending
                                           select new
                                           {
                                               Name = Count.Key,
                                               Count = Count.Count()
                                           });

                var biggestCount = mutualItemsInStores.Max(x => x.Count);

                var mostCommonItems = from it in mutualItemsInStores
                                          where it.Count == biggestCount
                                          select it.Name;
                                          */

                return mutualItemsInStores.First().ToList();

            }
        }
        /*---------------------------------*/

        public List<Cart> GetFullCartPrices(List<ItemQuantity> items)
        {
            using (var db = new PricesContext())
            {
                var allprices = items.Select(x => x.Item.Prices).First();
                var itemsListCodes = items.Select(x => x.Item.ItemCode);


                var result = from s in db.Stores
                             where allprices.Except(s.Prices).Count() == 0
                             select new Cart
                             {
                                 ChainName = s.Chain.Name,
                                 CartPrice = (from p in s.Prices
                                              where p.Item.ItemCode.Equals(itemsListCodes)
                                              select p.ItemPrice).Sum().ToString()

                             };
                return result.ToList();
            }

        }
        //return prices of stores that contains all items
        /*
        public List<Cart> GetFullCartPrices(List<ItemQuantity> items)
        {
            using (var db = new PricesContext())
            {
                var codes = items.Select(x => x.Item.ItemCode).First();
                var itemsObj = from it in db.Items
                               where codes.Equals(it.ItemCode)
                               group it by it.ItemCode into Unique
                               select Unique.FirstOrDefault();

                //var result = (from it in db.Stores
                //              where itemsObj.Except(it.Items).Count() == 0 //all the items exist in store
                //              select new Cart
                //              {
                //                  Chain = it.Chain,
                //                  Store = it,
                //                  //Adress = it.Adress,
                //                  CartPrice = CalculateCartPrice(it, items.Select(x=>x.ItemCode)),
                //              });

                var stores = (from it in db.Stores
                             where itemsObj.Except(it.Items).Count() == 0 //all the items exist in store
                             select it).ToList();

                var joined = (from it in db.Items
                           join p in db.Prices on it.Id equals p.Id
                           select new
                           {
                               itemCode = it.ItemCode,
                               StoreId = it.StoreId,
                               Price= p.ItemPrice
                           }).ToList();
                           
                           

                var result= from s in stores
                            select new Cart
                             {
                                 ChainName = s.Chain.Name,
                                 StoreName = s.StoreId,
                                 //Adress = it.Adress,
                                 CartPrice =  
                                             ((from it in joined
                                             where it.StoreId.Equals(s.StoreId)
                                             select it.Price).FirstOrDefault())          

                            };


                return result.ToList();
            }
        }
        /*---------------------------------*/

                //return prices in all stores that contatins at least one item
                /*
                public List<Cart> GetAllStoresPrices(List<ItemQuantity> items)
                {
                    using (var db = new PricesContext())
                    {
                        //calculate total price for each store

                        var result = (from it in db.Stores
                                      select new Cart
                                      {
                                          ChainName = it.Chain.Name,
                                          StoreName = it.Name,
                                          //Adress = it.Adress,
                                          CartPrice = CalculateCartPrice(it, items.Select(x => x.Item.ItemCode)),
                                      }).Where(x => x.TotalPrice > 0).ToList();
                        return result;

                    }
                }
                /*---------------------------------*/


        public double CalculateCartPrice(Store store, IEnumerable<string> itemsCode)
        {
            using (var db = new PricesContext())
            {
                //find all items' prices in store
                var allPrices = from p in db.Prices
                                join it in itemsCode on p.Item.ItemCode equals it
                                select p.ItemPrice;
                //summarize all prices of items
                var total = allPrices.Select(x => Convert.ToDouble(x)).Sum();
                return total;
            }
        }
        /*---------------------------------*/

        public IEnumerable<string> GetCities()
        {
            using (var db = new PricesContext())
            {
                var cities = from s in db.Stores
                             select s.City;
                return cities.ToList();

            }

        }/*---------------------------------*/


        /*---------------------------------*/
    }
}