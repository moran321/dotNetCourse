using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAwaiter
{
    class TestProgram
    {
        public async Task TestProgramAsync()
        {
            Console.WriteLine("Wait for 2000 milliseconds (2sec)");
            Console.WriteLine($"start time:{DateTime.Now.Second}");
            await 2000;
            Console.WriteLine($"end time:{DateTime.Now.Second}");

            await 1000;
            try
            {
                Console.WriteLine("Starting process (open explorer)");
                await Process.Start("IExplore.exe");
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
        /******************************/
    }
}
