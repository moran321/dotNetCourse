using PriceCompare.Model;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PriceCompare.Model.App
{
    public class Program
    {
        //     private string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; 
        //                          AttachDbFilename = C:\GiT\dotNetCourse\FinalProject\PriceCompare\PriceCompareServer\PricesDatabase.mdf; 
        //                         Integrated Security = True";


        static void Main(string[] args)
        {

            //download files direct from web
           // var d = new Download();
          //  d.Run();

            
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PricesContext>());
         //   Database.SetInitializer(new DropCreateDatabaseAlways<PricesContext>());

            DbBuilder manager = new DbBuilder();
            manager.InitializeDB();

            Task.WaitAll();
            Console.WriteLine("************done*************");
          
     
            Console.ReadLine();


        }

    }
}
