using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Jobs
{
    class Program
    {
        static void Main(string[] args)
        {
            //6.	In the Main method, create a Job object, 
            //      and assign some processes to it 
            //      (Use Process.Start to create some simple processes, such as “notepad” or “calc”).

            /*

            var jobs = new Job("myJobs");
            jobs.AddProcessToJob(Process.Start("mspaint"));
            jobs.AddProcessToJob(Process.Start("notepad"));
            
            //7. Call Console.ReadLine and after the user hits <enter> 
            //  kill all processes in the job using the Kill 
            Console.ReadLine();
            jobs.Kill();

            */

            ///////////////// 8. part B /////////////////
            Random rand = new Random();
            var jobs2 = new List<Job>();
            // d.   Create a loop in your main method that creates 20 Job objects
            for (int i =0; i<20; i++)
            {
                //test the dispose pattern
                if (rand.Next(10) > 7)
                {
                    jobs2[jobs2.Count - 1].Dispose();
                    jobs2.RemoveAt(jobs2.Count - 1);
                }
                Job j= new Job($"job {i}");
                jobs2.Add(j);
                j.AddProcessToJob(Process.Start("notepad"));

              
            }

            //e.	See what happens when you run the application with different “sizeInBytes”.
            //      Try 0 MB and 10 MB

            Console.ReadLine();
            foreach (Job j in jobs2)
            {
                j.Kill();
            }
            
        }
        ////
    }
}
