using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Primes
{
    class Program
    {

        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();

            //lab 1:

            Console.WriteLine("\n1 core:");
            var result = CalcPrimes_unefficient(1, 10000, 1);
            Console.WriteLine($"ElapsedTicks: {watch.ElapsedTicks}");

            watch.Restart();
            result = CalcPrimes(1, 10000, 1);
            Console.WriteLine($"ElapsedTicks: {watch.ElapsedTicks}");

            /*
            foreach (var i in result)
            {
                Console.Write($"{i} ,");
            }*/

            Console.WriteLine("\n2 cores:");
            watch.Restart();
            result = CalcPrimes(1, 10000, 2);
            Console.WriteLine($"ElapsedTicks: {watch.ElapsedTicks}");

            Console.WriteLine("\n3 cores:");
            watch.Restart();
            result = CalcPrimes(1, 10000, 3);
            Console.WriteLine($"ElapsedTicks: {watch.ElapsedTicks}");

            Console.WriteLine("\n8 cores:");
            watch.Restart();
            result = CalcPrimes(1, 10000, 8);
            Console.WriteLine($"ElapsedTicks: {watch.ElapsedTicks}");

            Console.WriteLine();
           
            //lab 2 test:
            result = CalcPrimes(1, 30000000);
            Console.WriteLine($" last number: {result.Last()}");

            Console.Read();
        }

        //lock every time prime number added to list 
        private static List<int> CalcPrimes_unefficient(int lower, int upper, int parallel_degree)
        {
            object locker = new object();
            var finalPrimes = new List<int>();
            bool isPrime;

            Parallel.For(lower, upper, new ParallelOptions { MaxDegreeOfParallelism = parallel_degree },
                 (i, loopstate) =>
             {
                 isPrime = true;
                 for (int j = 2; j < Math.Sqrt(i); j++)
                 {
                     if (i % j == 0)
                     {
                         isPrime = false;
                         break;
                     }
                 }
                 if (isPrime)
                 {
                     lock (locker)
                     {
                         finalPrimes.Add(i);
                     }
                 }
             });

            return finalPrimes;
        }

        //lock once when thread finished
        private static List<int> CalcPrimes(int lower, int upper, int parallel_degree)
        {
            object locker = new object();
            var finalPrimes = new List<int>();
            bool isPrime;

            Parallel.For(lower, upper, new ParallelOptions { MaxDegreeOfParallelism = parallel_degree },
              () =>
              {
                  return new List<int>();
              },
              (i, loopState, localPrimes) =>
              {

                  isPrime = true;
                  if (i < 2)
                  {
                      isPrime = false;
                  }
                  else {
                      isPrime = true;
                      for (int j = 2; j < Math.Sqrt(i); j++)
                      {

                          if (i % j == 0)
                          {
                              isPrime = false;
                              break;
                          }
                      }
                      if (isPrime)
                      {
                          localPrimes.Add(i);

                      }
                  }
                  return localPrimes;
              },
               (task_primes) =>
               {
                   lock (locker)
                   {
                       finalPrimes.AddRange(task_primes);
                   }
               }

              );
            return finalPrimes;
        }

        //lab 2- cancellation
        private static List<int> CalcPrimes(int lower, int upper)
        {
            object locker = new object();
            var finalPrimes = new List<int>();
            bool isPrime;
            Random rnd = new Random();

            Parallel.For(lower, upper,
              () =>
              {
                  return new List<int>();
              },
              (i, loopState, localPrimes) =>
              {
                  if (rnd.Next(10000000) == 0)
                  {
                      Console.WriteLine("Stopped");
                      loopState.Stop();
                  }
                  if (i < 2)
                  {
                      isPrime = false;
                  }
                  else {
                      isPrime = true;
                      for (int j = 2; j < Math.Sqrt(i); j++)
                      {

                          if (i % j == 0)
                          {
                              isPrime = false;
                              break;
                          }
                      }
                      if (isPrime)
                      {
                          localPrimes.Add(i);

                      }
                  }
                  return localPrimes;
              },
               (task_primes) =>
               {
                   lock (locker)
                   {
                       finalPrimes.AddRange(task_primes);
                   }
               }

              );
            return finalPrimes;
        }
    }
}
