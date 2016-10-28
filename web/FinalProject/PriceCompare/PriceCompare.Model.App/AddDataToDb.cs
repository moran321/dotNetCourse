using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace PriceCompare.Model.App
{
    public static class Extentions
    {
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
    /*---------------------------------------------------------------------*/
    /*---------------------------------------------------------------------*/

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
            Console.WriteLine("Prices added");
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
            Console.WriteLine("Stores added");
        }
        /*---------------------------------*/

         
    }
}
