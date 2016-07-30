/* Moran Ankori */
/* TPL & AsyncAwait- Lab 5 */

using System;

namespace CustomAwaiter
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new TestProgram();
            test.TestProgramAsync().Wait();
            Console.WriteLine("Program finished");
            Console.Read();
        }
        /******************************/

   
    }
}
