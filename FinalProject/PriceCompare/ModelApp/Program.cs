using PriceCompare.Model;
using System;
using System.Data.Entity;

namespace ModelApp
{
    public class Program
    {
   //     private string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; 
   //                          AttachDbFilename = C:\GiT\dotNetCourse\FinalProject\PriceCompare\PriceCompareServer\PricesDatabase.mdf; 
    //                         Integrated Security = True";

       

        static void Main(string[] args)
        {
           // Database.SetInitializer<PricesContext>
          //     (new DropCreateDatabaseIfModelChanges<PricesContext>());

            Database.SetInitializer(new DropCreateDatabaseAlways<PricesContext>());

            DbBuilder manager = new DbBuilder();
            manager.InitializeDB();
            Console.WriteLine("************done*************");
            Console.ReadLine();
        }

    }
}
