using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncDemo
{
    class SyncFile
    {
        public void Run()
        {
            using (Mutex mutex = new Mutex(false, "MySyncMutex"))
            {

                for (int i = 0; i < 1000; i++)
                {
					mutex.WaitOne();
                    try
                    {                    
                        using (StreamWriter writer = new StreamWriter(@"c:\temp\data.txt", true))
                        {
                            writer.WriteLine("Process is writing to file" + Process.GetCurrentProcess().Id);
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Exception: " + e);
                        return;
                    }
                    finally
                    {
						mutex.ReleaseMutex();
                    }
                }

            }
        }
        /******************************************/
    }
}
