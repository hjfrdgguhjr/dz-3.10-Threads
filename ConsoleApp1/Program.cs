using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(() =>
            {
                Console.WriteLine("[Поток 1] Запуск потока.");
                Console.WriteLine("[Поток 1] Запуск Notepad.");

                Process.Start("notepad.exe");
                Thread.Sleep(3000);

                foreach (Process p in Process.GetProcessesByName("Notepad"))
                    p.Kill();

                Console.WriteLine("[Поток 1] Notepad закрыт.");
            });

            Thread t2 = new Thread(() =>
            {
                Console.WriteLine("[Поток 2] Запуск потока.");
                Console.WriteLine("[Поток 2] Запуск cmd.exe.");

                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/c dir";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;

                p.Start();
                Console.WriteLine(p.StandardOutput.ReadToEnd());
                p.WaitForExit();
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }
    }
}
