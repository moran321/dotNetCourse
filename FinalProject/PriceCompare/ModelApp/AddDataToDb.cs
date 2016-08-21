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
            using (var db = new PricesContext())
            {
                foreach (var s in stores)
                {
                    try
                    {
                        db.Stores.Add(s);
                        db.SaveChanges();
                      
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        Console.WriteLine(e.InnerException.InnerException.Message);
                       // db.Chains.Remove(s.Chain);
                      //  db.Stores.Add(s);
                        // throw;
                    }
                }
                Console.WriteLine(" add stores ");
            }
        }
        /*---------------------------------*/

        public void AddItemsToDb(ICollection<Item> items)
        {
            using (var db = new PricesContext())
            {
                foreach (var it in items)
                {
                    try
                    {
                        db.Items.Add(it);
                        db.SaveChanges();
                        Console.WriteLine(" add items ");
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        Console.WriteLine(e.InnerException.InnerException.Message);

                        string format1 = "yy-mm-dd HH:mm:ss";
                        string format2 = "yy-mm-dd HH:mm";

                        var dateTime = DateTime.ParseExact(it.UpdateTime, format1,
                                            CultureInfo.InvariantCulture);
                        var dateTime2 = DateTime.ParseExact(it.UpdateTime, format2,
                                           CultureInfo.InvariantCulture);

                        var oldItem = db.Items.Where(x => x.ItemCode.Equals(it.ItemCode)).FirstOrDefault();
                        DateTime oldItemUpdataTime = DateTime.ParseExact(oldItem.UpdateTime, "yy-mm-dd HH:mm:ss",
                                            CultureInfo.InvariantCulture);
                        if (dateTime.CompareTo(oldItemUpdataTime) > 0) //the newer is later
                        {
                            db.Items.Remove(oldItem);
                            db.Items.Add(it);
                            db.SaveChanges();
                        }
                        else if (dateTime.CompareTo(oldItemUpdataTime) == 0)
                        {
                            //the same updateTime
                        }

                        // throw;
                    }
                }
            }
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
                        db.Prices.Add(p);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        SqlException innerException = e.InnerException.InnerException as SqlException;
                        if (innerException != null && (innerException.Number == 2627 ||
                            innerException.Number == 2601))
                        {
                            Console.WriteLine(e.InnerException.InnerException.Message);
                           // db.Prices.Remove(p);
                           // db.Prices.Add(p);
                        }
                        else
                        {
                            throw;
                        }
                    }
                  

                }
                Console.WriteLine(" add prices ");
            }
        }
        /*---------------------------------*/


            /*
        public void AddChainToDb()
        {
            var chains = new List<Chain>()
        {
            new Chain() {Name="hazihinam", ChainId= 7290700100008 },
            new Chain() {Name="mega", ChainId= 7290055700007 },
            new Chain() {Name="ramilevi", ChainId= 7290058140886 },
            new Chain() {Name="shufersal", ChainId= 7290027600007 },
        };

            using (var db = new PricesContext())
            {
                try
                {
                    db.Chains.AddRange(chains);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    Console.WriteLine(e.InnerException.InnerException.Message);
                    // throw;
                }
            }
        }

        /*---------------------------------*/

    }
}
