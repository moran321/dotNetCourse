/* Moran Ankori */
/* Threading ex2 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    class Program
    {

        static void Main(string[] args)
        {
            int maxSize = 10;
            LimitedQueue<int> queue = new LimitedQueue<int>(maxSize);

            for (int i = 0; i < 10; i++)
                ThreadPool.QueueUserWorkItem((_) =>
                {
                    Console.WriteLine($"thread:{Thread.GetDomainID()}");
                    Random r = new Random();
                    for (int j = 0; j < 100; j++)
                    {
                        if (r.Next(4) > 1)
                        {
                            queue.Enque(r.Next(10));
                            Console.WriteLine("Enque"); //write operation
                        }
                        else {
                            queue.Deque();
                            Console.WriteLine("Deque"); //write operation
                        }
                        Thread.Sleep(r.Next(200));
                        Console.WriteLine(queue.ToString()); //read operation

                        if (queue.Count > maxSize)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"ERROR!");
                        }
                        Console.WriteLine($"count:{queue.Count}");

                        ;

                    }
                });
            Console.Read();
        }
    }
}
