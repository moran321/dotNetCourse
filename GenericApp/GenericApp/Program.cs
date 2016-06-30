/* Lab 7.2 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GenericApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiDictionary<int, string> myDict = new MultiDictionary<int, string>();
            myDict.Add(1, "one");
            myDict.Add(2, "two");
            myDict.Add(3, "three");
            myDict.Add(1, "ich");
            myDict.Add(2, "nee");
            myDict.Add(3, "sun");

            Console.WriteLine("My Dictionary Values:");
            foreach (KeyValuePair<int, string> pair in myDict)
            {
                Console.WriteLine(pair);
            }

            Console.WriteLine("\nCount: " + myDict.Count);

            myDict.Remove(3);
            Console.WriteLine("\nafter remove key '3':");
            Console.WriteLine("My Dictionary Values:");
            foreach (KeyValuePair<int, string> pair in myDict)
            {
                Console.WriteLine(pair);
            }

            Console.WriteLine("\nCount: " + myDict.Count);

            Console.WriteLine("\ntry to remove non-exist key '4': " + myDict.Remove(4));

            Console.WriteLine("\ncontains {1,\"one\"}? " + myDict.Contains(1, "one"));

            myDict.Clear();
            Console.WriteLine("\nafter clear:");
            Console.WriteLine("Count: " + myDict.Count);

            Console.WriteLine("\ncontains {1,\"one\"}? " + myDict.Contains(1, "one"));

            myDict.Add(1, "ahat");
            myDict.Add(2, "shtaim");
            myDict.Add(3, "shalosh");

            Console.WriteLine("\nMy Dictionary Values:");
            foreach (KeyValuePair<int, string> pair in myDict)
            {
                Console.WriteLine(pair);
            }

            Console.WriteLine("\nCount: " + myDict.Count);

            Console.WriteLine("\nThe values: ");
            foreach (string val in myDict.Values)
            {
                Console.WriteLine(val);
            }
        }


    }
}
