/*Moran Ankori*/
/*8.2*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace MailSystem
{

    class Program
    {

        static void Main(string[] args)
        {

            // 7. In the Main method, create an instance of MailManager and connect to the MailArrived event.
            MailManager manager = new MailManager();

            // 9.Call the SimulateMailArrived to check the event connection.
            manager.SimulateMailArrived();

            //subscribe the handler to event
            manager.MailArrived += MailArrivedHandler;

            // 10. Create a System.Threading.Timer, and in the timer callback call SimulateMailArrived every 1 second.
            //     Call Thread.Sleep in the main thread to keep the application alive, or call Console.ReadLine.
            Timer timer = new Timer(_ => manager.SimulateMailArrived(), null, 0, 1000);
            Console.ReadLine();
        } /*** end main ***********************************/


        // 8.In the handler, output the title and body to the console.
        static void MailArrivedHandler(Object sender, MailArrivedEventArgs e)
        {
            Console.WriteLine("Title: {0}", e.Title);
            Console.WriteLine("Body: {0}", e.Body);
            Console.WriteLine();
        }/**************************************/

    }/* end class program *************************************/
}/**************************************/
