using PriceCompare.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApp
{
    public class AddDataToDb
    {
        public void AddStoresToDb(ICollection<Store> stores)
        {
            //  var dbStores = new Dictionary<string, Store>();
            using (var db = new PricesContext())
            {
                foreach (var s in stores)
                {
                    try
                    {
                        // dbStores.Add($"{s.Chain.ChainNumber}_{s.StoreId}", db.Stores.Add(s));

                        var contains = from st in db.Stores
                                       where st.StoreId.Equals(s.StoreId) && st.Chain.ChainNumber.Equals(s.Chain.ChainNumber)
                                       select st;


                        if (!contains.Any())
                        {
                            db.Stores.Add(s);

                        }
                        else
                        {
                            Console.WriteLine("contains store");
                         //   db.Stores.Remove(contains.First());
                         //   db.Stores.Add(s);
                        }
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        Console.WriteLine(e.InnerException.InnerException.Message);
                        throw;
                    }
                }
                Console.WriteLine(" add stores of: *{0}*", stores.First().Chain.Name);
            }

            // return dbStores;
        }
        /*---------------------------------*/



        public void AddPricesToDb(ICollection<Price> prices)
        {
            using (var db = new PricesContext())
            {

                foreach (var p in prices)
                {
                    try
                    {
                        //  dbStores.Add($"{s.Chain.ChainNumber}_{s.StoreId}", db.Stores.Add(s));
                        var contains = from pr in db.Prices
                                       where pr.StoreId.Equals(p.StoreId) 
                                       && pr.ChainId.Equals(p.ChainId) 
                                       && pr.Item.ItemCode.Equals(p.Item.ItemCode)
                                       select pr;
                        if (!contains.Any())
                        {

                            db.Prices.Add(p);

                        }
                        else
                        {
                            Console.WriteLine("contains Price");
                           // db.Prices.Remove(contains.First());
                          //  db.Prices.Add(p);
                        }
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        SqlException innerException = e.InnerException.InnerException as SqlException;
                        if (innerException != null && (innerException.Number == 2627 ||
                            innerException.Number == 2601))
                        {
                            Console.WriteLine(e.InnerException.InnerException.Message);
                            throw;
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
