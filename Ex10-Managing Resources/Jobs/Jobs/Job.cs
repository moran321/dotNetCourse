using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;

namespace Jobs
{
    static class NativeJob
    {
        [DllImport("kernel32")]
        public static extern IntPtr CreateJobObject(IntPtr sa, string name);

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool AssignProcessToJobObject(IntPtr hjob, IntPtr hprocess);

        [DllImport("kernel32")]
        public static extern bool CloseHandle(IntPtr h);

        [DllImport("kernel32")]
        public static extern bool TerminateJobObject(IntPtr hjob, uint code);
    }

    public class Job : IDisposable
    {
        private IntPtr _hJob;
        private List<Process> _processes;
        private bool _isDisposed;
        private int _sizeInBytes = 1024*10;

        //3.	Implement the constructor of the Job class, that accepts a string called “name”
        public Job(string name)
        {

            /*  a.   Call NativeJob.CreateJobObject passing IntPtr.Zero
             *       and the name argument.
             *       Store the returning handle in _hJob 
             */
            _hJob = NativeJob.CreateJobObject(IntPtr.Zero, name);

            //b.	If the handle is zero (IntPtr.Zero), 
            //      throw an InvalidOperationException
            if (_hJob == IntPtr.Zero)
            {
                throw new InvalidOperationException("IntPtr is zero");
            }
            if (_processes == null)
            {
                _processes = new List<Process>();
            }

            //c.	Create the _processes object
            _processes = new List<Process>();

            //b.	Add a call in the Job ctor to GC.AddMemoryPressure(sizeInByte) 
            //      and display a message that the Job was created
            GC.AddMemoryPressure(_sizeInBytes);
            Console.WriteLine($"job {name} created");
        }

        public Job() : this(null) { }

        protected void AddProcessToJob(IntPtr hProcess)
        {
            CheckIfDisposed();

            if (!NativeJob.AssignProcessToJobObject(_hJob, hProcess))
            {
                throw new InvalidOperationException("Failed to add process to job");
            }
            
        }

        private void CheckIfDisposed()
        {
            if (_isDisposed)
            {
                //b.  if the object is disposed throws a ObjectDisposedException
                throw new ObjectDisposedException($"{this.GetType().Name}");
            }
        }

        public void AddProcessToJob(int pid)
        {
            AddProcessToJob(Process.GetProcessById(pid));
        }

        public void AddProcessToJob(Process proc)
        {
            Debug.Assert(proc != null);
            AddProcessToJob(proc.Handle);
            _processes.Add(proc);
        }

        //5. Implement the Kill method by calling NativeJob.TerminateJobObject
        public void Kill()
        {
            NativeJob.TerminateJobObject(_hJob, 0);
        }

        //4. Implement the IDisposable interface 
        public void Dispose()
        {
            CheckIfDisposed();
            //a. Make use of the Dispose pattern
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                foreach (Process p in _processes)
                {
                    p.Dispose();
                }
                Kill();
                _processes.Clear();
                _hJob = IntPtr.Zero;
            }
            GC.RemoveMemoryPressure(_sizeInBytes);
            Console.WriteLine("job released");
            _isDisposed = true;

        }

        //Finalizer
        ~Job()
        {
            // c.   Add a call in the finalizer to GC.RemoveMemoryPressure(sizeInBytes)
            //      and display a message that the Job was released
            Dispose(false);
        }


    }
}
