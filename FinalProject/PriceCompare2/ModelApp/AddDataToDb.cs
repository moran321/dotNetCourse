
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;


namespace PriceCompare.Model.App
{
    public class AddDataToDb
    {


        public void AddChainsToDb(ICollection<Chain> chains)
        {
            if (!chains.Any())
                return;
            using (var db = new PricesContext())
            {
                foreach (var c in chains)
                {
                    try
                    {
                      

                        if (db.Chains.Any(o => o.ChainNumber == c.ChainNumber))
                        {
                            // Match!
                        }
                        else
                        {
                            db.Chains.Add(c);
                            db.SaveChanges();
                        }

                      
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        Console.WriteLine("Chain duplication");
                    }
                }
            }
            Console.WriteLine("* added chains *");
        }
        /*---------------------------------*/

        public void AddStoresToDb(ICollection<Store> stores)
        {
            //  var dbStores = new Dictionary<string, Store>();

            foreach (var s in stores)
            {
                using (var db = new PricesContext())
                {
                    try
                    {
                        // dbStores.Add($"{s.Chain.ChainNumber}_{s.StoreId}", db.Stores.Add(s));

                        //var contains = from st in db.Stores
                        //               where st.StoreId.Equals(s.StoreId) && st.Chain.ChainNumber.Equals(s.Chain.ChainNumber)
                        //               select st;


                        //if (!contains.Any())
                        //{
                        var c = (from ch in db.Chains
                                    where ch.ChainNumber == s.Chain.ChainNumber
                                    select ch).FirstOrDefault();
                        
                        if (c==null)
                            db.Chains.Add(s.Chain);
                        db.Stores.Add(s);

                        //db.Entry(s).State = s.Id == 0 ?
                        //        EntityState.Added :
                        //        EntityState.Modified;

                        db.SaveChanges();
                        //}
                        //else
                        //{
                        //      Console.WriteLine("contains store");

                        //    //  db.Stores.Remove(contains.First());
                        //    //  db.Stores.Add(s);
                        //}

                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        // Console.WriteLine(e.InnerException.InnerException.Message);
                        Console.Write("store exception, ");
                        //  throw;
                    }
                    Console.Write(" add store: *{0}* , ", stores.First().ChainId);
                }


            }

            // return dbStores;
        }
        /*---------------------------------*/

            /*
        public void AddItemsToDb(ICollection<Item> items)
        {
            foreach (var it in items)
            {
                using (var db = new PricesContext())
                {
                    try
                    {
                        db.Items.Add(it);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        Console.WriteLine(e.InnerException.InnerException.Message);
                    }
                }
            }
        }
        /*---------------------------------*/


        public void AddPricesToDb(ICollection<Price> prices)
        {
            foreach (var p in prices)
            {
                using (var db = new PricesContext())
                {
                    try
                    {
                        //  dbStores.Add($"{s.Chain.ChainNumber}_{s.StoreId}", db.Stores.Add(s));
                        //var contains = from pr in db.Prices
                        //               where pr.StoreId.Equals(p.StoreId)
                        //               && pr.ChainId.Equals(p.ChainId)
                        //               && pr.Item.ItemCode.Equals(p.Item.ItemCode)
                        //               select pr;
                        //if (!contains.Any())
                        //{

                        var item = (from it in db.Items
                                 where it.ItemCode == p.ItemId
                                 select it).FirstOrDefault();

                        if (item == null)
                            db.Items.Add(p.Item);

                        db.Prices.Add(p);

                        //}
                        //else
                        //{
                        //    //  Console.WriteLine("contains Price");
                        //    // db.Prices.Remove(contains.First());
                        //    //  db.Prices.Add(p);
                        //}


                        db.Entry(p).State = p.PriceId == 0 ?
                                  EntityState.Added :
                                  EntityState.Modified;

                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        SqlException innerException = e.InnerException.InnerException as SqlException;
                        if (innerException != null && (innerException.Number == 2627 ||
                            innerException.Number == 2601))
                        {
                            Console.WriteLine(e.InnerException.InnerException.Message);
                        }
                        else
                        {
                            Console.WriteLine(e.InnerException.InnerException.Message);
                            throw;
                        }
                    }
                }
                Console.WriteLine(" add prices of: *{0}*", prices.First().ChainId);
            }
        }
        /*---------------------------------*/


        /*---------------------------------*/
    }
}
