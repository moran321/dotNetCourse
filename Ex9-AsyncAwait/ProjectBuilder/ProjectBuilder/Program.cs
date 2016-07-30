/*Moran Ankori*/
/*Lab 3 - Task Continuations */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace ProjectBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ProjectBuilder();

            //3)
            var watch = new Stopwatch();
            watch.Start();
            builder.BuildSequentially();
            Task.WaitAll();
            Console.WriteLine($"elapsed time:{watch.ElapsedMilliseconds}");
            //4)
            watch.Restart();
            builder.BuildAll();
            Task.WaitAll();
            Console.WriteLine($"elapsed time:{watch.ElapsedMilliseconds}");

            Console.Read();
        }
    }
}
