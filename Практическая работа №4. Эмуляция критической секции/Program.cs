
using System;
using System.Threading;

namespace MultiThreadingExample
{
    class Program
    {
        public static DataBase db = new DataBase();

        static void WorkerThreadMethod1()
        {
            Console.WriteLine("worker thread #1 - started");
            db.SaveData("worker thread #1 - Calling Database. Savedata");
            Console.WriteLine("worker thread #1 - Returned from output");
        }

        static void WorkerThreadMethod2()
        {
            Console.WriteLine("worker thread #2 - started");
            db.SaveData("worker thread #2 - Calling Database. Savedata");
            Console.WriteLine("worker thread #2 - Returned from output");
        }

        static void Main(string[] args)
        {
            ThreadStart worker1 = new ThreadStart(WorkerThreadMethod1);
            ThreadStart worker2 = new ThreadStart(WorkerThreadMethod2);

            Thread t1 = new Thread(worker1);
            Thread t2 = new Thread(worker2);

            t1.Start();
            t2.Start();

            Console.WriteLine("main - creating worker threads");

            t1.Join();
            t2.Join();

            Console.WriteLine("main - all done");
            Console.ReadLine();
        }
    }
}




