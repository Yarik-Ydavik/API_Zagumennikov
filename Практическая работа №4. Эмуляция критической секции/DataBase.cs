using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class DataBase
{
    public void SaveData(string text)
    {
        lock (this)
        {
            Console.WriteLine("Database. SaveData - Started");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("x");
                Thread.Sleep(100);
            }
            Console.WriteLine();
            Console.WriteLine("dataBase. SaveData - working");
            Thread.Sleep(2000);
            Console.WriteLine("dataBase. SaveData - ended");
        }
    }
}
