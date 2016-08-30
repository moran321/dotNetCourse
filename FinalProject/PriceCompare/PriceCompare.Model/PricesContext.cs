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

            /*
            modelBuilder.Entity<Chain>().ToTable("Chains");
            modelBuilder.Entity<Store>().ToTable("Stores");
            modelBuilder.Entity<Item>().ToTable("Items");
            modelBuilder.Entity<Price>().ToTable("Prices");
            */

            modelBuilder.Entity<Chain>()
            .HasMany(e => e.Stores)
            .WithOptional(e => e.Chain);
            //.HasForeignKey(e => e.ChainId);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Prices)
                .WithOptional(e => e.Item);
                //.HasForeignKey(e => e.Item_Id);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Prices);
               // .WithOptional(e => e.Store)
               // .HasForeignKey(e => new { e.Store_StoreId, e.Store_ChainName });
                
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