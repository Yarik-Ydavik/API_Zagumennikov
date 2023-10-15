using System.Runtime.InteropServices;

class MainClass
{
    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "GlobalMemoryStatusEx", SetLastError = true)]
    static extern bool _GlobalMemoryStatusEx(ref MEMORYSTATUS lpBuffer);

    public struct MEMORYSTATUS
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }
    static void Main(string[] args)
    {
        MEMORYSTATUS memStatus = new MEMORYSTATUS();
        memStatus.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUS));
        _GlobalMemoryStatusEx(ref memStatus);

        Console.WriteLine("объем физической памяти: " + memStatus.ullTotalPhys);
        Console.WriteLine("объем памяти, доступной в данный момент: " + memStatus.ullAvailPhys);
        Console.WriteLine("объем файла подкачки: " + memStatus.ullTotalPageFile);
        Console.WriteLine("объем файла подкачки, доступного в данный момент: " + memStatus.ullAvailPageFile);
        Console.WriteLine("всего виртуальной памяти: " + memStatus.ullTotalVirtual);
        Console.WriteLine("объем виртуальной памяти, доступной в данный момент: " + memStatus.ullAvailVirtual);
        Console.WriteLine("объем памяти, используемой процессов: " + (memStatus.ullTotalVirtual - memStatus.ullAvailVirtual));
    }
}   