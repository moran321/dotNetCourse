using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace PriceCompare.Model.App
{
    public static class Extentions
    {
        /*
        public static Item AddOrUpdate<T>(this DbSet<T> dbset, Item item, PricesContext db) where T : class
        {
            db.Entry(item).State = item.Id == 0 ?
                    EntityState.Added :
                    EntityState.Modified;

            if (db.Entry(item).State == EntityState.Modified)
            {
                return item;
            }
            var newEntity = db.Set<Item>().Add(item);
            return newEntity;
        }
        /*---------------------------------*/

        // public static System.Data.Entity.Infrastructure.DbEntityEntry<T> GetEntity<T>(this DbSet<T> dbset, T obj, PricesContext db) where T : class
        public static T AddOrUpdate<T>(this DbSet<T> dbset, T obj, PricesContext db) where T : class
        {
            var entity_obj = obj as IEntity;

            db.Entry(obj).State = entity_obj.Id == 0 ?
                      EntityState.Added :
                      EntityState.Modified;

            //if the entity already exist in db
            if (db.Entry(obj).State == EntityState.Modified)
            {
                //TODO: update by the latest date
                return obj;
            }
            var newEntity = db.Set<T>().Add(obj);
            return newEntity;
        }
        /*---------------------------------*/

    }
    /*------------------------------------------------------------*/

    public class AddDataToDb
    {



        public void InsertOrUpdate(ICollection<Price> entity)
        {
            using (var db = new PricesContext())
            {
                foreach (var en in entity)
                {
                    try
                    {
                        var item = db.Set<Item>().AddOrUpdate((en.Item), db);
                        en.Item = item;
                        var price = db.Set<Price>().Add(en);

                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        Console.WriteLine(e.InnerException.InnerException.Message);
                    }
                }
            }
            Console.WriteLine("Price added");
        }
        /*---------------------------------*/




        public void InsertOrUpdate(ICollection<Store> entity)
        {
            using (var db = new PricesContext())
            {
                foreach (var en in entity)
                {
                    try
                    {
                        var chain = db.Set<Chain>().AddOrUpdate((en.Chain), db);
                        en.Chain = chain;
                        var store = db.Set<Store>().Add(en);

                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        Console.WriteLine(e.InnerException.InnerException.Message);
                    }
                }
            }
            Console.WriteLine("Store added");
        }
        /*---------------------------------*/

            /*
        public void InsertOrUpdate<T>(ICollection<T> entity) where T : class
        {
            using (var db = new PricesContext())
            {
                foreach (var en in entity)
                {
                    try
                    {

                        if (db.Entry(en).State == EntityState.Detached)
                        {
                            var exist_entry = db.Set<T>().AddOrUpdate(en, db);
                            db.SaveChanges();

                        }
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {
                        Console.WriteLine(e.InnerException.InnerException.Message);
                    }

                }
                Console.WriteLine("entity added");
            }
        }
        /*---------------------------------*/

        /*
                public void AddPricesToDb(ICollection<Price> prices)
                {
                    foreach (var p in prices)
                    {
                        InsertOrUpdate(p);
                    }
                    Console.WriteLine(" add prices of: *{0}*", prices.First().ChainId);
                }
                /*---------------------------------*/

        /*
    public void AddStoresToDb(ICollection<Store> stores)
    {
        foreach (var s in stores)
        {
            InsertOrUpdate(s);
        }
        Console.WriteLine(" add stores of: *{0}*", stores.First().ChainId);
    }
    /*---------------------------------*/


        /*
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
                    db.Chains.Add(c);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    Console.WriteLine(e.InnerException.InnerException.Message);
                }
            }
        }
        Console.WriteLine(" add chains");
    }
    /*---------------------------------*/

        /*
    public void AddStoresToDb(ICollection<Store> stores)
    {
        //  var dbStores = new Dictionary<string, Store>();

        foreach (var s in stores)
        {
            using (var db = new PricesContext())
            {
                try
                {

                    //var c = (from ch in db.Chains
                    //         where ch.ChainNumber == s.Chain.ChainNumber
                    //         select ch).FirstOrDefault();


                    //if (c == null)
                    //    db.Chains.Add(s.Chain);

                    db.Entry(s).State = s.Id == 0 ?
                           EntityState.Added :
                           EntityState.Modified;

                    db.Stores.Add(s);
                    db.SaveChanges();

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    // Console.WriteLine(e.InnerException.InnerException.Message);
                    Console.Write("exception");
                    //  throw;
                }
                Console.Write(" add store: *{0}* ", stores.First().ChainId);
            }

        }

    }
    /*---------------------------------*/


        /*
        public void AddPricesToDb(ICollection<Price> prices)
        {
            foreach (var p in prices)
            {
                using (var db = new PricesContext())
                {
                    try
                    {

                        db.Prices.Add(p);

                        //db.Entry(p).State = p.PriceId == 0 ?
                        //         EntityState.Added :
                        //         EntityState.Modified;

                        db.Entry(p.Item).State = p.Item.Id == 0 ?
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
