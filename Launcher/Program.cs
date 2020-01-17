using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (object i in args)
            {
                Console.WriteLine(i);
            }
            try
            {
                string executable = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\AllGameLauncher\start.txt");
                Process.Start(executable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

        }
    }
}
