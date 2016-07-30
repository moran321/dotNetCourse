using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectBuilder
{
    class ProjectBuilder
    {

        private void BuildProject(int projectNumber)
        {
            Thread.Sleep(1000); //1sec
            Console.WriteLine($"Built project {projectNumber}");
        }

        //4)
        public void BuildAll()
        {
            Task proj_1 = Task.Factory.StartNew(() => BuildProject(1));
            Task proj_2 = Task.Factory.StartNew(() => BuildProject(2));
            Task proj_3 = Task.Factory.StartNew(() => BuildProject(3));

            Task proj_4 = proj_1.ContinueWith(_ => BuildProject(4));
            Task proj_5 = Task.Factory.ContinueWhenAll(new Task[] { proj_1, proj_2, proj_3 }, _ => BuildProject(5));
            Task proj_6 = Task.Factory.ContinueWhenAll(new Task[] { proj_3, proj_4 }, _ => BuildProject(6));
            Task proj_7 = Task.Factory.ContinueWhenAll(new Task[] { proj_5, proj_6 }, _ => BuildProject(7));
            Task proj_8 = proj_5.ContinueWith((_) => BuildProject(8));
            Task.WaitAll(proj_7, proj_8);
        }

        //3)
        public void BuildSequentially()
        {
            BuildProject(1);
            BuildProject(2);
            BuildProject(3);
            BuildProject(4);
            BuildProject(5);
            BuildProject(6);
            BuildProject(7);
            BuildProject(8);
        }
    }


   
}
