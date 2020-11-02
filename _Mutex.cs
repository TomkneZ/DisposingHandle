using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinarySemaphore
{
    class _Mutex
    {
        private int IsLocked = 0;

        public void Lock()
        {
            while (Interlocked.CompareExchange(ref IsLocked, 1, 0) == 1)
            {
                Thread.Sleep(10);
            }
        }

        public void UnLock()
        {
            Interlocked.Exchange(ref IsLocked, 0);
        }
    }
}
