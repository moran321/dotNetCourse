/*Moran Ankori*/
/*Lab 4.1*/
using System;
using System.Linq;
using System.Diagnostics;

namespace LINQLab
{
    class Program
    {

        static void Main(string[] args)
        {

            // 1) a.
            //Display all the public interface names in the mscorlib assembly
            //and the number of methods in each type. 
            //Sort by type name
            var intrefaces = from mscorlib in typeof(string).Assembly.GetExportedTypes()
                             where mscorlib.IsInterface
                             orderby mscorlib.Name
                             select new
                             {   //tuple of interface name and number of methods in it
                                 TypeName = mscorlib.Name,
                                 NumMethods = mscorlib.GetMethods().Length
                             };




            // 1) b.
            //  Display all processes running on your system (process name, id and start time), 
            //  whose thread count is less than 5.
            //  Sort by process id
            var processes = from systemProcess in Process.GetProcesses()
                            where systemProcess.IsAccessible() && systemProcess.Threads.Count < 5
                            orderby systemProcess.Id
                            select new
                            {   //tuple of process name id and start time
                                Name = systemProcess.ProcessName,
                                ID = systemProcess.Id,
                                StartTime = systemProcess.StartTime
                            };





            // 1) c. (*)
            //grouping the processes by their base priority.
            var grouped_processes = from systemProcess in Process.GetProcesses()
                                    where systemProcess.IsAccessible() && systemProcess.Threads.Count < 5
                                    orderby systemProcess.Id
                                    group new
                                    {
                                        Name = systemProcess.ProcessName,
                                        ID = systemProcess.Id,
                                        StartTime = systemProcess.StartTime
                                    } by systemProcess.BasePriority
                                    into priority
                                    //  orderby priority.Key
                                    select priority;


            // 1) d.
            Console.WriteLine($"Total threads: {Process.GetProcesses().Sum(x => x.Threads.Count)}");


            //2)test:
            var this_obj = new TestObj("Moran", 201, "RamatGan");
            var other_obj = new TestObj("Someone", 340, "Herzeliya");
            Console.WriteLine($"other_obj before: { other_obj} ");
            this_obj.CopyTo(other_obj);
            Console.WriteLine($"other_obj after: { other_obj} ");
           
            //test outputs:

            Console.WriteLine("\nintrefaces in mscorlib assembly:");
            foreach (var intreface in intrefaces)
            {
                Console.WriteLine(intreface);
            }

            Console.WriteLine("\nprocesses running on my system:");
            foreach (var process in processes)
            {
                Console.WriteLine(process);
            }

            Console.WriteLine("\nprocess grouped by priority:");
            foreach (var priority in grouped_processes)
            {
                Console.WriteLine($"Priority: {priority.Key}");
                foreach (var process in priority)
                    Console.WriteLine(process);
            }


            Console.Read();

        }
    }
    /******************************************/


   


}

