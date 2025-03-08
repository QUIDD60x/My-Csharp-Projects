using System;
using System.Diagnostics;
using System.Threading;

namespace FakeVirus
{
    class Program
    {
        public static void Main(string[] args)
        {
            int amt = 0;
            while (amt < 10)
            {
                Random rand = new Random();
                int num = rand.Next(1, 3);
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/c dir & timeout /t {num} >nul"; 
                process.StartInfo.UseShellExecute = true;
                process.Start();

                amt++;
            }
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string newPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GhostProgram.exe";

            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c timeout /t 2 >nul & move \"{exePath}\" \"{newPath}\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }
    }
}
