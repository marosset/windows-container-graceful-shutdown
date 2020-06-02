using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace delayed_shutdown
{
    class Program
    {
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWON_EVENT
        }

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool Add);

        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        public static volatile ManualResetEvent exitEvent = new ManualResetEvent(false);


        public static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                    Console.WriteLine("CTRL_C received");
                    exitEvent.Set();
                    return true;

                case CtrlTypes.CTRL_CLOSE_EVENT:
                    Console.WriteLine("CTRL_CLOSE received");
                    exitEvent.Set();
                    return true;

                case CtrlTypes.CTRL_BREAK_EVENT:
                    Console.WriteLine("CTRL_BREAK received");
                    exitEvent.Set();
                    return true;

                case CtrlTypes.CTRL_LOGOFF_EVENT:
                    Console.WriteLine("CTRL_LOGOFF received");
                    exitEvent.Set();
                    return true;

                case CtrlTypes.CTRL_SHUTDOWON_EVENT:
                    Console.WriteLine("CTRL_SHUTDOWN received");
                    exitEvent.Set();
                    return true;

                default:
                    return false;
            }
        }

        static int Main(string[] args)
        {
            if (!SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true))
            {
                Console.WriteLine("Error setting up control handler... :(");
                return -1;
            }

            Console.WriteLine("Waiting for control event...");

            exitEvent.WaitOne();

            var i = 60;
            Console.WriteLine($"Exiting in {i} seconds...");
            while (i > 0)
            {
                Console.WriteLine($"{i}");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                i--;
            }
            Console.WriteLine("Goodbye");
            return 0;
        }
    }
}