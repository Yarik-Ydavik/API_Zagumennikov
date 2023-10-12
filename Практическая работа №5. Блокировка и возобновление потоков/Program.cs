object lockObject = new object(); // создаем объект блокировки
int value = 0;
bool shouldStop = false; // флаг для завершения потока

Thread thread2 = new Thread(delegate () {
    int a = 2; // первый элемент прогрессии
    int q = 3; // знаменатель прогрессии
    int n = 5; // количество элементов прогрессии

    Console.WriteLine("Геометрическая прогрессия:");

    for (int i = 0; i < n; i++)
    {
        Thread.Sleep(1000);
        if (a == 18) // если получено заранее определенное значение
        {
            value = a;
            lock (lockObject) // блокируем объект
            {
                // делаем что-то, например, выводим сообщение о блокировке
                Console.Write("Поток заблокирован");

                Thread.Sleep(500); // ждем произвольное время

                Monitor.Pulse(lockObject); // отправляем сигнал на разблокировку
            }
            shouldStop = true; // устанавливаем флаг завершения потока
            break; // выходим из цикла
        }
        Console.Write(a + " ");
        a *= q;
    }

});

Thread thread1 = new Thread(delegate () {
    thread2.Start();

    if (value == 18) // если получено заранее определенное значение
    {
        lock (lockObject) // блокируем объект
        {
            Monitor.Wait(lockObject); // ожидаем сигнала на разблокировку
        }
    }

    while (thread2.IsAlive) // пока поток не завершился
    {
        if (shouldStop) // если установлен флаг завершения потока
        {
            thread2.Join(); // ожидаем завершения потока
            Console.WriteLine("Поток завершен");
            break; // выходим из цикла
        }
    }
});
thread1.Start();