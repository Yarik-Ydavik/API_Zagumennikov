using System.Text;
using System.Runtime.InteropServices;
using System;

[System.Runtime.InteropServices.DllImport("user32.dll")]
static extern int GetSystemMetrics(int nIndex);

[DllImport("kernel32.dll", SetLastError = true)]
static extern uint GetVersion();

uint minorVersion = Convert.ToUInt32(GetVersion() & 0x0000FF00) >> 8;

Console.WriteLine(" ");
// Имя компьютера
[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
static extern int GetComputerName([Out] StringBuilder nameBuffer, ref int bufferSize);
int size = 0;
GetComputerName(null, ref size); // Какой актуальный размер у имени компа
StringBuilder buffer = new StringBuilder(size);
GetComputerName(buffer, ref size);
// Получение пути к системным каталогам Windows
Console.WriteLine("Имя ПК: " + buffer.ToString());
Console.WriteLine("Имя пользователя ПК: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
Console.WriteLine(" ");



// Получение пути к системным каталогам Windows
[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
static extern uint GetWindowsDirectory(StringBuilder lpBuffer, uint uSize);

string WindowsDirectory()
{
    uint size = 0;
    size = GetWindowsDirectory(null, size);

    StringBuilder sb = new StringBuilder((int)size);
    GetWindowsDirectory(sb, size);

    return sb.ToString();
}
Console.WriteLine("Получение пути к системным каталогам Windows");
Console.WriteLine(WindowsDirectory());
Console.WriteLine(" ");



Console.WriteLine("Версия ОС: ");
Console.WriteLine("(major) Главная версия ОС: " + Convert.ToUInt32(GetVersion() & 0x000000FF));
Console.WriteLine("(minor) Незначительная версия ОС после точки: " + minorVersion);

