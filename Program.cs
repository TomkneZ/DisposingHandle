using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinarySemaphore
{
    internal class Program
    {
        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        private const int StdOutputHandle = -11;
        static _Mutex myMutex = new _Mutex();
        static int x = 0;
        static void Main(string[] args)
        {
            for (int i = 0; i<5; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = $"Поток {i}";
                myThread.Start();
            }
            CloseHandleAsync(GetStdHandle(StdOutputHandle), 8000);
            Console.ReadLine();
        }
        public static void Count()
        {           
            myMutex.Lock();
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
            myMutex.UnLock();
        }
        private static async void CloseHandleAsync(IntPtr ptr, int delay)
        {
            Console.WriteLine("Handle " + ptr + " will be disposed in " + delay + " msecs");
            await Task.Delay(delay);
            Console.WriteLine("Disposing handle " + ptr);
            new OSHandle(ptr).Dispose();
        }
    }
}
