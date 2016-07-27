/* Moran Ankori */
/* Threading ex3 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace SyncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var instance1 = new SyncFile();
            instance1.Run();
            Console.WriteLine("Program finished");
            Console.Read();

        }
    }
}
