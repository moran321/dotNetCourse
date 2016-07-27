
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    class LimitedQueue<T>
    {
        private ReaderWriterLockSlim cacheLock;
        private Queue<T> _queue;
        private SemaphoreSlim _semaphor;

        public int Count
        { get { return _queue.Count; } }


        /*******************************/

        public LimitedQueue(int maxQSize)
        {
            if (maxQSize < 1)
                throw new ArgumentException();
            _queue = new Queue<T>();
            _semaphor = new SemaphoreSlim(maxQSize, maxQSize);

            cacheLock = new ReaderWriterLockSlim();

        }
        /*******************************/



        public void Enque(T inputObj)
        {
            //wait if semaphore reached to full capacity
            _semaphor.Wait(); 
            cacheLock.EnterWriteLock();
            try
            {
                _queue.Enqueue(inputObj);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
        /*******************************/


        public T Deque()
        {    
            T returnVal = default(T);
            cacheLock.EnterWriteLock();
            try
            {
                returnVal = _queue.Dequeue();
                _semaphor.Release(); //release place in queue
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Queue is empty");
            }
            finally
            {
                cacheLock.ExitWriteLock();
              
            }
           
            return returnVal;

        }
        /*******************************/


        //reader 
        public override string ToString()
        {
            var sb = new StringBuilder("Objects in queue:");
           
            cacheLock.EnterReadLock();
            try
            {
                foreach (T t in _queue)
                {
                    sb.Append(t.ToString() + "\t");
                }
            }
            finally
            {
                cacheLock.ExitReadLock();
               
            }
           
            return sb.ToString();
        }
        /*******************************/
    }
}
