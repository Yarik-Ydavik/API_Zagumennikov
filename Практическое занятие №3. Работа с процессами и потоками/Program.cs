/*Задание: разработать приложение на языке C#, которое получает дескриптор, имя или полное имя 
модуля и возвращает другие два элемента в выходных параметрах.
Алгоритм выполнения:
▪ Используя функцию GetCurrentProcessId определит идентификатор текущего
процесса. 
▪ Используя функцию GetCurrentProcess определит псевдодескриптор текущего
процесса. 
▪ Используя функцию DuplicateHandl и значение псевдодескриптора определит
дескриптора текущего процесса. 
▪ Используя функцию OpenProcess определит копию дескриптора текущего
процесса. 
▪ Закроет дескриптор, полученный функцией DuplicateHandl. 
▪ Закроет дескриптор, полученный функцией OpenProcess.*/
using System;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll", SetLastError = true)]
static extern int GetCurrentProcessId();

[DllImport("kernel32.dll", SetLastError = true)]
static extern IntPtr GetCurrentProcess();

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess, bool bInheritHandle, uint dwOptions);

[DllImport("kernel32.dll", SetLastError = true)]
static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool CloseHandle(IntPtr hObject);

[DllImport("psapi.dll", SetLastError = true)]
static extern bool GetModuleHandleEx(uint dwFlags, string lpModuleName, out IntPtr phModule);

[DllImport("psapi.dll", SetLastError = true)]
static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] char[] lpBaseName, uint nSize);

static void Main(string[] args)
{
    int processId = GetCurrentProcessId();
    IntPtr pseudoDescriptor = GetCurrentProcess();
    IntPtr realDescriptor;
    bool success = DuplicateHandle(pseudoDescriptor, pseudoDescriptor, pseudoDescriptor, out realDescriptor, 0, false, 2);
    if (!success)
    {
        Console.WriteLine("Error duplicating handle: " + Marshal.GetLastWin32Error());
        return;
    }
    IntPtr processHandle = OpenProcess(0x0400 | 0x0010 | 0x0008 | 0x00100000, false, processId);
    if (processHandle == IntPtr.Zero)
    {
        Console.WriteLine("Error opening process: " + Marshal.GetLastWin32Error());
        return;
    }
    success = CloseHandle(realDescriptor);
    if (!success)
    {
        Console.WriteLine("Error closing real descriptor: " + Marshal.GetLastWin32Error());
    }
    success = CloseHandle(processHandle);
    if (!success)
    {
        Console.WriteLine("Error closing process handle: " + Marshal.GetLastWin32Error());
    }

    string moduleName = args[0];
    IntPtr moduleHandle;
    success = GetModuleHandleEx(0, moduleName, out moduleHandle);
    if (!success)
    {
        Console.WriteLine("Error getting module handle: " + Marshal.GetLastWin32Error());
        return;
    }

    char[] moduleNameChars = new char[1024];
    uint result = GetModuleFileNameEx(processHandle, moduleHandle, moduleNameChars, (uint)moduleNameChars.Length);
    if (result == 0)
    {
        Console.WriteLine("Error getting module file name: " + Marshal.GetLastWin32Error());
        return;
    }
    string moduleFileName = new string(moduleNameChars, 0, (int)result);

    Console.WriteLine("Module handle: " + moduleHandle);
    Console.WriteLine("Module file name: " + moduleFileName);




    /*    while (true)
        {
            Console.WriteLine(
            "1: Вывести все\n" +
            "2: По имени\n" +
            "3: По полному имени\n" +
            "4: По дескриптору\n" +
            "5: Информация о процессе\n" +
            "6: Информация о процессах, потоках, модулях\n"
            );
            switch (pressedKey)
            {
                case 1: //all 
                    init();
                    Console.WriteLine(
                     $"Имя: {shortFileName}\n" +
                     $"Полное имя: {longFileName}\n" +
                     $"Дескриптор: {processHandle}\n"
                     );
                    break;
                case 2: //name 
                        //
                    break
                   ;
                case 3: //fname 
                        //...
                default
           :
                    break
                   ;

            }

        }*/
}