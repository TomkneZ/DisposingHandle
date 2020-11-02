using System;
using System.Runtime.InteropServices;

namespace BinarySemaphore
{
    internal class OSHandle : IDisposable
    {
        private bool _disposed = false;

        public IntPtr Handle { get; set; }

        private _Mutex myMutex = new _Mutex();

        public OSHandle(IntPtr ptr)
        {
            Handle = ptr;
        }

        ~OSHandle() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            myMutex.Lock();
            if (_disposed)
            {
                return;
            }
            if (disposing && Handle != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Handle);
                Handle = IntPtr.Zero;
            }            
            _disposed = true;
            myMutex.UnLock();
        }       
    }
}