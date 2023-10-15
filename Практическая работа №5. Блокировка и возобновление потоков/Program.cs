object lockObject = new object();
int value = 0;
bool shouldStop = false;

Thread thread2 = new Thread(delegate () {
    int x = 3;
    int y = 5;
    Console.WriteLine("Geometric progression:");
    for (int i = 0; i < 5; i++)
    {
        Thread.Sleep(1000);
        if (x == 375)
        {
            value = x;
            lock (lockObject)
            {
                Console.WriteLine("Thread was blocked");
                Thread.Sleep(500);
                Monitor.Pulse(lockObject);
            }
            shouldStop = true;
            break;
        }
        Console.Write(x + " ");
        x *= y;
    }

});

Thread thread1 = new Thread(delegate () {
    thread2.Start();
    if (value == 375)
    {
        lock (lockObject)
        {
            Monitor.Wait(lockObject);
        }
    }

    while (thread2.IsAlive)
    {
        if (shouldStop)
        {
            thread2.Join();
            Console.WriteLine("Thread was ended");
            break;
        }
    }
});
thread1.Start();