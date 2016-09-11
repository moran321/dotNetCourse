using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceCompare.Model
{




    //context class
    public class PricesContext : DbContext
    {
        public DbSet<Chain> Chains { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }


        public PricesContext() : base("PricesDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            //one-to-many 
            modelBuilder.Entity<Store>()
                        .HasRequired(s => s.Chain)
                        .WithMany(s => s.Stores)
                        .HasForeignKey(s => s.ChainId);
            // .HasForeignKey(e => new { e.StoreId, e.ChainId });


            //one-to-many 
            modelBuilder.Entity<Price>()
                .HasRequired<Item>(s => s.Item)
                .WithMany(s => s.Prices);

        }

        /*---------------------------------*/


        // RecreateDatabaseIfModelChanges 
        public class ContextSeedInitializer : DropCreateDatabaseIfModelChanges<PricesContext> //CreateDatabaseIfNotExists<PricesContext>
        {
            protected override void Seed(PricesContext context)
            {
                Console.WriteLine("seed");
            }
        }
        /*---------------------------------*/




    }
}