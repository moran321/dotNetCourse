using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomAwaiter
{

    public static class MyAwaiter
    {
       
        // 1.	Create an awaiter that will allow awaiting on a integer.
        public static TaskAwaiter GetAwaiter(this int milliseconds)
        {
            return Task.Delay(TimeSpan.FromMilliseconds(milliseconds)).GetAwaiter();
        }
        /******************************/

        // 2.	Create a custom awaiter that will allow awaiting on a process and continue when the process exit
        public static TaskAwaiter<int> GetAwaiter(this Process process)
        {
            var tcs = new TaskCompletionSource<int>();
            process.EnableRaisingEvents = true;
            process.Exited += (obj, ea) => tcs.TrySetResult(process.ExitCode);

            if (process.HasExited)
            {
                tcs.TrySetResult(process.ExitCode);
            }

            return tcs.Task.GetAwaiter();
        }
        /******************************/

    }

}


